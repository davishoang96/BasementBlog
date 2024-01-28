using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class addDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Post",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ModifiedDate", "PublishDate" },
                values: new object[] { "Test", new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9056), new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9067) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ModifiedDate", "PublishDate" },
                values: new object[] { "Test", new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9109), new DateTime(2024, 1, 28, 10, 33, 7, 477, DateTimeKind.Local).AddTicks(9110) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Post");

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1489), new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1500) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1501), new DateTime(2024, 1, 26, 1, 2, 14, 418, DateTimeKind.Local).AddTicks(1502) });
        }
    }
}
