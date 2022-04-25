using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacunas_sis.Data.Migrations
{
    public partial class porfa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id_direccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalle_direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id_direccion);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    Id_vacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_vacuna = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Abreviatura_vacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dosis_aplicables = table.Column<int>(type: "int", nullable: false),
                    Edad_aplicable = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.Id_vacuna);
                });

            migrationBuilder.CreateTable(
                name: "Informacion_nino",
                columns: table => new
                {
                    Id_nino = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_identidad = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nombre_nino = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nombre_responsabe = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Fecha_nacimineto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad_cap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_contacto = table.Column<int>(type: "int", nullable: false),
                    Id_direccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informacion_nino", x => x.Id_nino);
                    table.ForeignKey(
                        name: "FK_Informacion_nino_Contacto_Id_contacto",
                        column: x => x.Id_contacto,
                        principalTable: "Contacto",
                        principalColumn: "Id_contacto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Informacion_nino_Direccion_Id_direccion",
                        column: x => x.Id_direccion,
                        principalTable: "Direccion",
                        principalColumn: "Id_direccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Vacuna",
                columns: table => new
                {
                    Id_detalle_vacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_aplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero_dosis_aplicada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero_dosis_restantes = table.Column<int>(type: "int", nullable: false),
                    Fecha_proxima_dosis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_vacuna = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Vacuna", x => x.Id_detalle_vacuna);
                    table.ForeignKey(
                        name: "FK_Detalle_Vacuna_Vacunas_Id_vacuna",
                        column: x => x.Id_vacuna,
                        principalTable: "Vacunas",
                        principalColumn: "Id_vacuna",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registro",
                columns: table => new
                {
                    Id_registro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_historia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_nino = table.Column<int>(type: "int", nullable: false),
                    Id_detalle_vacuna = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro", x => x.Id_registro);
                    table.ForeignKey(
                        name: "FK_Registro_Detalle_Vacuna_Id_detalle_vacuna",
                        column: x => x.Id_detalle_vacuna,
                        principalTable: "Detalle_Vacuna",
                        principalColumn: "Id_detalle_vacuna",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registro_Informacion_nino_Id_nino",
                        column: x => x.Id_nino,
                        principalTable: "Informacion_nino",
                        principalColumn: "Id_nino",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Vacuna_Id_vacuna",
                table: "Detalle_Vacuna",
                column: "Id_vacuna");

            migrationBuilder.CreateIndex(
                name: "IX_Informacion_nino_Id_contacto",
                table: "Informacion_nino",
                column: "Id_contacto");

            migrationBuilder.CreateIndex(
                name: "IX_Informacion_nino_Id_direccion",
                table: "Informacion_nino",
                column: "Id_direccion");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_Id_detalle_vacuna",
                table: "Registro",
                column: "Id_detalle_vacuna");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_Id_nino",
                table: "Registro",
                column: "Id_nino");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registro");

            migrationBuilder.DropTable(
                name: "Detalle_Vacuna");

            migrationBuilder.DropTable(
                name: "Informacion_nino");

            migrationBuilder.DropTable(
                name: "Vacunas");

            migrationBuilder.DropTable(
                name: "Direccion");
        }
    }
}
