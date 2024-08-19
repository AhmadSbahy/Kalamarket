using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalamarket.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class editorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UO_Id",
                table: "UserOrders",
                newName: "UserOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserOrderId",
                table: "UserOrders",
                newName: "UO_Id");
        }
    }
}
