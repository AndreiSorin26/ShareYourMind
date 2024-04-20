using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class ReconfiguredDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_PosterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_PosterId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_ReporterId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReporterId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReactions",
                table: "PostReactions");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PosterId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions");

            migrationBuilder.RenameColumn(
                name: "ReporterId",
                table: "Reports",
                newName: "ReportingUserId");

            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_PosterId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PostReactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PostReactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PostReactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CommentReactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CommentReactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CommentReactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Reports_ReportingUserId_PostId",
                table: "Reports",
                columns: new[] { "ReportingUserId", "PostId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PostReactions_UserId_PostId",
                table: "PostReactions",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReactions",
                table: "PostReactions",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FriendRequests_SenderId_ReceiverId",
                table: "FriendRequests",
                columns: new[] { "SenderId", "ReceiverId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Comments_UserId_PostId",
                table: "Comments",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CommentReactions_UserId_CommentId",
                table: "CommentReactions",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_ReportingUserId",
                table: "Reports",
                column: "ReportingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_ReportingUserId",
                table: "Reports");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Reports_ReportingUserId_PostId",
                table: "Reports");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PostReactions_UserId_PostId",
                table: "PostReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReactions",
                table: "PostReactions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FriendRequests_SenderId_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Comments_UserId_PostId",
                table: "Comments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CommentReactions_UserId_CommentId",
                table: "CommentReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostReactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PostReactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PostReactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CommentReactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CommentReactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CommentReactions");

            migrationBuilder.RenameColumn(
                name: "ReportingUserId",
                table: "Reports",
                newName: "ReporterId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "PosterId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                newName: "IX_Posts_PosterId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "PosterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReactions",
                table: "PostReactions",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterId",
                table: "Reports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PosterId",
                table: "Comments",
                column: "PosterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_PosterId",
                table: "Comments",
                column: "PosterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_PosterId",
                table: "Posts",
                column: "PosterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_ReporterId",
                table: "Reports",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
