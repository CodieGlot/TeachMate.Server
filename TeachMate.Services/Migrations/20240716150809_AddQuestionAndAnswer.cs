using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionAndAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Answers_AnswerId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_AnswerId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionId",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AnswerId",
                table: "Questions",
                column: "AnswerId",
                unique: true,
                filter: "[AnswerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Answers_AnswerId",
                table: "Questions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");
        }
    }
}
