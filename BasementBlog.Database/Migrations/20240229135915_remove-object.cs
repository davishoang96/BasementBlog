using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class removeobject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4161), new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4175) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4177), new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4178) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4179), new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4182), new DateTime(2024, 2, 29, 21, 59, 14, 760, DateTimeKind.Local).AddTicks(4183) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6294), new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6294) });

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6296), new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6297) });
        }
    }
}
