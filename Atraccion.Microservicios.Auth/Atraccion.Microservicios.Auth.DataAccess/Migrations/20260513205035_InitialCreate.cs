using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Atraccion.Microservicios.Auth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol_guid = table.Column<string>(type: "text", nullable: false),
                    rol_descripcion = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    rol_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    usu_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usu_guid = table.Column<string>(type: "text", nullable: false),
                    usu_login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usu_password_hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    usu_fecha_registro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usu_usuario_registro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usu_ip_registro = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    usu_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usu_usuario_mod = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    usu_ip_mod = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    usu_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usu_usuario_eliminacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    usu_ip_eliminacion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    usu_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.usu_id);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOXROL",
                columns: table => new
                {
                    usu_rol_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usu_id = table.Column<int>(type: "integer", nullable: false),
                    rol_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOXROL", x => x.usu_rol_id);
                    table.ForeignKey(
                        name: "FK_USUARIOXROL_ROL_rol_id",
                        column: x => x.rol_id,
                        principalTable: "ROL",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USUARIOXROL_USUARIO_usu_id",
                        column: x => x.usu_id,
                        principalTable: "USUARIO",
                        principalColumn: "usu_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOXROL_rol_id",
                table: "USUARIOXROL",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOXROL_usu_id",
                table: "USUARIOXROL",
                column: "usu_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIOXROL");

            migrationBuilder.DropTable(
                name: "ROL");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
