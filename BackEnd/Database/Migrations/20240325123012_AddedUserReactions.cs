using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserReactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DislikeReactions",
                columns: table => new
                {
                    DislikeReactionPostsId = table.Column<Guid>(type: "uuid", nullable: false),
                    DislikeReactionUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikeReactions", x => new { x.DislikeReactionPostsId, x.DislikeReactionUsersId });
                    table.ForeignKey(
                        name: "FK_DislikeReactions_Posts_DislikeReactionPostsId",
                        column: x => x.DislikeReactionPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikeReactions_Users_DislikeReactionUsersId",
                        column: x => x.DislikeReactionUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaughReactions",
                columns: table => new
                {
                    LaughReactionPostsId = table.Column<Guid>(type: "uuid", nullable: false),
                    LaughReactionUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaughReactions", x => new { x.LaughReactionPostsId, x.LaughReactionUsersId });
                    table.ForeignKey(
                        name: "FK_LaughReactions_Posts_LaughReactionPostsId",
                        column: x => x.LaughReactionPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaughReactions_Users_LaughReactionUsersId",
                        column: x => x.LaughReactionUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoveReactions",
                columns: table => new
                {
                    LoveReactionPostsId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoveReactionUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoveReactions", x => new { x.LoveReactionPostsId, x.LoveReactionUsersId });
                    table.ForeignKey(
                        name: "FK_LoveReactions_Posts_LoveReactionPostsId",
                        column: x => x.LoveReactionPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoveReactions_Users_LoveReactionUsersId",
                        column: x => x.LoveReactionUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DislikeReactions_DislikeReactionUsersId",
                table: "DislikeReactions",
                column: "DislikeReactionUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_LaughReactions_LaughReactionUsersId",
                table: "LaughReactions",
                column: "LaughReactionUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_LoveReactions_LoveReactionUsersId",
                table: "LoveReactions",
                column: "LoveReactionUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DislikeReactions");

            migrationBuilder.DropTable(
                name: "LaughReactions");

            migrationBuilder.DropTable(
                name: "LoveReactions");
        }
    }
}
