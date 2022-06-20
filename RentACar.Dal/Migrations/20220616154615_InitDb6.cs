using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Dal.Migrations
{
    public partial class InitDb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsContinue",
                table: "Rents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContinue",
                table: "Rents");
        }
    }
}
