using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Atraccion.Microservicios.Atraccion.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_guid = table.Column<string>(type: "text", nullable: false),
                    cat_parent_id = table.Column<int>(type: "integer", nullable: true),
                    cat_nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cat_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.cat_id);
                    table.ForeignKey(
                        name: "FK_CATEGORIA_CATEGORIA_cat_parent_id",
                        column: x => x.cat_parent_id,
                        principalTable: "CATEGORIA",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DESTINO",
                columns: table => new
                {
                    des_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    des_guid = table.Column<string>(type: "text", nullable: false),
                    des_nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    des_pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    des_imagen_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    des_fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    des_usuario_ingreso = table.Column<string>(type: "text", nullable: false),
                    des_ip_ingreso = table.Column<string>(type: "text", nullable: false),
                    des_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    des_usuario_mod = table.Column<string>(type: "text", nullable: true),
                    des_ip_mod = table.Column<string>(type: "text", nullable: true),
                    des_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    des_usuario_eliminacion = table.Column<string>(type: "text", nullable: true),
                    des_ip_eliminacion = table.Column<string>(type: "text", nullable: true),
                    des_estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESTINO", x => x.des_id);
                });

            migrationBuilder.CreateTable(
                name: "IDIOMA",
                columns: table => new
                {
                    id_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_descripcion = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    id_estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDIOMA", x => x.id_id);
                });

            migrationBuilder.CreateTable(
                name: "INCLUYE",
                columns: table => new
                {
                    inc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inc_descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    inc_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCLUYE", x => x.inc_id);
                });

            migrationBuilder.CreateTable(
                name: "NOINCLUYE",
                columns: table => new
                {
                    noinc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inc_descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    inc_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOINCLUYE", x => x.noinc_id);
                });

            migrationBuilder.CreateTable(
                name: "TAG",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAG", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "ATRACCION",
                columns: table => new
                {
                    at_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    at_guid = table.Column<string>(type: "text", nullable: false),
                    des_id = table.Column<int>(type: "integer", nullable: false),
                    at_num_establecimiento = table.Column<string>(type: "text", nullable: true),
                    at_nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    at_descripcion = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    at_direccion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    at_duracion_minutos = table.Column<int>(type: "integer", nullable: true),
                    at_punto_encuentro = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    AtMoneda = table.Column<string>(type: "text", nullable: true),
                    at_precio_referencia = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    at_incluye_acompaniante = table.Column<bool>(type: "boolean", nullable: false),
                    at_incluye_transporte = table.Column<bool>(type: "boolean", nullable: false),
                    at_fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    at_usuario_ingreso = table.Column<string>(type: "text", nullable: false),
                    at_ip_ingreso = table.Column<string>(type: "text", nullable: false),
                    at_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    at_usuario_mod = table.Column<string>(type: "text", nullable: true),
                    at_ip_mod = table.Column<string>(type: "text", nullable: true),
                    at_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    at_usuario_eliminacion = table.Column<string>(type: "text", nullable: true),
                    at_ip_eliminacion = table.Column<string>(type: "text", nullable: true),
                    at_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATRACCION", x => x.at_id);
                    table.ForeignKey(
                        name: "FK_ATRACCION_DESTINO_des_id",
                        column: x => x.des_id,
                        principalTable: "DESTINO",
                        principalColumn: "des_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ATRACCION_INCLUYE",
                columns: table => new
                {
                    inc_id = table.Column<int>(type: "integer", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false),
                    AtraccionAtId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATRACCION_INCLUYE", x => new { x.inc_id, x.at_id });
                    table.ForeignKey(
                        name: "FK_ATRACCION_INCLUYE_ATRACCION_AtraccionAtId",
                        column: x => x.AtraccionAtId,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id");
                    table.ForeignKey(
                        name: "FK_ATRACCION_INCLUYE_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATRACCION_INCLUYE_INCLUYE_inc_id",
                        column: x => x.inc_id,
                        principalTable: "INCLUYE",
                        principalColumn: "inc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ATRACCION_NOINCLUYE",
                columns: table => new
                {
                    noinc_id = table.Column<int>(type: "integer", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false),
                    AtraccionAtId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATRACCION_NOINCLUYE", x => new { x.noinc_id, x.at_id });
                    table.ForeignKey(
                        name: "FK_ATRACCION_NOINCLUYE_ATRACCION_AtraccionAtId",
                        column: x => x.AtraccionAtId,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id");
                    table.ForeignKey(
                        name: "FK_ATRACCION_NOINCLUYE_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATRACCION_NOINCLUYE_NOINCLUYE_noinc_id",
                        column: x => x.noinc_id,
                        principalTable: "NOINCLUYE",
                        principalColumn: "noinc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIA_ATRACCION",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "integer", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_ATRACCION", x => new { x.cat_id, x.at_id });
                    table.ForeignKey(
                        name: "FK_CATEGORIA_ATRACCION_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CATEGORIA_ATRACCION_CATEGORIA_cat_id",
                        column: x => x.cat_id,
                        principalTable: "CATEGORIA",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HORARIO",
                columns: table => new
                {
                    hor_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hor_guid = table.Column<string>(type: "text", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false),
                    hor_fecha = table.Column<DateTime>(type: "date", nullable: false),
                    hor_hora_inicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    hor_hora_fin = table.Column<TimeSpan>(type: "time", nullable: true),
                    hor_cupos_disponibles = table.Column<int>(type: "integer", nullable: false),
                    hor_fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hor_usuario_ingreso = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    hor_ip_ingreso = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    hor_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    hor_usuario_mod = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    hor_ip_mod = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    hor_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    hor_usuario_eliminacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    hor_ip_eliminacion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    hor_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HORARIO", x => x.hor_id);
                    table.ForeignKey(
                        name: "FK_HORARIO_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDIOMA_ATRACCION",
                columns: table => new
                {
                    id_id = table.Column<int>(type: "integer", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false),
                    AtraccionAtId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDIOMA_ATRACCION", x => new { x.id_id, x.at_id });
                    table.ForeignKey(
                        name: "FK_IDIOMA_ATRACCION_ATRACCION_AtraccionAtId",
                        column: x => x.AtraccionAtId,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id");
                    table.ForeignKey(
                        name: "FK_IDIOMA_ATRACCION_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDIOMA_ATRACCION_IDIOMA_id_id",
                        column: x => x.id_id,
                        principalTable: "IDIOMA",
                        principalColumn: "id_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IMAGEN",
                columns: table => new
                {
                    img_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    img_guid = table.Column<string>(type: "text", nullable: false),
                    img_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    img_descripcion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    at_id = table.Column<int>(type: "integer", nullable: false),
                    img_fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    img_usuario_ingreso = table.Column<string>(type: "text", nullable: false),
                    img_ip_ingreso = table.Column<string>(type: "text", nullable: false),
                    img_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    img_usuario_mod = table.Column<string>(type: "text", nullable: true),
                    img_ip_mod = table.Column<string>(type: "text", nullable: true),
                    img_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    img_usuario_eliminacion = table.Column<string>(type: "text", nullable: true),
                    img_ip_eliminacion = table.Column<string>(type: "text", nullable: true),
                    img_estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGEN", x => x.img_id);
                    table.ForeignKey(
                        name: "FK_IMAGEN_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESENIA",
                columns: table => new
                {
                    rsn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rsn_guid = table.Column<string>(type: "text", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false),
                    cli_id = table.Column<int>(type: "integer", nullable: false),
                    rev_id = table.Column<int>(type: "integer", nullable: true),
                    rsn_rating = table.Column<int>(type: "integer", nullable: false),
                    rsn_comentario = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    rsn_fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rsn_usuario_creacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rsn_ip_creacion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    rsn_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rsn_usuario_mod = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rsn_ip_mod = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    rsn_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rsn_usuario_eliminacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rsn_ip_eliminacion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    rsn_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESENIA", x => x.rsn_id);
                    table.ForeignKey(
                        name: "FK_RESENIA_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAG_ATRACCION",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false),
                    at_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAG_ATRACCION", x => new { x.tag_id, x.at_id });
                    table.ForeignKey(
                        name: "FK_TAG_ATRACCION_ATRACCION_at_id",
                        column: x => x.at_id,
                        principalTable: "ATRACCION",
                        principalColumn: "at_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAG_ATRACCION_TAG_tag_id",
                        column: x => x.tag_id,
                        principalTable: "TAG",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TICKET",
                columns: table => new
                {
                    tck_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tck_guid = table.Column<string>(type: "text", nullable: false),
                    hor_id = table.Column<int>(type: "integer", nullable: false),
                    tck_titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    tck_precio = table.Column<decimal>(type: "numeric", nullable: false),
                    tck_tipo_participante = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    tck_fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tck_usuario_ingreso = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tck_ip_ingreso = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    tck_fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tck_usuario_mod = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tck_ip_mod = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    tck_fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tck_usuario_eliminacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tck_ip_eliminacion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    tck_estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICKET", x => x.tck_id);
                    table.ForeignKey(
                        name: "FK_TICKET_HORARIO_hor_id",
                        column: x => x.hor_id,
                        principalTable: "HORARIO",
                        principalColumn: "hor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATRACCION_des_id",
                table: "ATRACCION",
                column: "des_id");

            migrationBuilder.CreateIndex(
                name: "IX_ATRACCION_INCLUYE_at_id",
                table: "ATRACCION_INCLUYE",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_ATRACCION_INCLUYE_AtraccionAtId",
                table: "ATRACCION_INCLUYE",
                column: "AtraccionAtId");

            migrationBuilder.CreateIndex(
                name: "IX_ATRACCION_NOINCLUYE_at_id",
                table: "ATRACCION_NOINCLUYE",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_ATRACCION_NOINCLUYE_AtraccionAtId",
                table: "ATRACCION_NOINCLUYE",
                column: "AtraccionAtId");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_cat_parent_id",
                table: "CATEGORIA",
                column: "cat_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_ATRACCION_at_id",
                table: "CATEGORIA_ATRACCION",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_HORARIO_at_id",
                table: "HORARIO",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "UK_HORARIO_guid",
                table: "HORARIO",
                column: "hor_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IDIOMA_ATRACCION_at_id",
                table: "IDIOMA_ATRACCION",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_IDIOMA_ATRACCION_AtraccionAtId",
                table: "IDIOMA_ATRACCION",
                column: "AtraccionAtId");

            migrationBuilder.CreateIndex(
                name: "IX_IMAGEN_at_id",
                table: "IMAGEN",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_RESENIA_at_id",
                table: "RESENIA",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_TAG_ATRACCION_at_id",
                table: "TAG_ATRACCION",
                column: "at_id");

            migrationBuilder.CreateIndex(
                name: "IX_TICKET_hor_id",
                table: "TICKET",
                column: "hor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATRACCION_INCLUYE");

            migrationBuilder.DropTable(
                name: "ATRACCION_NOINCLUYE");

            migrationBuilder.DropTable(
                name: "CATEGORIA_ATRACCION");

            migrationBuilder.DropTable(
                name: "IDIOMA_ATRACCION");

            migrationBuilder.DropTable(
                name: "IMAGEN");

            migrationBuilder.DropTable(
                name: "RESENIA");

            migrationBuilder.DropTable(
                name: "TAG_ATRACCION");

            migrationBuilder.DropTable(
                name: "TICKET");

            migrationBuilder.DropTable(
                name: "INCLUYE");

            migrationBuilder.DropTable(
                name: "NOINCLUYE");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "IDIOMA");

            migrationBuilder.DropTable(
                name: "TAG");

            migrationBuilder.DropTable(
                name: "HORARIO");

            migrationBuilder.DropTable(
                name: "ATRACCION");

            migrationBuilder.DropTable(
                name: "DESTINO");
        }
    }
}
