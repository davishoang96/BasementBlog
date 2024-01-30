using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class morePosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6268), new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6289) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6291), new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6292) });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "Id", "Body", "Description", "ModifiedDate", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 3, "Test", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum", new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6294), new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6294), "Lorem Ipsum" },
                    { 4, "Test", "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots", new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6296), new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6297), "Where does it come from?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9056), new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9067) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9109), new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9110) });
        }
    }
}
