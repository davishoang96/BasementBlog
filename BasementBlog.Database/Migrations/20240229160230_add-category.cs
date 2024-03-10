using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class addcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "CategoryId", "Description", "ModifiedDate", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, "Test", null, "Test", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(720), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(730), "Make the world better" },
                    { 2, "Test", null, "Test", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(732), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(732), "AI take over the world" },
                    { 3, "Test", null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(734), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(734), "Lorem Ipsum" },
                    { 4, "Test", null, "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(736), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(736), "Where does it come from?" },
                    { 5, "Test", null, "Lorem Ipsum is not simply random text. It has roots", new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(737), new DateTime(2024, 3, 1, 0, 2, 30, 335, DateTimeKind.Local).AddTicks(738), "Create post and add catetory" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
