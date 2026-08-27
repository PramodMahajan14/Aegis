using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rullable_unassigned_roleemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 27, 19, 5, 54, 134, DateTimeKind.Utc).AddTicks(6118));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 27, 18, 33, 5, 267, DateTimeKind.Utc).AddTicks(6976));
        }
    }
}
