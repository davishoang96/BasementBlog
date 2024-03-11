using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 11, 23, 14, 50, 484, DateTimeKind.Local).AddTicks(1424), new DateTime(2024, 3, 11, 23, 14, 50, 484, DateTimeKind.Local).AddTicks(1433) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 11, 23, 14, 50, 484, DateTimeKind.Local).AddTicks(1435), new DateTime(2024, 3, 11, 23, 14, 50, 484, DateTimeKind.Local).AddTicks(1435) });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Id",
                table: "Posts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Id",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(720), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(732), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(732) });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "CategoryId", "Description", "ModifiedDate", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 3, "Test", null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(734), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(734), "Lorem Ipsum" },
                    { 4, "Test", null, "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(736), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(736), "Where does it come from?" },
                    { 5, "Test", null, "Lorem Ipsum is not simply random text. It has roots", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(737), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(738), "Create post and add catetory" }
                });
        }
    }
}
