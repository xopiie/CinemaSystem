using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uzunova_Nadica_1002387434_DSR_2021.Migrations.db
{
    public partial class initialcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dvorane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stevilo_sedezev = table.Column<int>(type: "int", nullable: false),
                    ThreeD = table.Column<bool>(type: "bit", nullable: false),
                    Pot_slike = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dvorane", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Jezik = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DatumIzida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trajanje = table.Column<int>(type: "int", nullable: false),
                    Ocena = table.Column<double>(type: "float", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pot_slike = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdSpored = table.Column<int>(type: "int", nullable: false),
                    SteviloSedezev = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spored",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaslovFilma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NazivDvorane = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumCas = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spored", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dvorane");

            migrationBuilder.DropTable(
                name: "Filmi");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "Spored");
        }
    }
}
