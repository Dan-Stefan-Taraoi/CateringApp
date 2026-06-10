using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "SerialNumbers",
                newName: "InventoryItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_InventoryItemId",
                table: "SerialNumbers",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumberId",
                table: "Items",
                column: "SerialNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId",
                table: "Items",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_InventoryItemId",
                table: "SerialNumbers",
                column: "InventoryItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_InventoryItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_InventoryItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Items_SerialNumberId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "InventoryItemId",
                table: "SerialNumbers",
                newName: "ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                unique: true,
                filter: "[ItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
