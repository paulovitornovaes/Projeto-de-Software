using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iduff.Migrations
{
    /// <inheritdoc />
    public partial class ch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CargaHorariaId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "organizacoes",
                table: "CargaHoraria",
                newName: "organizarPalestras");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CargaHorariaId",
                table: "AspNetUsers",
                column: "CargaHorariaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CargaHorariaId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "organizarPalestras",
                table: "CargaHoraria",
                newName: "organizacoes");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CargaHorariaId",
                table: "AspNetUsers",
                column: "CargaHorariaId");
        }
    }
}
