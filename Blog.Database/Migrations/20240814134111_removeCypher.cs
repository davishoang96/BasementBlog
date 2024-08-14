using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Database.Migrations
{
    /// <inheritdoc />
    public partial class removeCypher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 8, 14, 21, 41, 10, 748, DateTimeKind.Local).AddTicks(1868), new DateTime(2024, 8, 14, 21, 41, 10, 748, DateTimeKind.Local).AddTicks(1877) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 8, 14, 21, 41, 10, 748, DateTimeKind.Local).AddTicks(1879), new DateTime(2024, 8, 14, 21, 41, 10, 748, DateTimeKind.Local).AddTicks(1879) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 8, 13, 19, 22, 47, 247, DateTimeKind.Local).AddTicks(9585), new DateTime(2024, 8, 13, 19, 22, 47, 247, DateTimeKind.Local).AddTicks(9595) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 8, 13, 19, 22, 47, 247, DateTimeKind.Local).AddTicks(9597), new DateTime(2024, 8, 13, 19, 22, 47, 247, DateTimeKind.Local).AddTicks(9598) });
        }
    }
}
