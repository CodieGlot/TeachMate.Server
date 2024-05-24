using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachMate.Services.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotificationsReceivers_PushNotifications_PushNotificationId",
                table: "PushNotificationsReceivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PushNotificationsReceivers",
                table: "PushNotificationsReceivers");

            migrationBuilder.RenameTable(
                name: "PushNotificationsReceivers",
                newName: "PushNotificationReceivers");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "PushNotifications",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "PushNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushNotificationReceivers",
                table: "PushNotificationReceivers",
                columns: new[] { "PushNotificationId", "ReceiverId" });

            migrationBuilder.CreateIndex(
                name: "IX_PushNotifications_CreatedAt",
                table: "PushNotifications",
                column: "CreatedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotificationReceivers_PushNotifications_PushNotificationId",
                table: "PushNotificationReceivers",
                column: "PushNotificationId",
                principalTable: "PushNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotificationReceivers_PushNotifications_PushNotificationId",
                table: "PushNotificationReceivers");

            migrationBuilder.DropIndex(
                name: "IX_PushNotifications_CreatedAt",
                table: "PushNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PushNotificationReceivers",
                table: "PushNotificationReceivers");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "PushNotifications");

            migrationBuilder.RenameTable(
                name: "PushNotificationReceivers",
                newName: "PushNotificationsReceivers");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "PushNotifications",
                newName: "CreatedOn");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushNotificationsReceivers",
                table: "PushNotificationsReceivers",
                columns: new[] { "PushNotificationId", "ReceiverId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotificationsReceivers_PushNotifications_PushNotificationId",
                table: "PushNotificationsReceivers",
                column: "PushNotificationId",
                principalTable: "PushNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
