using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackForClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearningModuleFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Star = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LearningModuleId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LikeNumber = table.Column<int>(type: "int", nullable: false),
                    DislikeNumber = table.Column<int>(type: "int", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningModuleFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningModuleFeedbacks_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LearningModuleFeedbacks_LearningModules_LearningModuleId",
                        column: x => x.LearningModuleId,
                        principalTable: "LearningModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningModuleFeedbacks_AppUserId",
                table: "LearningModuleFeedbacks",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningModuleFeedbacks_LearningModuleId",
                table: "LearningModuleFeedbacks",
                column: "LearningModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningModuleFeedbacks");
        }
    }
}
