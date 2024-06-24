using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class TutorReplyFeedbacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Response",
                table: "LearningModuleFeedbacks");

            migrationBuilder.CreateTable(
                name: "TutorReplyFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LearningModuleFeedbackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorReplyFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorReplyFeedback_AppUsers_ReplierId",
                        column: x => x.ReplierId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorReplyFeedback_LearningModuleFeedbacks_LearningModuleFeedbackId",
                        column: x => x.LearningModuleFeedbackId,
                        principalTable: "LearningModuleFeedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorReplyFeedback_LearningModuleFeedbackId",
                table: "TutorReplyFeedback",
                column: "LearningModuleFeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorReplyFeedback_ReplierId",
                table: "TutorReplyFeedback",
                column: "ReplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorReplyFeedback");

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "LearningModuleFeedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
