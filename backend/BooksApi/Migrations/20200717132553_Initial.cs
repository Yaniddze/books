using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_author", x => x.id); });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_genre", x => x.id); });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    login = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_users", x => x.id); });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false),
                    genre_id = table.Column<Guid>(nullable: false),
                    author_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                    table.ForeignKey(
                        name: "FK_books_author_author_id",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_books_genre_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    token_value = table.Column<string>(nullable: true),
                    user_id = table.Column<Guid>(nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    jwt_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_author_id",
                table: "books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_genre_id",
                table: "books",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_tokens_user_id",
                table: "tokens",
                column: "user_id");

            migrationBuilder.Sql(
                "INSERT INTO genre VALUES (uuid_generate_v4(), 'Detective'), (uuid_generate_v4(), 'Action');");
            migrationBuilder.Sql(
                "INSERT INTO author VALUES (uuid_generate_v4(), 'Zinevich Yan'), (uuid_generate_v4(), 'Bagrov Nikolay');");
            migrationBuilder.Sql(
                "INSERT INTO books VALUES (uuid_generate_v4(), 'New book', 1942, (SELECT id FROM genre WHERE title = 'Detective' LIMIT 1), (SELECT id FROM author WHERE name = 'Zinevich Yan' LIMIT 1));");
            migrationBuilder.Sql(
                "INSERT INTO users VALUES (uuid_generate_v4(), 'root', 'root');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}