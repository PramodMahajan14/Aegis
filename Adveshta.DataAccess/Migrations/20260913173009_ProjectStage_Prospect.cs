using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adveshta.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProjectStage_Prospect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectStageId",
                table: "Prospects",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 13, 17, 30, 9, 541, DateTimeKind.Utc).AddTicks(4519));

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_ProjectStageId",
                table: "Prospects",
                column: "ProjectStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_ProjectStages_ProjectStageId",
                table: "Prospects",
                column: "ProjectStageId",
                principalTable: "ProjectStages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_ProjectStages_ProjectStageId",
                table: "Prospects");

            migrationBuilder.DropIndex(
                name: "IX_Prospects_ProjectStageId",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProjectStageId",
                table: "Prospects");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 13, 12, 9, 3, 969, DateTimeKind.Utc).AddTicks(1839));
        }
    }
}
