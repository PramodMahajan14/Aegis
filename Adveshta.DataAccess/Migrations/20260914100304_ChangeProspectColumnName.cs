using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adveshta.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProspectColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Prospects",
                newName: "ProjectLocation");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 14, 10, 3, 4, 428, DateTimeKind.Utc).AddTicks(792));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectLocation",
                table: "Prospects",
                newName: "Location");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 13, 19, 4, 5, 679, DateTimeKind.Utc).AddTicks(7754));
        }
    }
}
