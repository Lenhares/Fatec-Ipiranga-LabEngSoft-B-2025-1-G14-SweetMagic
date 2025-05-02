using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetMagic.Migrations
{
    /// <inheritdoc />
    public partial class Bolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    papelArroz = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bolos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagemFinal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    peso = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    coberturaId = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    criadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolos_Coberturas_coberturaId",
                        column: x => x.coberturaId,
                        principalTable: "Coberturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bolos_Users_criadorId",
                        column: x => x.criadorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Camadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ordem = table.Column<int>(type: "int", nullable: false),
                    saborMassa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saborRecheio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Camadas_Bolos_BoloId",
                        column: x => x.BoloId,
                        principalTable: "Bolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolos_coberturaId",
                table: "Bolos",
                column: "coberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolos_criadorId",
                table: "Bolos",
                column: "criadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Camadas_BoloId",
                table: "Camadas",
                column: "BoloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Camadas");

            migrationBuilder.DropTable(
                name: "Bolos");

            migrationBuilder.DropTable(
                name: "Coberturas");
        }
    }
}
