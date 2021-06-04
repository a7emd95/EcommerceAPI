using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class orderMg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalOrderPrice",
                table: "Order",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "OrderDateTime",
                table: "Order",
                newName: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order",
                newName: "TotalOrderPrice");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Order",
                newName: "OrderDateTime");
        }
    }
}
