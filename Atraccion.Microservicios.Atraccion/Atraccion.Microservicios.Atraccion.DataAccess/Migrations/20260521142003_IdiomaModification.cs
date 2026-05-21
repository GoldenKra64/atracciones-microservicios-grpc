using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atraccion.Microservicios.Atraccion.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IdiomaModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_codigo",
                table: "IDIOMA",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_codigo",
                table: "IDIOMA");
        }
    }
}
