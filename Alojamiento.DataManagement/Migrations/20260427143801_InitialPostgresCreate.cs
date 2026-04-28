using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Alojamiento.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "alo");

            migrationBuilder.CreateTable(
                name: "Instalaciones",
                schema: "alo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Ciudad = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<string>(type: "text", nullable: false),
                    Longitud = table.Column<string>(type: "text", nullable: false),
                    TipoAlojamiento = table.Column<int>(type: "integer", nullable: false),
                    Capacidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioPorNoche = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Moneda = table.Column<string>(type: "text", nullable: false),
                    TieneWifi = table.Column<bool>(type: "boolean", nullable: false),
                    AdmiteMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    TienePiscina = table.Column<bool>(type: "boolean", nullable: false),
                    TieneParking = table.Column<bool>(type: "boolean", nullable: false),
                    TieneSpa = table.Column<bool>(type: "boolean", nullable: false),
                    HoraCheckIn = table.Column<string>(type: "text", nullable: false),
                    HoraCheckOut = table.Column<string>(type: "text", nullable: false),
                    Calificacion = table.Column<double>(type: "double precision", nullable: false),
                    ColaboradorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "text", nullable: false),
                    Domicilio = table.Column<string>(type: "text", nullable: false),
                    TokenTarjeta = table.Column<string>(type: "text", nullable: false),
                    Calificacion = table.Column<double>(type: "double precision", nullable: false),
                    AdvertenciasNoShow = table.Column<int>(type: "integer", nullable: false),
                    EsColaborador = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Disponibilidad = table.Column<bool>(type: "boolean", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    CapacidadAdultos = table.Column<int>(type: "integer", nullable: false),
                    CapacidadNinos = table.Column<int>(type: "integer", nullable: false),
                    NumeroBanos = table.Column<int>(type: "integer", nullable: false),
                    NumeroDormitorios = table.Column<int>(type: "integer", nullable: false),
                    TieneCocina = table.Column<bool>(type: "boolean", nullable: false),
                    TieneAireAcondicionado = table.Column<bool>(type: "boolean", nullable: false),
                    IncluyeDesayuno = table.Column<bool>(type: "boolean", nullable: false),
                    IncluyeCena = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    Url = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    PuntosAcumulados = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    FechaCheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCheckOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CantidadAdultos = table.Column<int>(type: "integer", nullable: false),
                    CantidadNiños = table.Column<int>(type: "integer", nullable: false),
                    LlevaMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    MetodoPago = table.Column<int>(type: "integer", nullable: false),
                    CorreoVerificado = table.Column<bool>(type: "boolean", nullable: false),
                    Pagado = table.Column<bool>(type: "boolean", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
