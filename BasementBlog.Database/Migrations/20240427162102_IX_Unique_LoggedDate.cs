using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class IX_Unique_LoggedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkLogs_Id",
                table: "WorkLogs");

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
                values: new object[] { new DateTime(2024, 4, 28, 0, 21, 2, 437, DateTimeKind.Local).AddTicks(9920), new DateTime(2024, 4, 28, 0, 21, 2, 437, DateTimeKind.Local).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 28, 0, 21, 2, 437, DateTimeKind.Local).AddTicks(9990), new DateTime(2024, 4, 28, 0, 21, 2, 437, DateTimeKind.Local).AddTicks(9990) });

            migrationBuilder.CreateIndex(
                name: "IX_Unique_LoggedDate",
                table: "WorkLogs",
                column: "LoggedDate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs");

            migrationBuilder.DropIndex(
                name: "IX_Unique_LoggedDate",
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkLogs_Id",
                table: "WorkLogs",
                column: "Id");
        }
    }
}
