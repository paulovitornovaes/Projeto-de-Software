using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iduff.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificado",
                table: "Certificado");

            migrationBuilder.RenameTable(
                name: "Certificado",
                newName: "Certificados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificados",
                table: "Certificados",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EVENTO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PalestranteId = table.Column<string>(type: "TEXT", nullable: true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Local = table.Column<string>(type: "TEXT", nullable: false),
                    HorasComplementares = table.Column<long>(type: "INTEGER", nullable: false),
                    OrganizadorId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EVENTO_AspNetUsers_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EVENTO_AspNetUsers_PalestranteId",
                        column: x => x.PalestranteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EVENTO_OrganizadorId",
                table: "EVENTO",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_EVENTO_PalestranteId",
                table: "EVENTO",
                column: "PalestranteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EVENTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificados",
                table: "Certificados");

            migrationBuilder.RenameTable(
                name: "Certificados",
                newName: "Certificado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificado",
                table: "Certificado",
                column: "Id");
        }
    }
}
