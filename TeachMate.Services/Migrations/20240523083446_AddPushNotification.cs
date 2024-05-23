using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddPushNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationsReceivers",
                columns: table => new
                {
                    PushNotificationId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationsReceivers", x => new { x.PushNotificationId, x.ReceiverId });
                    table.ForeignKey(
                        name: "FK_PushNotificationsReceivers_PushNotifications_PushNotificationId",
                        column: x => x.PushNotificationId,
                        principalTable: "PushNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushNotificationsReceivers");

            migrationBuilder.DropTable(
                name: "PushNotifications");
        }
    }
}
