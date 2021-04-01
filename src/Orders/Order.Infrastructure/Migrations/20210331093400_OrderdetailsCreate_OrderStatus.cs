using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.Infrastructure.Migrations
{
    public partial class OrderdetailsCreate_OrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Components",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Components",
                newName: "Id");
        }
    }
}
