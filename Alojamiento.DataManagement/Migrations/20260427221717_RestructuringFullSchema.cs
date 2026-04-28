using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Alojamiento.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class RestructuringFullSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagenes",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "PropiedadInstalacion",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Puntos",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Resenas",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Instalaciones",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Reservas",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Habitaciones",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Propiedades",
                schema: "alo");

            migrationBuilder.EnsureSchema(
                name: "aud");

            migrationBuilder.EnsureSchema(
                name: "res");

            migrationBuilder.EnsureSchema(
                name: "seg");

            migrationBuilder.EnsureSchema(
                name: "geo");

            migrationBuilder.EnsureSchema(
                name: "pag");

            migrationBuilder.CreateTable(
                name: "AuditoriaGeneral",
                schema: "aud",
                columns: table => new
                {
                    AuditoriaGeneralId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreEsquema = table.Column<string>(type: "text", nullable: false),
                    NombreTabla = table.Column<string>(type: "text", nullable: false),
                    Operacion = table.Column<string>(type: "text", nullable: false),
                    RegistroId = table.Column<string>(type: "text", nullable: false),
                    DatosAnteriores = table.Column<string>(type: "text", nullable: true),
                    DatosNuevos = table.Column<string>(type: "text", nullable: true),
                    UsuarioAccion = table.Column<string>(type: "text", nullable: true),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaGeneral", x => x.AuditoriaGeneralId);
                });

            migrationBuilder.CreateTable(
                name: "Moneda",
                schema: "geo",
                columns: table => new
                {
                    MonedaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Simbolo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moneda", x => x.MonedaId);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                schema: "geo",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CodigoISO2 = table.Column<string>(type: "text", nullable: false),
                    CodigoISO3 = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "seg",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                schema: "alo",
                columns: table => new
                {
                    ServicioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Icono = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.ServicioId);
                });

            migrationBuilder.CreateTable(
                name: "TipoAlojamiento",
                schema: "alo",
                columns: table => new
                {
                    TipoAlojamientoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAlojamiento", x => x.TipoAlojamientoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "seg",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    NombreCompleto = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    FotoUrl = table.Column<string>(type: "text", nullable: true),
                    EmailVerificado = table.Column<bool>(type: "boolean", nullable: false),
                    UltimoAcceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "TasaCambio",
                schema: "geo",
                columns: table => new
                {
                    TasaCambioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonedaOrigenId = table.Column<int>(type: "integer", nullable: false),
                    MonedaDestinoId = table.Column<int>(type: "integer", nullable: false),
                    Tasa = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaVigencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasaCambio", x => x.TasaCambioId);
                    table.ForeignKey(
                        name: "FK_TasaCambio_Moneda_MonedaDestinoId",
                        column: x => x.MonedaDestinoId,
                        principalSchema: "geo",
                        principalTable: "Moneda",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TasaCambio_Moneda_MonedaOrigenId",
                        column: x => x.MonedaOrigenId,
                        principalSchema: "geo",
                        principalTable: "Moneda",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                schema: "geo",
                columns: table => new
                {
                    CiudadId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaisId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    EsCapital = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.CiudadId);
                    table.ForeignKey(
                        name: "FK_Ciudad_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "geo",
                        principalTable: "Pais",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaisMoneda",
                schema: "geo",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "integer", nullable: false),
                    MonedaId = table.Column<int>(type: "integer", nullable: false),
                    PaisMonedaId = table.Column<int>(type: "integer", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaisMoneda", x => new { x.PaisId, x.MonedaId });
                    table.ForeignKey(
                        name: "FK_PaisMoneda_Moneda_MonedaId",
                        column: x => x.MonedaId,
                        principalSchema: "geo",
                        principalTable: "Moneda",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaisMoneda_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "geo",
                        principalTable: "Pais",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccesoUsuario",
                schema: "aud",
                columns: table => new
                {
                    AccesoUsuarioId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    FechaAcceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    Exitoso = table.Column<bool>(type: "boolean", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesoUsuario", x => x.AccesoUsuarioId);
                    table.ForeignKey(
                        name: "FK_AccesoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anfitrion",
                schema: "seg",
                columns: table => new
                {
                    AnfitrionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    NombreEmpresa = table.Column<string>(type: "text", nullable: true),
                    DocumentoFiscal = table.Column<string>(type: "text", nullable: true),
                    CuentaBancaria = table.Column<string>(type: "text", nullable: true),
                    Verificado = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anfitrion", x => x.AnfitrionId);
                    table.ForeignKey(
                        name: "FK_Anfitrion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorAplicacion",
                schema: "aud",
                columns: table => new
                {
                    ErrorAplicacionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Modulo = table.Column<string>(type: "text", nullable: true),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    FechaError = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorAplicacion", x => x.ErrorAplicacionId);
                    table.ForeignKey(
                        name: "FK_ErrorAplicacion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                schema: "seg",
                columns: table => new
                {
                    NotificacionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Leida = table.Column<bool>(type: "boolean", nullable: false),
                    FechaLectura = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.NotificacionId);
                    table.ForeignKey(
                        name: "FK_Notificacion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenVerificacion",
                schema: "seg",
                columns: table => new
                {
                    TokenVerificacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Usado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaUso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenVerificacion", x => x.TokenVerificacionId);
                    table.ForeignKey(
                        name: "FK_TokenVerificacion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                schema: "seg",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioRolId = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol_RolId",
                        column: x => x.RolId,
                        principalSchema: "seg",
                        principalTable: "Rol",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                schema: "seg",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    CiudadId = table.Column<int>(type: "integer", nullable: true),
                    Domicilio = table.Column<string>(type: "text", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Calificacion = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReservas = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Cliente_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalSchema: "geo",
                        principalTable: "Ciudad",
                        principalColumn: "CiudadId");
                    table.ForeignKey(
                        name: "FK_Cliente_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "seg",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Propiedad",
                schema: "alo",
                columns: table => new
                {
                    PropiedadId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnfitrionId = table.Column<int>(type: "integer", nullable: false),
                    TipoAlojamientoId = table.Column<int>(type: "integer", nullable: false),
                    CiudadId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<decimal>(type: "numeric", nullable: true),
                    Longitud = table.Column<decimal>(type: "numeric", nullable: true),
                    Estrellas = table.Column<int>(type: "integer", nullable: true),
                    CalificacionPromedio = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalResenas = table.Column<int>(type: "integer", nullable: false),
                    AdmiteMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    Verificada = table.Column<bool>(type: "boolean", nullable: false),
                    EstadoPropiedad = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.PropiedadId);
                    table.ForeignKey(
                        name: "FK_Propiedad_Anfitrion_AnfitrionId",
                        column: x => x.AnfitrionId,
                        principalSchema: "seg",
                        principalTable: "Anfitrion",
                        principalColumn: "AnfitrionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Propiedad_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalSchema: "geo",
                        principalTable: "Ciudad",
                        principalColumn: "CiudadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Propiedad_TipoAlojamiento_TipoAlojamientoId",
                        column: x => x.TipoAlojamientoId,
                        principalSchema: "alo",
                        principalTable: "TipoAlojamiento",
                        principalColumn: "TipoAlojamientoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPagoCliente",
                schema: "pag",
                columns: table => new
                {
                    MetodoPagoClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    NombreTitular = table.Column<string>(type: "text", nullable: true),
                    MarcaTarjeta = table.Column<string>(type: "text", nullable: true),
                    UltimosCuatroDigitos = table.Column<string>(type: "text", nullable: true),
                    FechaExpiracion = table.Column<string>(type: "text", nullable: true),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagoCliente", x => x.MetodoPagoClienteId);
                    table.ForeignKey(
                        name: "FK_MetodoPagoCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Habitacion",
                schema: "alo",
                columns: table => new
                {
                    HabitacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    CapacidadAdultos = table.Column<int>(type: "integer", nullable: false),
                    CapacidadNinos = table.Column<int>(type: "integer", nullable: false),
                    NumBanos = table.Column<int>(type: "integer", nullable: false),
                    NumDormitorios = table.Column<int>(type: "integer", nullable: false),
                    AdmiteMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    TieneCocina = table.Column<bool>(type: "boolean", nullable: false),
                    TieneAireAcondicionado = table.Column<bool>(type: "boolean", nullable: false),
                    SuperficieM2 = table.Column<decimal>(type: "numeric", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.HabitacionId);
                    table.ForeignKey(
                        name: "FK_Habitacion_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadFoto",
                schema: "alo",
                columns: table => new
                {
                    PropiedadFotoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadFoto", x => x.PropiedadFotoId);
                    table.ForeignKey(
                        name: "FK_PropiedadFoto_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadPolitica",
                schema: "alo",
                columns: table => new
                {
                    PropiedadPoliticaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    HoraCheckIn = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraCheckOut = table.Column<TimeSpan>(type: "interval", nullable: false),
                    PermiteMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    PermiteNinos = table.Column<bool>(type: "boolean", nullable: false),
                    PoliticaCancelacion = table.Column<string>(type: "text", nullable: true),
                    ReglasCasa = table.Column<string>(type: "text", nullable: true),
                    EdadMinimaReserva = table.Column<int>(type: "integer", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadPolitica", x => x.PropiedadPoliticaId);
                    table.ForeignKey(
                        name: "FK_PropiedadPolitica_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadServicio",
                schema: "alo",
                columns: table => new
                {
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    ServicioId = table.Column<int>(type: "integer", nullable: false),
                    PropiedadServicioId = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadServicio", x => new { x.PropiedadId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_PropiedadServicio_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropiedadServicio_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalSchema: "alo",
                        principalTable: "Servicio",
                        principalColumn: "ServicioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                schema: "res",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    CodigoReserva = table.Column<string>(type: "text", nullable: false),
                    FechaCheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCheckOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumAdultos = table.Column<int>(type: "integer", nullable: false),
                    NumNinos = table.Column<int>(type: "integer", nullable: false),
                    LlevaMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    NumHabitaciones = table.Column<int>(type: "integer", nullable: false),
                    MonedaId = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    EstadoReserva = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Moneda_MonedaId",
                        column: x => x.MonedaId,
                        principalSchema: "geo",
                        principalTable: "Moneda",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisponibilidadHabitacion",
                schema: "alo",
                columns: table => new
                {
                    DisponibilidadHabitacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitacionId = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibilidadHabitacion", x => x.DisponibilidadHabitacionId);
                    table.ForeignKey(
                        name: "FK_DisponibilidadHabitacion_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalSchema: "alo",
                        principalTable: "Habitacion",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitacionFoto",
                schema: "alo",
                columns: table => new
                {
                    HabitacionFotoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitacionId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitacionFoto", x => x.HabitacionFotoId);
                    table.ForeignKey(
                        name: "FK_HabitacionFoto_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalSchema: "alo",
                        principalTable: "Habitacion",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarifaHabitacion",
                schema: "alo",
                columns: table => new
                {
                    TarifaHabitacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitacionId = table.Column<int>(type: "integer", nullable: false),
                    MonedaId = table.Column<int>(type: "integer", nullable: false),
                    PrecioPorNoche = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarifaHabitacion", x => x.TarifaHabitacionId);
                    table.ForeignKey(
                        name: "FK_TarifaHabitacion_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalSchema: "alo",
                        principalTable: "Habitacion",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarifaHabitacion_Moneda_MonedaId",
                        column: x => x.MonedaId,
                        principalSchema: "geo",
                        principalTable: "Moneda",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertenciaCliente",
                schema: "res",
                columns: table => new
                {
                    AdvertenciaClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    ReservaId = table.Column<int>(type: "integer", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Severidad = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertenciaCliente", x => x.AdvertenciaClienteId);
                    table.ForeignKey(
                        name: "FK_AdvertenciaCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertenciaCliente_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId");
                });

            migrationBuilder.CreateTable(
                name: "CancelacionReserva",
                schema: "res",
                columns: table => new
                {
                    CancelacionReservaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: false),
                    Penalizacion = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaCancelacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAccion = table.Column<string>(type: "text", nullable: true),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelacionReserva", x => x.CancelacionReservaId);
                    table.ForeignKey(
                        name: "FK_CancelacionReserva_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaExperiencia",
                schema: "res",
                columns: table => new
                {
                    EncuestaExperienciaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    CalificacionGeneral = table.Column<decimal>(type: "numeric", nullable: false),
                    Limpieza = table.Column<decimal>(type: "numeric", nullable: true),
                    Ubicacion = table.Column<decimal>(type: "numeric", nullable: true),
                    Servicio = table.Column<decimal>(type: "numeric", nullable: true),
                    RelacionCalidadPrecio = table.Column<decimal>(type: "numeric", nullable: true),
                    Comentarios = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaExperiencia", x => x.EncuestaExperienciaId);
                    table.ForeignKey(
                        name: "FK_EncuestaExperiencia_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncuestaExperiencia_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncuestaExperiencia_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                schema: "pag",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    MetodoPagoClienteId = table.Column<int>(type: "integer", nullable: true),
                    TipoPago = table.Column<string>(type: "text", nullable: false),
                    Monto = table.Column<decimal>(type: "numeric", nullable: false),
                    MonedaId = table.Column<int>(type: "integer", nullable: false),
                    EstadoPago = table.Column<string>(type: "text", nullable: false),
                    ReferenciaPago = table.Column<string>(type: "text", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.PagoId);
                    table.ForeignKey(
                        name: "FK_Pago_MetodoPagoCliente_MetodoPagoClienteId",
                        column: x => x.MetodoPagoClienteId,
                        principalSchema: "pag",
                        principalTable: "MetodoPagoCliente",
                        principalColumn: "MetodoPagoClienteId");
                    table.ForeignKey(
                        name: "FK_Pago_Moneda_MonedaId",
                        column: x => x.MonedaId,
                        principalSchema: "geo",
                        principalTable: "Moneda",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pago_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResenaCliente",
                schema: "res",
                columns: table => new
                {
                    ResenaClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnfitrionId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    Puntuacion = table.Column<decimal>(type: "numeric", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    EsNoShow = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResenaCliente", x => x.ResenaClienteId);
                    table.ForeignKey(
                        name: "FK_ResenaCliente_Anfitrion_AnfitrionId",
                        column: x => x.AnfitrionId,
                        principalSchema: "seg",
                        principalTable: "Anfitrion",
                        principalColumn: "AnfitrionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResenaCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResenaCliente_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResenaPropiedad",
                schema: "res",
                columns: table => new
                {
                    ResenaPropiedadId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    Puntuacion = table.Column<decimal>(type: "numeric", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResenaPropiedad", x => x.ResenaPropiedadId);
                    table.ForeignKey(
                        name: "FK_ResenaPropiedad_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResenaPropiedad_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResenaPropiedad_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaEstadoHistorial",
                schema: "res",
                columns: table => new
                {
                    ReservaEstadoHistorialId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    EstadoAnterior = table.Column<string>(type: "text", nullable: true),
                    EstadoNuevo = table.Column<string>(type: "text", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: true),
                    UsuarioAccion = table.Column<string>(type: "text", nullable: true),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaEstadoHistorial", x => x.ReservaEstadoHistorialId);
                    table.ForeignKey(
                        name: "FK_ReservaEstadoHistorial_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaHabitacionDetalle",
                schema: "res",
                columns: table => new
                {
                    ReservaHabitacionDetalleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    HabitacionId = table.Column<int>(type: "integer", nullable: false),
                    PrecioPorNoche = table.Column<decimal>(type: "numeric", nullable: false),
                    NumNoches = table.Column<int>(type: "integer", nullable: false),
                    SubTotalHabitacion = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaHabitacionDetalle", x => x.ReservaHabitacionDetalleId);
                    table.ForeignKey(
                        name: "FK_ReservaHabitacionDetalle_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalSchema: "alo",
                        principalTable: "Habitacion",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaHabitacionDetalle_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "res",
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                schema: "pag",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PagoId = table.Column<int>(type: "integer", nullable: false),
                    NumeroFactura = table.Column<string>(type: "text", nullable: false),
                    NombreFacturacion = table.Column<string>(type: "text", nullable: false),
                    DocumentoFacturacion = table.Column<string>(type: "text", nullable: false),
                    DireccionFacturacion = table.Column<string>(type: "text", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Impuesto = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoFactura = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "text", nullable: true),
                    EliminadoLogico = table.Column<bool>(type: "boolean", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.FacturaId);
                    table.ForeignKey(
                        name: "FK_Factura_Pago_PagoId",
                        column: x => x.PagoId,
                        principalSchema: "pag",
                        principalTable: "Pago",
                        principalColumn: "PagoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PagoEstadoHistorial",
                schema: "pag",
                columns: table => new
                {
                    PagoEstadoHistorialId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PagoId = table.Column<int>(type: "integer", nullable: false),
                    EstadoAnterior = table.Column<string>(type: "text", nullable: true),
                    EstadoNuevo = table.Column<string>(type: "text", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: true),
                    UsuarioAccion = table.Column<string>(type: "text", nullable: true),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoEstadoHistorial", x => x.PagoEstadoHistorialId);
                    table.ForeignKey(
                        name: "FK_PagoEstadoHistorial_Pago_PagoId",
                        column: x => x.PagoId,
                        principalSchema: "pag",
                        principalTable: "Pago",
                        principalColumn: "PagoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccesoUsuario_UsuarioId",
                schema: "aud",
                table: "AccesoUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertenciaCliente_ClienteId",
                schema: "res",
                table: "AdvertenciaCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertenciaCliente_ReservaId",
                schema: "res",
                table: "AdvertenciaCliente",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anfitrion_UsuarioId",
                schema: "seg",
                table: "Anfitrion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelacionReserva_ReservaId",
                schema: "res",
                table: "CancelacionReserva",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_PaisId",
                schema: "geo",
                table: "Ciudad",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CiudadId",
                schema: "seg",
                table: "Cliente",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_UsuarioId",
                schema: "seg",
                table: "Cliente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DisponibilidadHabitacion_HabitacionId",
                schema: "alo",
                table: "DisponibilidadHabitacion",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaExperiencia_ClienteId",
                schema: "res",
                table: "EncuestaExperiencia",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaExperiencia_PropiedadId",
                schema: "res",
                table: "EncuestaExperiencia",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaExperiencia_ReservaId",
                schema: "res",
                table: "EncuestaExperiencia",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorAplicacion_UsuarioId",
                schema: "aud",
                table: "ErrorAplicacion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_PagoId",
                schema: "pag",
                table: "Factura",
                column: "PagoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_PropiedadId",
                schema: "alo",
                table: "Habitacion",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitacionFoto_HabitacionId",
                schema: "alo",
                table: "HabitacionFoto",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodoPagoCliente_ClienteId",
                schema: "pag",
                table: "MetodoPagoCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_UsuarioId",
                schema: "seg",
                table: "Notificacion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_MetodoPagoClienteId",
                schema: "pag",
                table: "Pago",
                column: "MetodoPagoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_MonedaId",
                schema: "pag",
                table: "Pago",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ReservaId",
                schema: "pag",
                table: "Pago",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoEstadoHistorial_PagoId",
                schema: "pag",
                table: "PagoEstadoHistorial",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_PaisMoneda_MonedaId",
                schema: "geo",
                table: "PaisMoneda",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_AnfitrionId",
                schema: "alo",
                table: "Propiedad",
                column: "AnfitrionId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_CiudadId",
                schema: "alo",
                table: "Propiedad",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_TipoAlojamientoId",
                schema: "alo",
                table: "Propiedad",
                column: "TipoAlojamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadFoto_PropiedadId",
                schema: "alo",
                table: "PropiedadFoto",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadPolitica_PropiedadId",
                schema: "alo",
                table: "PropiedadPolitica",
                column: "PropiedadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadServicio_ServicioId",
                schema: "alo",
                table: "PropiedadServicio",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ResenaCliente_AnfitrionId",
                schema: "res",
                table: "ResenaCliente",
                column: "AnfitrionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResenaCliente_ClienteId",
                schema: "res",
                table: "ResenaCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ResenaCliente_ReservaId",
                schema: "res",
                table: "ResenaCliente",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResenaPropiedad_ClienteId",
                schema: "res",
                table: "ResenaPropiedad",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ResenaPropiedad_PropiedadId",
                schema: "res",
                table: "ResenaPropiedad",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_ResenaPropiedad_ReservaId",
                schema: "res",
                table: "ResenaPropiedad",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteId",
                schema: "res",
                table: "Reserva",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_MonedaId",
                schema: "res",
                table: "Reserva",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_PropiedadId",
                schema: "res",
                table: "Reserva",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaEstadoHistorial_ReservaId",
                schema: "res",
                table: "ReservaEstadoHistorial",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaHabitacionDetalle_HabitacionId",
                schema: "res",
                table: "ReservaHabitacionDetalle",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaHabitacionDetalle_ReservaId",
                schema: "res",
                table: "ReservaHabitacionDetalle",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaHabitacion_HabitacionId",
                schema: "alo",
                table: "TarifaHabitacion",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaHabitacion_MonedaId",
                schema: "alo",
                table: "TarifaHabitacion",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_TasaCambio_MonedaDestinoId",
                schema: "geo",
                table: "TasaCambio",
                column: "MonedaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_TasaCambio_MonedaOrigenId",
                schema: "geo",
                table: "TasaCambio",
                column: "MonedaOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenVerificacion_UsuarioId",
                schema: "seg",
                table: "TokenVerificacion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                schema: "seg",
                table: "UsuarioRol",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesoUsuario",
                schema: "aud");

            migrationBuilder.DropTable(
                name: "AdvertenciaCliente",
                schema: "res");

            migrationBuilder.DropTable(
                name: "AuditoriaGeneral",
                schema: "aud");

            migrationBuilder.DropTable(
                name: "CancelacionReserva",
                schema: "res");

            migrationBuilder.DropTable(
                name: "DisponibilidadHabitacion",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "EncuestaExperiencia",
                schema: "res");

            migrationBuilder.DropTable(
                name: "ErrorAplicacion",
                schema: "aud");

            migrationBuilder.DropTable(
                name: "Factura",
                schema: "pag");

            migrationBuilder.DropTable(
                name: "HabitacionFoto",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Notificacion",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "PagoEstadoHistorial",
                schema: "pag");

            migrationBuilder.DropTable(
                name: "PaisMoneda",
                schema: "geo");

            migrationBuilder.DropTable(
                name: "PropiedadFoto",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "PropiedadPolitica",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "PropiedadServicio",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "ResenaCliente",
                schema: "res");

            migrationBuilder.DropTable(
                name: "ResenaPropiedad",
                schema: "res");

            migrationBuilder.DropTable(
                name: "ReservaEstadoHistorial",
                schema: "res");

            migrationBuilder.DropTable(
                name: "ReservaHabitacionDetalle",
                schema: "res");

            migrationBuilder.DropTable(
                name: "TarifaHabitacion",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "TasaCambio",
                schema: "geo");

            migrationBuilder.DropTable(
                name: "TokenVerificacion",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "UsuarioRol",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "Pago",
                schema: "pag");

            migrationBuilder.DropTable(
                name: "Servicio",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Habitacion",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "MetodoPagoCliente",
                schema: "pag");

            migrationBuilder.DropTable(
                name: "Reserva",
                schema: "res");

            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "Moneda",
                schema: "geo");

            migrationBuilder.DropTable(
                name: "Propiedad",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Anfitrion",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "Ciudad",
                schema: "geo");

            migrationBuilder.DropTable(
                name: "TipoAlojamiento",
                schema: "alo");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "seg");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "geo");

            migrationBuilder.CreateTable(
                name: "Instalaciones",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propiedades",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdmiteMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    Calificacion = table.Column<double>(type: "double precision", nullable: false),
                    Capacidad = table.Column<int>(type: "integer", nullable: false),
                    Ciudad = table.Column<string>(type: "text", nullable: false),
                    ColaboradorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    HoraCheckIn = table.Column<string>(type: "text", nullable: false),
                    HoraCheckOut = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<string>(type: "text", nullable: false),
                    Longitud = table.Column<string>(type: "text", nullable: false),
                    Moneda = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    PrecioPorNoche = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TieneParking = table.Column<bool>(type: "boolean", nullable: false),
                    TienePiscina = table.Column<bool>(type: "boolean", nullable: false),
                    TieneSpa = table.Column<bool>(type: "boolean", nullable: false),
                    TieneWifi = table.Column<bool>(type: "boolean", nullable: false),
                    TipoAlojamiento = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertenciasNoShow = table.Column<int>(type: "integer", nullable: false),
                    Calificacion = table.Column<double>(type: "double precision", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Domicilio = table.Column<string>(type: "text", nullable: false),
                    EsColaborador = table.Column<bool>(type: "boolean", nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    TokenTarjeta = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    CapacidadAdultos = table.Column<int>(type: "integer", nullable: false),
                    CapacidadNinos = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Disponibilidad = table.Column<bool>(type: "boolean", nullable: false),
                    IncluyeCena = table.Column<bool>(type: "boolean", nullable: false),
                    IncluyeDesayuno = table.Column<bool>(type: "boolean", nullable: false),
                    NumeroBanos = table.Column<int>(type: "integer", nullable: false),
                    NumeroDormitorios = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TieneAireAcondicionado = table.Column<bool>(type: "boolean", nullable: false),
                    TieneCocina = table.Column<bool>(type: "boolean", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habitaciones_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagenes",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagenes_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadInstalacion",
                schema: "alo",
                columns: table => new
                {
                    InstalacionesId = table.Column<int>(type: "integer", nullable: false),
                    PropiedadesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadInstalacion", x => new { x.InstalacionesId, x.PropiedadesId });
                    table.ForeignKey(
                        name: "FK_PropiedadInstalacion_Instalaciones_InstalacionesId",
                        column: x => x.InstalacionesId,
                        principalSchema: "alo",
                        principalTable: "Instalaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropiedadInstalacion_Propiedades_PropiedadesId",
                        column: x => x.PropiedadesId,
                        principalSchema: "alo",
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puntos",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    PuntosAcumulados = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Puntos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "alo",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitacionId = table.Column<int>(type: "integer", nullable: false),
                    CantidadAdultos = table.Column<int>(type: "integer", nullable: false),
                    CantidadNiños = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    CorreoVerificado = table.Column<bool>(type: "boolean", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    FechaCheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCheckOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LlevaMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    MetodoPago = table.Column<int>(type: "integer", nullable: false),
                    Pagado = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalSchema: "alo",
                        principalTable: "Habitaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resenas",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    Calificacion = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resenas_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalSchema: "alo",
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_PropiedadId",
                schema: "alo",
                table: "Habitaciones",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagenes_PropiedadId",
                schema: "alo",
                table: "Imagenes",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadInstalacion_PropiedadesId",
                schema: "alo",
                table: "PropiedadInstalacion",
                column: "PropiedadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Puntos_UsuarioId",
                schema: "alo",
                table: "Puntos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Resenas_ReservaId",
                schema: "alo",
                table: "Resenas",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HabitacionId",
                schema: "alo",
                table: "Reservas",
                column: "HabitacionId");
        }
    }
}
