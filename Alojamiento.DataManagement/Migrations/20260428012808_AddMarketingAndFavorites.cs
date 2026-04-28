using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Alojamiento.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketingAndFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fav");

            migrationBuilder.EnsureSchema(
                name: "mkt");

            migrationBuilder.CreateTable(
                name: "ClienteFavoritoPropiedad",
                schema: "fav",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    FechaAgregado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteFavoritoPropiedad", x => new { x.ClienteId, x.PropiedadId });
                    table.ForeignKey(
                        name: "FK_ClienteFavoritoPropiedad_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteFavoritoPropiedad_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                schema: "mkt",
                columns: table => new
                {
                    PromocionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    PorcentajeDescuento = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_Promocion", x => x.PromocionId);
                    table.ForeignKey(
                        name: "FK_Promocion_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalSchema: "alo",
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PuntosAnfitrion",
                schema: "mkt",
                columns: table => new
                {
                    PuntosAnfitrionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnfitrionId = table.Column<int>(type: "integer", nullable: false),
                    PuntosAcumulados = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_PuntosAnfitrion", x => x.PuntosAnfitrionId);
                    table.ForeignKey(
                        name: "FK_PuntosAnfitrion_Anfitrion_AnfitrionId",
                        column: x => x.AnfitrionId,
                        principalSchema: "seg",
                        principalTable: "Anfitrion",
                        principalColumn: "AnfitrionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PuntosCliente",
                schema: "mkt",
                columns: table => new
                {
                    PuntosClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    PuntosAcumulados = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_PuntosCliente", x => x.PuntosClienteId);
                    table.ForeignKey(
                        name: "FK_PuntosCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "seg",
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialPuntosAnfitrion",
                schema: "mkt",
                columns: table => new
                {
                    HistorialPuntosAnfitrionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PuntosAnfitrionId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    TipoTransaccion = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaTransaccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialPuntosAnfitrion", x => x.HistorialPuntosAnfitrionId);
                    table.ForeignKey(
                        name: "FK_HistorialPuntosAnfitrion_PuntosAnfitrion_PuntosAnfitrionId",
                        column: x => x.PuntosAnfitrionId,
                        principalSchema: "mkt",
                        principalTable: "PuntosAnfitrion",
                        principalColumn: "PuntosAnfitrionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialPuntosCliente",
                schema: "mkt",
                columns: table => new
                {
                    HistorialPuntosClienteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PuntosClienteId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    TipoTransaccion = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaTransaccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialPuntosCliente", x => x.HistorialPuntosClienteId);
                    table.ForeignKey(
                        name: "FK_HistorialPuntosCliente_PuntosCliente_PuntosClienteId",
                        column: x => x.PuntosClienteId,
                        principalSchema: "mkt",
                        principalTable: "PuntosCliente",
                        principalColumn: "PuntosClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteFavoritoPropiedad_PropiedadId",
                schema: "fav",
                table: "ClienteFavoritoPropiedad",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPuntosAnfitrion_PuntosAnfitrionId",
                schema: "mkt",
                table: "HistorialPuntosAnfitrion",
                column: "PuntosAnfitrionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPuntosCliente_PuntosClienteId",
                schema: "mkt",
                table: "HistorialPuntosCliente",
                column: "PuntosClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_PropiedadId",
                schema: "mkt",
                table: "Promocion",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_PuntosAnfitrion_AnfitrionId",
                schema: "mkt",
                table: "PuntosAnfitrion",
                column: "AnfitrionId");

            migrationBuilder.CreateIndex(
                name: "IX_PuntosCliente_ClienteId",
                schema: "mkt",
                table: "PuntosCliente",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteFavoritoPropiedad",
                schema: "fav");

            migrationBuilder.DropTable(
                name: "HistorialPuntosAnfitrion",
                schema: "mkt");

            migrationBuilder.DropTable(
                name: "HistorialPuntosCliente",
                schema: "mkt");

            migrationBuilder.DropTable(
                name: "Promocion",
                schema: "mkt");

            migrationBuilder.DropTable(
                name: "PuntosAnfitrion",
                schema: "mkt");

            migrationBuilder.DropTable(
                name: "PuntosCliente",
                schema: "mkt");
        }
    }
}
