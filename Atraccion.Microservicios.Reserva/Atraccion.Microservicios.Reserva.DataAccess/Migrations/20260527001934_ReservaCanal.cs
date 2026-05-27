using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atraccion.Microservicios.Reserva.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReservaCanal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RevCanal",
                table: "RESERVAS",
                newName: "rev_canal");

            migrationBuilder.RenameColumn(
                name: "HorHoraInicio",
                table: "RESERVAS",
                newName: "hor_hora_inicio");

            migrationBuilder.RenameColumn(
                name: "HorHoraFin",
                table: "RESERVAS",
                newName: "hor_hora_fin");

            migrationBuilder.RenameColumn(
                name: "HorFecha",
                table: "RESERVAS",
                newName: "hor_fecha");

            migrationBuilder.AlterColumn<string>(
                name: "rev_canal",
                table: "RESERVAS",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.AddColumn<string>(
                name: "horario_guid",
                table: "RESERVAS",
                type: "text",
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

            migrationBuilder.DropColumn(
                name: "horario_guid",
                table: "RESERVAS");

            migrationBuilder.RenameColumn(
                name: "rev_canal",
                table: "RESERVAS",
                newName: "RevCanal");

            migrationBuilder.RenameColumn(
                name: "hor_hora_inicio",
                table: "RESERVAS",
                newName: "HorHoraInicio");

            migrationBuilder.RenameColumn(
                name: "hor_hora_fin",
                table: "RESERVAS",
                newName: "HorHoraFin");

            migrationBuilder.RenameColumn(
                name: "hor_fecha",
                table: "RESERVAS",
                newName: "HorFecha");

            migrationBuilder.AlterColumn<string>(
                name: "RevCanal",
                table: "RESERVAS",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
