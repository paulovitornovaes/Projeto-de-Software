using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iduff.Migrations
{
    /// <inheritdoc />
    public partial class certificado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

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

            migrationBuilder.AddColumn<string>(
                name: "AlunoId",
                table: "Certificados",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "EventoId",
                table: "Certificados",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evento",
                table: "Evento",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_AlunoId",
                table: "Certificados",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_EventoId",
                table: "Certificados",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_AspNetUsers_AlunoId",
                table: "Certificados",
                column: "AlunoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_Evento_EventoId",
                table: "Certificados",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_AspNetUsers_AlunoId",
                table: "Certificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_Evento_EventoId",
                table: "Certificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AspNetUsers_OrganizadorId",
                table: "Evento");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AspNetUsers_PalestranteId",
                table: "Evento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evento",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Certificados_AlunoId",
                table: "Certificados");

            migrationBuilder.DropIndex(
                name: "IX_Certificados_EventoId",
                table: "Certificados");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Certificados");

            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "Certificados");
            
        }
    }
}
