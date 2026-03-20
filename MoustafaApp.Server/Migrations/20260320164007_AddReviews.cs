using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoustafaApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "ProductId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "ProductId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "ProductId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "ProductId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "ProductId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 6,
                column: "ProductId",
                value: 24);

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "DatePosted", "ProductId", "Rating", "ReviewText", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 7, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 8, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 9, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 10, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 11, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 12, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null },
                    { 13, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 14, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 15, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 16, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 17, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 18, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null },
                    { 19, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 20, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 21, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 22, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 23, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 24, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null },
                    { 25, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 26, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 27, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 28, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 29, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 30, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null },
                    { 31, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, 4.5m, "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable. As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.", null, null },
                    { 32, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, 4.5m, "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch. Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.", null, null },
                    { 33, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 32, 4.5m, "This t-shirt is a must-have for anyone who appreciates good design. The minimalistic yet stylish pattern caught my eye, and the fit is perfect. I can see the designer's touch in every aspect of this shirt.", null, null },
                    { 34, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 32, 4.5m, "As a UI/UX enthusiast, I value simplicity and functionality. This t-shirt not only represents those principlesto wear. It's evident that the designer poured their creativity into making this t-shirt stand out.", null, null },
                    { 35, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 33, 4.5m, "This t-shirt is a fusion of comfort and creativity. The fabric is soft, and the design speaks volumes about the designer's skill. It's like wearing a piece of art that reflects my passion for both design and fashion.", null, null },
                    { 36, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 33, 4.5m, "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy. The intricate details and thoughtful layout of the design make this shirt a conversation starter.", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 36);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 1, 49, 30, 111, DateTimeKind.Utc).AddTicks(2544));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 1, 49, 30, 111, DateTimeKind.Utc).AddTicks(2561));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 1, 49, 30, 111, DateTimeKind.Utc).AddTicks(2745));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 1, 49, 30, 111, DateTimeKind.Utc).AddTicks(2756));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 6,
                column: "ProductId",
                value: 1);
        }
    }
}
