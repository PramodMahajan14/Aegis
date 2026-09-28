using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adveshta.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class added_IsActiveCol_contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Contacts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 28, 16, 41, 33, 250, DateTimeKind.Utc).AddTicks(9911));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Contacts");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 24, 19, 38, 41, 737, DateTimeKind.Utc).AddTicks(2921));
        }
    }
}
