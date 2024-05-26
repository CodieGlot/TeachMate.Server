using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LearningModuleId",
                table: "LearningSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradeLevel",
                table: "Learners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LearningSessions_LearningModuleId",
                table: "LearningSessions",
                column: "LearningModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningSessions_LearningModules_LearningModuleId",
                table: "LearningSessions",
                column: "LearningModuleId",
                principalTable: "LearningModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningSessions_LearningModules_LearningModuleId",
                table: "LearningSessions");

            migrationBuilder.DropIndex(
                name: "IX_LearningSessions_LearningModuleId",
                table: "LearningSessions");

            migrationBuilder.DropColumn(
                name: "LearningModuleId",
                table: "LearningSessions");

            migrationBuilder.DropColumn(
                name: "GradeLevel",
                table: "Learners");
        }
    }
}
