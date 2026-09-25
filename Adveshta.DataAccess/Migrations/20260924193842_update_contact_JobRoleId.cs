using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adveshta.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_contact_JobRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_JobRoles_ProjectContactRoleId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "ProjectContactRoleId",
                table: "Contacts",
                newName: "JobRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_ProjectContactRoleId",
                table: "Contacts",
                newName: "IX_Contacts_JobRoleId");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 24, 19, 38, 41, 737, DateTimeKind.Utc).AddTicks(2921));

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_JobRoles_JobRoleId",
                table: "Contacts",
                column: "JobRoleId",
                principalTable: "JobRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_JobRoles_JobRoleId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "JobRoleId",
                table: "Contacts",
                newName: "ProjectContactRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_JobRoleId",
                table: "Contacts",
                newName: "IX_Contacts_ProjectContactRoleId");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 24, 19, 31, 5, 54, DateTimeKind.Utc).AddTicks(9844));

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_JobRoles_ProjectContactRoleId",
                table: "Contacts",
                column: "ProjectContactRoleId",
                principalTable: "JobRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
