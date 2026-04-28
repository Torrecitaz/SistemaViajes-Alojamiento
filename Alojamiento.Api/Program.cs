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
    // Mantiene la compatibilidad de nombres de clases entre proyectos
    options.CustomSchemaIds(type => type.FullName);
});

// --- SEGURIDAD: CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMobileApp", policy =>
    {
        policy.WithOrigins(
                "https://m.booking.com", 
                "http://localhost:3000", 
                "http://localhost:5173",
                "https://hospeda-ya-frontend.vercel.app" 
              ) 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("X-Pagination"); 
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
builder.Services.AddScoped<IAlojamientoService, AlojamientoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBuscadorService, BuscadorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
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

// Middleware de excepciones globales
app.UseMiddleware<GlobalExceptionMiddleware>();

// Habilitar Swagger en CUALQUIER entorno (Desarrollo y Producción/Render)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HospedaYa API V1");
    options.RoutePrefix = string.Empty; // Hace que Swagger cargue directamente en la raíz de la URL
});

app.UseHttpsRedirection();
app.UseStaticFiles(); // Para fotos en wwwroot/uploads

app.UseMiddleware<AntiXssMiddleware>();

app.UseCors("AllowMobileApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();