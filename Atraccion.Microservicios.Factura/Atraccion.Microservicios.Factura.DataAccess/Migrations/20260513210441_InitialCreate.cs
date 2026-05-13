using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Atraccion.Microservicios.Factura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FACTURAS",
                columns: table => new
                {
                    fac_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fac_guid = table.Column<string>(type: "text", nullable: false),
                    rev_id = table.Column<int>(type: "integer", nullable: false),
                    fac_numero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fac_fecha_emision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fac_total = table.Column<decimal>(type: "numeric", nullable: false),
                    fac_observacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fac_origen_canal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fac_estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fac_usuario_ingreso = table.Column<string>(type: "text", nullable: true),
                    fac_ip_ingreso = table.Column<string>(type: "text", nullable: true),
                    fac_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fac_usuario_mod = table.Column<string>(type: "text", nullable: true),
                    fac_ip_mod = table.Column<string>(type: "text", nullable: true),
                    fac_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fac_usuario_eliminacion = table.Column<string>(type: "text", nullable: true),
                    fac_ip_eliminacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACTURAS", x => x.fac_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FACTURAS");
        }
    }
}
