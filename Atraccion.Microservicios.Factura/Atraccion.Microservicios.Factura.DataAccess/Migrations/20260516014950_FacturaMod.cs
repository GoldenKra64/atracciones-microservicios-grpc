using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atraccion.Microservicios.Factura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FacturaMod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CliId",
                table: "FACTURAS",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "FacCorreoReceptor",
                table: "FACTURAS",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacNombreReceptor",
                table: "FACTURAS",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacCorreoReceptor",
                table: "FACTURAS");

            migrationBuilder.DropColumn(
                name: "FacNombreReceptor",
                table: "FACTURAS");

            migrationBuilder.AlterColumn<int>(
                name: "CliId",
                table: "FACTURAS",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
