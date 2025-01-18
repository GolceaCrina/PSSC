using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSSC_Proiect.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comenzi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Adresa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comenzi", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Cod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nume = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pret = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Stoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produse", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LiniiComanda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ComandaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Cantitate = table.Column<int>(type: "int", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiniiComanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiniiComanda_Comenzi_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comenzi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiniiComanda_Produse_ProdusId",
                        column: x => x.ProdusId,
                        principalTable: "Produse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LiniiComanda_ComandaId",
                table: "LiniiComanda",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_LiniiComanda_ProdusId",
                table: "LiniiComanda",
                column: "ProdusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiniiComanda");

            migrationBuilder.DropTable(
                name: "Comenzi");

            migrationBuilder.DropTable(
                name: "Produse");
        }
    }
}
