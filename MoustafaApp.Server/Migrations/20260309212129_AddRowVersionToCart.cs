using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoustafaApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Carts",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 21, 21, 24, 344, DateTimeKind.Utc).AddTicks(2571));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 21, 21, 24, 344, DateTimeKind.Utc).AddTicks(2588));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 21, 21, 24, 344, DateTimeKind.Utc).AddTicks(2601));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 21, 21, 24, 344, DateTimeKind.Utc).AddTicks(2614));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 20, 25, 19, 126, DateTimeKind.Utc).AddTicks(5611));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 20, 25, 19, 126, DateTimeKind.Utc).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 20, 25, 19, 126, DateTimeKind.Utc).AddTicks(5643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 20, 25, 19, 126, DateTimeKind.Utc).AddTicks(5654));
        }
    }
}
