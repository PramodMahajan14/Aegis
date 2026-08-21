using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seed_system_tenant_e : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                columns: new[] { "Email", "OnboardingDate" },
                values: new object[] { "codedev90@gmail.com", new DateTime(2026, 8, 21, 20, 24, 34, 178, DateTimeKind.Utc).AddTicks(3415) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                columns: new[] { "Email", "OnboardingDate" },
                values: new object[] { "", new DateTime(2026, 8, 21, 20, 20, 8, 652, DateTimeKind.Utc).AddTicks(2703) });
        }
    }
}
