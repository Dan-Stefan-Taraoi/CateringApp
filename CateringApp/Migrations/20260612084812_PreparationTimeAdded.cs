using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringApp.Migrations
{
    /// <inheritdoc />
    public partial class PreparationTimeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PreparationTime",
                table: "MenuItems",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "MenuItems");
        }
    }
}
