using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksApi.Migrations
{
    public partial class AddUserIdToToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tokens_users_UserDBId",
                table: "tokens");

            migrationBuilder.DropIndex(
                name: "IX_tokens_UserDBId",
                table: "tokens");

            migrationBuilder.DropColumn(
                name: "UserDBId",
                table: "tokens");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "tokens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tokens_user_id",
                table: "tokens",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tokens_users_user_id",
                table: "tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tokens_users_user_id",
                table: "tokens");

            migrationBuilder.DropIndex(
                name: "IX_tokens_user_id",
                table: "tokens");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tokens");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDBId",
                table: "tokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tokens_UserDBId",
                table: "tokens",
                column: "UserDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_tokens_users_UserDBId",
                table: "tokens",
                column: "UserDBId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
