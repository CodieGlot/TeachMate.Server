using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class LearningModuleModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedSchedule",
                table: "LearningModules");

            migrationBuilder.DropColumn(
                name: "SerializedSchedule",
                table: "LearningModuleRequests");

            migrationBuilder.DropColumn(
                name: "TutorDisplayName",
                table: "LearningModuleRequests");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "LearningModuleRequests");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "LearningSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "LearningSessions");

            migrationBuilder.AddColumn<string>(
                name: "SerializedSchedule",
                table: "LearningModules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerializedSchedule",
                table: "LearningModuleRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TutorDisplayName",
                table: "LearningModuleRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TutorId",
                table: "LearningModuleRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
