using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityAdjusted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaystackReference",
                table: "Payments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerEmail",
                table: "Invoices",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaystackReference",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BuyerEmail",
                table: "Invoices");
        }
    }
}
