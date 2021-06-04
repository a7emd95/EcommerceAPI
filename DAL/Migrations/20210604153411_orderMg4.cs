using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class orderMg4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Order_UserID",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_UserID",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "OrderProduct");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderID",
                table: "OrderProduct",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Order_OrderID",
                table: "OrderProduct",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Order_OrderID",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_OrderID",
                table: "OrderProduct");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "OrderProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_UserID",
                table: "OrderProduct",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Order_UserID",
                table: "OrderProduct",
                column: "UserID",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
