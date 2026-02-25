using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoustafaApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class EditUpdatedAtinReviewEntityAndSizeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 20, 34, 38, 707, DateTimeKind.Utc).AddTicks(9789));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 20, 34, 38, 707, DateTimeKind.Utc).AddTicks(9806));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 20, 34, 38, 707, DateTimeKind.Utc).AddTicks(9859));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 20, 34, 38, 707, DateTimeKind.Utc).AddTicks(9872));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 6,
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 1,
                column: "SizeName",
                value: "Small");

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 2,
                column: "SizeName",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 3,
                column: "SizeName",
                value: "Large");

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 4,
                column: "SizeName",
                value: "X-Large");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 13, 5, 1, 583, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 13, 5, 1, 583, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 13, 5, 1, 583, DateTimeKind.Utc).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 13, 5, 1, 583, DateTimeKind.Utc).AddTicks(9051));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 1,
                column: "SizeName",
                value: "S");

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 2,
                column: "SizeName",
                value: "M");

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 3,
                column: "SizeName",
                value: "L");

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 4,
                column: "SizeName",
                value: "XL");
        }
    }
}
