using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Atraccion.Microservicios.Reserva.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RESERVAS",
                columns: table => new
                {
                    rev_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rev_guid = table.Column<string>(type: "text", nullable: false),
                    rev_codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cli_id = table.Column<int>(type: "integer", nullable: true),
                    rev_fecha_reserva_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rev_subtotal = table.Column<double>(type: "double precision", nullable: false),
                    rev_valor_iva = table.Column<double>(type: "double precision", nullable: false),
                    rev_total = table.Column<double>(type: "double precision", nullable: false),
                    RevCanal = table.Column<string>(type: "text", nullable: false),
                    HorFecha = table.Column<string>(type: "text", nullable: true),
                    HorHoraInicio = table.Column<string>(type: "text", nullable: true),
                    HorHoraFin = table.Column<string>(type: "text", nullable: true),
                    rev_usuario_ingreso = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rev_ip_ingreso = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    rev_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rev_usuario_mod = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rev_ip_mod = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    rev_fecha_cancelacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rev_usuario_cancelacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rev_ip_cancelacion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    rev_motivo_cancelacion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    rev_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVAS", x => x.rev_id);
                });

            migrationBuilder.CreateTable(
                name: "RESERVA_DETALLE",
                columns: table => new
                {
                    rev_id = table.Column<int>(type: "integer", nullable: false),
                    tck_id = table.Column<int>(type: "integer", nullable: false),
                    rdet_guid = table.Column<string>(type: "text", nullable: false),
                    rdet_tipo_participante = table.Column<string>(type: "text", nullable: true),
                    TicTitulo = table.Column<string>(type: "text", nullable: false),
                    TicCantidad = table.Column<int>(type: "integer", nullable: false),
                    rdet_precio_unit = table.Column<double>(type: "double precision", nullable: false),
                    rdet_subtotal = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVA_DETALLE", x => new { x.rev_id, x.tck_id });
                    table.ForeignKey(
                        name: "FK_RESERVA_DETALLE_RESERVAS_rev_id",
                        column: x => x.rev_id,
                        principalTable: "RESERVAS",
                        principalColumn: "rev_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RESERVA_DETALLE");

            migrationBuilder.DropTable(
                name: "RESERVAS");
        }
    }
}
