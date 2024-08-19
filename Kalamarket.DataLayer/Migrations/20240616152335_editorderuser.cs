using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalamarket.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class editorderuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_OrderStatus_OrderStatus",
                table: "UserOrders");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "UserOrders",
                newName: "OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrders_OrderStatus",
                table: "UserOrders",
                newName: "IX_UserOrders_OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_OrderStatus_OrderStatusId",
                table: "UserOrders",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_OrderStatus_OrderStatusId",
                table: "UserOrders");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "UserOrders",
                newName: "OrderStatus");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrders_OrderStatusId",
                table: "UserOrders",
                newName: "IX_UserOrders_OrderStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_OrderStatus_OrderStatus",
                table: "UserOrders",
                column: "OrderStatus",
                principalTable: "OrderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
