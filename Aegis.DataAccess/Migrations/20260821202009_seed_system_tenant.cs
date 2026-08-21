using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seed_system_tenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tenants",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRootUser",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "ContactNumber", "ContactPerson", "CreatedAt", "Currency", "DeactivatedAt", "DomainName", "Email", "IsActive", "IsSystemTenant", "Locale", "Name", "OnboardingDate", "Status", "SubscriptionEndDate", "SubscriptionPlanId", "SubscriptionStartDate", "SuspendedAt", "TimeZone", "UpdatedAt" },
                values: new object[] { new Guid("c84c9fae-750c-4327-b3fd-338517be8161"), "Pramod Mahajan", "Pramod Mahajan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "INR", null, "codedev.in", "", false, true, "en-IN", "Code Dev", new DateTime(2026, 8, 21, 20, 20, 8, 652, DateTimeKind.Utc).AddTicks(2703), 1, null, null, null, null, "Asia/Kolkata", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsRootUser",
                table: "AspNetUsers");
        }
    }
}
