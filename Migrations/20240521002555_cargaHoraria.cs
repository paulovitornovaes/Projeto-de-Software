using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iduff.Migrations
{
    /// <inheritdoc />
    public partial class cargaHoraria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CargaHorariaId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CargaHoraria",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    presencaPalestras = table.Column<int>(type: "INTEGER", nullable: false),
                    organizacoes = table.Column<int>(type: "INTEGER", nullable: false),
                    ministrarPalestras = table.Column<int>(type: "INTEGER", nullable: false),
                    estagio = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargaHoraria", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CargaHorariaId",
                table: "AspNetUsers",
                column: "CargaHorariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CargaHoraria_CargaHorariaId",
                table: "AspNetUsers",
                column: "CargaHorariaId",
                principalTable: "CargaHoraria",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CargaHoraria_CargaHorariaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CargaHoraria");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CargaHorariaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CargaHorariaId",
                table: "AspNetUsers");
        }
    }
}
