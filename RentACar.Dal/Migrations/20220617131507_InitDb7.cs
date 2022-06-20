using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Dal.Migrations
{
    public partial class InitDb7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsContinue",
                table: "Rents",
                newName: "IsFinished");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Rents",
                newName: "IsContinue");
        }
    }
}
