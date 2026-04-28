using Alojamiento.DataManagement.Context;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.Services;
using Alojamiento.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS (Dependency Injection) ---

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // ESTA ES LA LÍNEA MÁGICA PARA ARREGLAR EL ERROR:
    options.CustomSchemaIds(type => type.FullName);
});

// --- SEGURIDAD: CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMobileApp", policy =>
    {
        policy.WithOrigins("https://m.booking.com", "http://localhost:3000", "http://localhost:5173") // Permitir Vue local
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("X-Pagination"); // Exponer headers custom (Paginación)
    });
});

// --- SEGURIDAD: JWT Authentication ---
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecretaParaDesarrolloQueDeberiaEstarEnVariablesDeEntorno123";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "AlojamientoApi";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Registro de la Fuente Única de Verdad (PostgreSQL en Supabase)
builder.Services.AddDbContext<AlojamientoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de Capas de Negocio (Business Layer)

// Maneja la gestión de propiedades (Hoteles, Suites, Departamentos)
builder.Services.AddScoped<IAlojamientoService, AlojamientoService>();

// Maneja validación de disponibilidad y costos de estadía
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Maneja perfiles de Clientes/Colaboradores y sistema de puntos
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Nuevos servicios para el buscador de filtros y correos
builder.Services.AddScoped<IBuscadorService, BuscadorService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// --- NUEVOS SERVICIOS (Arquitectura Global) ---
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICollaboratorService, CollaboratorService>();

var app = builder.Build();

// --- INICIALIZACIÓN DE DATOS (SEEDER) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Alojamiento.DataManagement.Context.AlojamientoDbContext>();
        await Alojamiento.DataManagement.Seeders.DbSeeder.SeedAsync(context);
    }
    catch (System.Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al sembrar la base de datos.");
    }
}

// --- 2. CONFIGURACIÓN DEL PIPELINE (Middleware) ---

// Agregar el middleware de excepciones globales
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    // Interoperabilidad: Swagger permite visualizar y probar los contratos de la API
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permitir acceder a wwwroot/uploads para las fotos

// Middleware de Mitigación XSS (Custom)
app.UseMiddleware<AntiXssMiddleware>();

// Aplicar CORS
app.UseCors("AllowMobileApp");

// Autenticación antes que Autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapeo de Controladores para habilitar los Endpoints de Alojamiento y Usuarios
app.MapControllers(); 

app.Run();