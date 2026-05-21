using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atraccion.Microservicios.Reserva.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddHorIdAndAtNombre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "at_nombre",
                table: "RESERVAS",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "hor_id",
                table: "RESERVAS",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "at_nombre",
                table: "RESERVAS");

            migrationBuilder.DropColumn(
                name: "hor_id",
                table: "RESERVAS");
        }
    }
}
