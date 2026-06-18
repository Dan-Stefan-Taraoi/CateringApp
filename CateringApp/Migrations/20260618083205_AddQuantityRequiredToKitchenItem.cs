using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringApp.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityRequiredToKitchenItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "QuantityRequired",
                table: "KitchenItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityRequired",
                table: "KitchenItems");
        }
    }
}
