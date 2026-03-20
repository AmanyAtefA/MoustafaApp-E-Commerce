using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoustafaApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddReviews2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 17, 11, 36, 408, DateTimeKind.Utc).AddTicks(7128));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 17, 11, 36, 408, DateTimeKind.Utc).AddTicks(7148));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 17, 11, 36, 408, DateTimeKind.Utc).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 17, 11, 36, 408, DateTimeKind.Utc).AddTicks(7176));

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "DatePosted", "ProductId", "Rating", "ReviewText", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 37, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 38, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 39, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 40, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 41, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 42, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 42);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 16, 40, 5, 213, DateTimeKind.Utc).AddTicks(3694));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 16, 40, 5, 213, DateTimeKind.Utc).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 16, 40, 5, 213, DateTimeKind.Utc).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 16, 40, 5, 213, DateTimeKind.Utc).AddTicks(3723));

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "DatePosted", "ProductId", "Rating", "ReviewText", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 2, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 3, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 4, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 5, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 6, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null }
                });
        }
    }
}
