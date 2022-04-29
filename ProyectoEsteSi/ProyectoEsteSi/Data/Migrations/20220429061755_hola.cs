using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoEsteSi.Data.Migrations
{
    public partial class hola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id_direccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalle_direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id_direccion);
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
                name: "Pacientes",
                columns: table => new
                {
                    Id_nino = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_identidad = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nombre_nino = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nombre_responsabe = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Fecha_nacimineto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad_cap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<int>(type: "int", maxLength: 60, nullable: false),
                    Id_direccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id_nino);
                    table.ForeignKey(
                        name: "FK_Pacientes_Direcciones_Id_direccion",
                        column: x => x.Id_direccion,
                        principalTable: "Direcciones",
                        principalColumn: "Id_direccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dosis",
                columns: table => new
                {
                    IdDosis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_nino = table.Column<int>(type: "int", nullable: false),
                    Id_vacuna = table.Column<int>(type: "int", nullable: false),
                    restante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosis", x => x.IdDosis);
                    table.ForeignKey(
                        name: "FK_Dosis_Pacientes_Id_nino",
                        column: x => x.Id_nino,
                        principalTable: "Pacientes",
                        principalColumn: "Id_nino",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dosis_Vacunas_Id_vacuna",
                        column: x => x.Id_vacuna,
                        principalTable: "Vacunas",
                        principalColumn: "Id_vacuna",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDosis = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_proxima = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Citas_Dosis_IdDosis",
                        column: x => x.IdDosis,
                        principalTable: "Dosis",
                        principalColumn: "IdDosis",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdDosis",
                table: "Citas",
                column: "IdDosis");

            migrationBuilder.CreateIndex(
                name: "IX_Dosis_Id_nino",
                table: "Dosis",
                column: "Id_nino");

            migrationBuilder.CreateIndex(
                name: "IX_Dosis_Id_vacuna",
                table: "Dosis",
                column: "Id_vacuna");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Id_direccion",
                table: "Pacientes",
                column: "Id_direccion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Dosis");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Vacunas");

            migrationBuilder.DropTable(
                name: "Direcciones");
        }
    }
}
