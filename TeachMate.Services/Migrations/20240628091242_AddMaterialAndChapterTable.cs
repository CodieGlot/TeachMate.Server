using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterialAndChapterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_SystemReport_SystemReportId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_UserReport_UserReportId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReport_AppUsers_ReportedUserId",
                table: "UserReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReport",
                table: "UserReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemReport",
                table: "SystemReport");

            migrationBuilder.RenameTable(
                name: "UserReport",
                newName: "UserReports");

            migrationBuilder.RenameTable(
                name: "SystemReport",
                newName: "SystemReports");

            migrationBuilder.RenameIndex(
                name: "IX_UserReport_ReportedUserId",
                table: "UserReports",
                newName: "IX_UserReports_ReportedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReports",
                table: "UserReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemReports",
                table: "SystemReports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LearningChapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningChapters_LearningModules_LearningModuleId",
                        column: x => x.LearningModuleId,
                        principalTable: "LearningModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkDownload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LearningChapterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningMaterials_LearningChapters_LearningChapterId",
                        column: x => x.LearningChapterId,
                        principalTable: "LearningChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningChapters_LearningModuleId",
                table: "LearningChapters",
                column: "LearningModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningMaterials_LearningChapterId",
                table: "LearningMaterials",
                column: "LearningChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_SystemReports_SystemReportId",
                table: "Report",
                column: "SystemReportId",
                principalTable: "SystemReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_UserReports_UserReportId",
                table: "Report",
                column: "UserReportId",
                principalTable: "UserReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AppUsers_ReportedUserId",
                table: "UserReports",
                column: "ReportedUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_SystemReports_SystemReportId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_UserReports_UserReportId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AppUsers_ReportedUserId",
                table: "UserReports");

            migrationBuilder.DropTable(
                name: "LearningMaterials");

            migrationBuilder.DropTable(
                name: "LearningChapters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReports",
                table: "UserReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemReports",
                table: "SystemReports");

            migrationBuilder.RenameTable(
                name: "UserReports",
                newName: "UserReport");

            migrationBuilder.RenameTable(
                name: "SystemReports",
                newName: "SystemReport");

            migrationBuilder.RenameIndex(
                name: "IX_UserReports_ReportedUserId",
                table: "UserReport",
                newName: "IX_UserReport_ReportedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReport",
                table: "UserReport",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemReport",
                table: "SystemReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_SystemReport_SystemReportId",
                table: "Report",
                column: "SystemReportId",
                principalTable: "SystemReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_UserReport_UserReportId",
                table: "Report",
                column: "UserReportId",
                principalTable: "UserReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReport_AppUsers_ReportedUserId",
                table: "UserReport",
                column: "ReportedUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
