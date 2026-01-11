using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoustafaApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAtInProductEntite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");
        }
    }
}
