using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1489), new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1500) });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "Id", "Body", "ModifiedDate", "PublishDate", "Title" },
                values: new object[] { 2, "Test", new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1501), new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1502), "AI take over the world" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 26, 0, 41, 46, 647, DateTimeKind.Local).AddTicks(2334), new DateTime(2024, 1, 26, 0, 41, 46, 647, DateTimeKind.Local).AddTicks(2343) });
        }
    }
}
