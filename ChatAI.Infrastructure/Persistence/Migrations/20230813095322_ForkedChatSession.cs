using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ForkedChatSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_Messages_ForkedFromMessageId",
                table: "ChatSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "ForkedFromChatSessionId",
                table: "ChatSessions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_ForkedFromChatSessionId",
                table: "ChatSessions",
                column: "ForkedFromChatSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_ChatSessions_ForkedFromChatSessionId",
                table: "ChatSessions",
                column: "ForkedFromChatSessionId",
                principalTable: "ChatSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_Messages_ForkedFromMessageId",
                table: "ChatSessions",
                column: "ForkedFromMessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_ChatSessions_ForkedFromChatSessionId",
                table: "ChatSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_Messages_ForkedFromMessageId",
                table: "ChatSessions");

            migrationBuilder.DropIndex(
                name: "IX_ChatSessions_ForkedFromChatSessionId",
                table: "ChatSessions");

            migrationBuilder.DropColumn(
                name: "ForkedFromChatSessionId",
                table: "ChatSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_Messages_ForkedFromMessageId",
                table: "ChatSessions",
                column: "ForkedFromMessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
