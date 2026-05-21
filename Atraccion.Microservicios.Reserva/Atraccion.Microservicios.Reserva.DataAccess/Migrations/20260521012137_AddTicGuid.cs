using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atraccion.Microservicios.Reserva.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTicGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tck_guid",
                table: "RESERVA_DETALLE",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tck_guid",
                table: "RESERVA_DETALLE");
        }
    }
}
