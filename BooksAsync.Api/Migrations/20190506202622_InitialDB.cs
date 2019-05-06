using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAsync.Api.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("c8077fa3-9c81-4c0d-b5c7-b44884ffaf29"), "George", "RR Martin" },
                    { new Guid("64d0b6b5-a50c-4e50-9ca5-0851e6eee16d"), "Stephen", "Fry" },
                    { new Guid("16f33673-09fe-42a8-9e2d-1023a1d65e6f"), "James", "Elroy" },
                    { new Guid("e731f22a-e272-4ae5-b2eb-290d95770584"), "Douglas", "Adams" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[] { new Guid("1c714380-05ba-49e3-846a-8dca1f47fbc9"), new Guid("c8077fa3-9c81-4c0d-b5c7-b44884ffaf29"), "The book that seems imposible to write", "The Winds of Winter" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[] { new Guid("9d3acb2b-7df4-4bd8-9f38-9919f3db1aaf"), new Guid("c8077fa3-9c81-4c0d-b5c7-b44884ffaf29"), "The book that was easy to write", "The Summer Sun" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
