using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class SavePostDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostDtoId",
                table: "Likes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostTitle = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BlogId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostDto_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                    table.ForeignKey(
                        name: "FK_PostDto_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostDtoId",
                table: "Likes",
                column: "PostDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDto_BlogId",
                table: "PostDto",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDto_UserId",
                table: "PostDto",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_PostDto_PostDtoId",
                table: "Likes",
                column: "PostDtoId",
                principalTable: "PostDto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_PostDto_PostDtoId",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "PostDto");

            migrationBuilder.DropIndex(
                name: "IX_Likes_PostDtoId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "PostDtoId",
                table: "Likes");
        }
    }
}
