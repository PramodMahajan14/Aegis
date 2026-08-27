using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeUnassignedColumnsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_AssignedById",
                table: "EmployeeAppRoleMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_UnassignedById",
                table: "EmployeeAppRoleMaps");

            migrationBuilder.AlterColumn<Guid>(
                name: "UnassignedById",
                table: "EmployeeAppRoleMaps",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UnassignedAt",
                table: "EmployeeAppRoleMaps",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedById",
                table: "EmployeeAppRoleMaps",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignedAt",
                table: "EmployeeAppRoleMaps",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 27, 19, 12, 57, 636, DateTimeKind.Utc).AddTicks(5395));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_AssignedById",
                table: "EmployeeAppRoleMaps",
                column: "AssignedById",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_UnassignedById",
                table: "EmployeeAppRoleMaps",
                column: "UnassignedById",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_AssignedById",
                table: "EmployeeAppRoleMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_UnassignedById",
                table: "EmployeeAppRoleMaps");

            migrationBuilder.AlterColumn<Guid>(
                name: "UnassignedById",
                table: "EmployeeAppRoleMaps",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UnassignedAt",
                table: "EmployeeAppRoleMaps",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedById",
                table: "EmployeeAppRoleMaps",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignedAt",
                table: "EmployeeAppRoleMaps",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 27, 19, 5, 54, 134, DateTimeKind.Utc).AddTicks(6118));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_AssignedById",
                table: "EmployeeAppRoleMaps",
                column: "AssignedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppRoleMaps_Employees_UnassignedById",
                table: "EmployeeAppRoleMaps",
                column: "UnassignedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
