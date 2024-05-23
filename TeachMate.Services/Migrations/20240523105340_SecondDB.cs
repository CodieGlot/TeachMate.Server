using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class SecondDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GradeLevel",
                table: "Tutors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradeLevel",
                table: "LearningModules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModuleType",
                table: "LearningModules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumOfWeeks",
                table: "LearningModules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyScheduleId",
                table: "LearningModules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LearnerId",
                table: "LearningModuleRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LearningModuleId",
                table: "LearningModuleRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LearningSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    LinkMeet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfSlot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklySchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklySlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklySlots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningModules_WeeklyScheduleId",
                table: "LearningModules",
                column: "WeeklyScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningModuleRequests_LearnerId",
                table: "LearningModuleRequests",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningModuleRequests_LearningModuleId",
                table: "LearningModuleRequests",
                column: "LearningModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningModuleRequests_Learners_LearnerId",
                table: "LearningModuleRequests",
                column: "LearnerId",
                principalTable: "Learners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningModuleRequests_LearningModules_LearningModuleId",
                table: "LearningModuleRequests",
                column: "LearningModuleId",
                principalTable: "LearningModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningModules_WeeklySchedules_WeeklyScheduleId",
                table: "LearningModules",
                column: "WeeklyScheduleId",
                principalTable: "WeeklySchedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningModuleRequests_Learners_LearnerId",
                table: "LearningModuleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningModuleRequests_LearningModules_LearningModuleId",
                table: "LearningModuleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningModules_WeeklySchedules_WeeklyScheduleId",
                table: "LearningModules");

            migrationBuilder.DropTable(
                name: "LearningSessions");

            migrationBuilder.DropTable(
                name: "WeeklySchedules");

            migrationBuilder.DropTable(
                name: "WeeklySlots");

            migrationBuilder.DropIndex(
                name: "IX_LearningModules_WeeklyScheduleId",
                table: "LearningModules");

            migrationBuilder.DropIndex(
                name: "IX_LearningModuleRequests_LearnerId",
                table: "LearningModuleRequests");

            migrationBuilder.DropIndex(
                name: "IX_LearningModuleRequests_LearningModuleId",
                table: "LearningModuleRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "GradeLevel",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "GradeLevel",
                table: "LearningModules");

            migrationBuilder.DropColumn(
                name: "ModuleType",
                table: "LearningModules");

            migrationBuilder.DropColumn(
                name: "NumOfWeeks",
                table: "LearningModules");

            migrationBuilder.DropColumn(
                name: "WeeklyScheduleId",
                table: "LearningModules");

            migrationBuilder.DropColumn(
                name: "LearnerId",
                table: "LearningModuleRequests");

            migrationBuilder.DropColumn(
                name: "LearningModuleId",
                table: "LearningModuleRequests");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AppUsers");
        }
    }
}
