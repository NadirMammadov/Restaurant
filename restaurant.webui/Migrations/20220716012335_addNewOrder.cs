using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurant.webui.Migrations
{
    public partial class addNewOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NewOrder",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewOrder",
                table: "AspNetUsers");
        }
    }
}
