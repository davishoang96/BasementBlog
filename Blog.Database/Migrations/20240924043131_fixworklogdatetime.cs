using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Database.Migrations
{
    /// <inheritdoc />
    public partial class fixworklogdatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Unique_LoggedDate",
                table: "WorkLogs",
                newName: "IX_WorkLogs_LoggedDate");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 9, 24, 12, 31, 31, 154, DateTimeKind.Local).AddTicks(6755), new DateTime(2024, 9, 24, 12, 31, 31, 154, DateTimeKind.Local).AddTicks(6765) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 9, 24, 12, 31, 31, 154, DateTimeKind.Local).AddTicks(6766), new DateTime(2024, 9, 24, 12, 31, 31, 154, DateTimeKind.Local).AddTicks(6767) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_WorkLogs_LoggedDate",
                table: "WorkLogs",
                newName: "IX_Unique_LoggedDate");

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
    }
}
