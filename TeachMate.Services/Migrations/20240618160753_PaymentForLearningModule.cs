using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class PaymentForLearningModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "LearningModules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "LearningModules",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "LearningModulePaymentOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LearningModuleId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    HasClaimed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningModulePaymentOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningModulePaymentOrders_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LearningModulePaymentOrders_LearningModules_LearningModuleId",
                        column: x => x.LearningModuleId,
                        principalTable: "LearningModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningModulePaymentOrders_LearnerId",
                table: "LearningModulePaymentOrders",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningModulePaymentOrders_LearningModuleId",
                table: "LearningModulePaymentOrders",
                column: "LearningModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningModulePaymentOrders");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "LearningModules");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "LearningModules");
        }
    }
}
