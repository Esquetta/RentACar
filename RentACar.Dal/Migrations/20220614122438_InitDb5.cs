using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Dal.Migrations
{
    public partial class InitDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_CustomerId1",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_CustomerId1",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Rents");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Rents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CustomerId",
                table: "Rents",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_CustomerId",
                table: "Rents",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_CustomerId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_CustomerId",
                table: "Rents");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "Rents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CustomerId1",
                table: "Rents",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_CustomerId1",
                table: "Rents",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
