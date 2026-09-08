using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_prospect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "Prospects",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Prospects",
                keyColumn: "Location",
                keyValue: null,
                column: "Location",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Prospects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Prospects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OfficeLocation",
                table: "Prospects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProspectNo",
                table: "Prospects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProspectSource",
                table: "Prospects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProspectTemperature",
                table: "Prospects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Prospects",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 8, 19, 52, 0, 263, DateTimeKind.Utc).AddTicks(6296));

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_UpdatedById",
                table: "Prospects",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_Employees_UpdatedById",
                table: "Prospects",
                column: "UpdatedById",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_Employees_UpdatedById",
                table: "Prospects");

            migrationBuilder.DropIndex(
                name: "IX_Prospects_UpdatedById",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "OfficeLocation",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProspectNo",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProspectSource",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProspectTemperature",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Prospects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "Prospects",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Prospects",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 30, 20, 5, 52, 508, DateTimeKind.Utc).AddTicks(8313));
        }
    }
}
