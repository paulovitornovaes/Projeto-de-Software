using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iduff.Migrations
{
    /// <inheritdoc />
    public partial class eventoNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AspNetUsers_OrganizadorId",
                table: "Evento");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AspNetUsers_PalestranteId",
                table: "Evento");

            migrationBuilder.AlterColumn<string>(
                name: "PalestranteId",
                table: "Evento",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizadorId",
                table: "Evento",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_AspNetUsers_OrganizadorId",
                table: "Evento",
                column: "OrganizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_AspNetUsers_PalestranteId",
                table: "Evento",
                column: "PalestranteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AspNetUsers_OrganizadorId",
                table: "Evento");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AspNetUsers_PalestranteId",
                table: "Evento");

            migrationBuilder.AlterColumn<string>(
                name: "PalestranteId",
                table: "Evento",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizadorId",
                table: "Evento",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_AspNetUsers_OrganizadorId",
                table: "Evento",
                column: "OrganizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_AspNetUsers_PalestranteId",
                table: "Evento",
                column: "PalestranteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
