using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeventeenthModule.Migrations
{
    public partial class MigrationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Migrate",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Migrate",
                table: "Orders");
        }
    }
}
