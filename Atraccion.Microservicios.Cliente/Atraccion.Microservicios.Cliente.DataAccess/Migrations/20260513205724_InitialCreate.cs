using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Atraccion.Microservicios.Cliente.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    cli_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cli_guid = table.Column<string>(type: "text", nullable: false),
                    usu_id = table.Column<int>(type: "integer", nullable: true),
                    cli_tipo_identificacion = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cli_numero_identificacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cli_nombres = table.Column<string>(type: "text", nullable: false),
                    cli_apellidos = table.Column<string>(type: "text", nullable: false),
                    cli_correo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cli_telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    cli_direccion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    cli_fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cli_usuario_ingreso = table.Column<string>(type: "text", nullable: false),
                    cli_ip_ingreso = table.Column<string>(type: "text", nullable: false),
                    cli_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cli_usuario_eliminacion = table.Column<string>(type: "text", nullable: true),
                    cli_ip_eliminacion = table.Column<string>(type: "text", nullable: true),
                    cli_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.cli_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTES");
        }
    }
}
