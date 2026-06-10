using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringApp.Migrations
{
    /// <inheritdoc />
    public partial class RefactorItemHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemClients_Items_ItemId",
                table: "ItemClients");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_InventoryItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_InventoryItemId",
                table: "SerialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SerialNumberId",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameColumn(
                name: "InventoryItemId",
                table: "SerialNumbers",
                newName: "HardwareItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "Item",
                newName: "IX_Item_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Hardware" });

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_HardwareItemId",
                table: "SerialNumbers",
                column: "HardwareItemId",
                unique: true,
                filter: "[HardwareItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Categories_CategoryId",
                table: "Item",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemClients_Item_ItemId",
                table: "ItemClients",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Item_HardwareItemId",
                table: "SerialNumbers",
                column: "HardwareItemId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Categories_CategoryId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemClients_Item_ItemId",
                table: "ItemClients");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Item_HardwareItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_HardwareItemId",
                table: "SerialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameColumn(
                name: "HardwareItemId",
                table: "SerialNumbers",
                newName: "InventoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CategoryId",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_InventoryItemId",
                table: "SerialNumbers",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumberId",
                table: "Items",
                column: "SerialNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemClients_Items_ItemId",
                table: "ItemClients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

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
    }
}
