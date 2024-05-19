using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Database.Migrations
{
    /// <inheritdoc />
    public partial class worklog_dateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WorkLogs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs",
                column: "LoggedDate");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3600), new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3610) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3610), new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3610) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs");

            migrationBuilder.DeleteData(
                table: "WorkLogs",
                keyColumn: "LoggedDate",
                keyValue: "11/04/1996");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WorkLogs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2726), new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2738) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2739), new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2740) });
        }
    }
}
