using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rename_tenant_organization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRoles_Tenants_TenantId",
                table: "ApplicationRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppRoleMaps_Tenants_TenantId",
                table: "EmployeeAppRoleMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tenants_TenantId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRoles_Tenants_TenantId",
                table: "JobRoles");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "JobRoles",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRoles_TenantId",
                table: "JobRoles",
                newName: "IX_JobRoles_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Employees",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_TenantId",
                table: "Employees",
                newName: "IX_Employees_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "EmployeeAppRoleMaps",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAppRoleMaps_TenantId",
                table: "EmployeeAppRoleMaps",
                newName: "IX_EmployeeAppRoleMaps_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "ApplicationRoles",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationRoles_TenantId",
                table: "ApplicationRoles",
                newName: "IX_ApplicationRoles_OrganizationId");

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPerson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DomainName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeZone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Currency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Locale = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OnboardingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SuspendedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SubscriptionPlanId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsSystemTenant = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "ContactNumber", "ContactPerson", "CreatedAt", "Currency", "DeactivatedAt", "DomainName", "Email", "IsActive", "IsSystemTenant", "Locale", "Name", "OnboardingDate", "Status", "SubscriptionEndDate", "SubscriptionPlanId", "SubscriptionStartDate", "SuspendedAt", "TimeZone", "UpdatedAt" },
                values: new object[] { new Guid("c84c9fae-750c-4327-b3fd-338517be8161"), "Pramod Mahajan", "Pramod Mahajan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "INR", null, "codedev.in", "codedev90@gmail.com", false, true, "en-IN", "Code Dev", new DateTime(2026, 8, 28, 16, 20, 54, 616, DateTimeKind.Utc).AddTicks(47), 1, null, null, null, null, "Asia/Kolkata", null });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRoles_Organizations_OrganizationId",
                table: "ApplicationRoles",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppRoleMaps_Organizations_OrganizationId",
                table: "EmployeeAppRoleMaps",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Organizations_OrganizationId",
                table: "Employees",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRoles_Organizations_OrganizationId",
                table: "JobRoles",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRoles_Organizations_OrganizationId",
                table: "ApplicationRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppRoleMaps_Organizations_OrganizationId",
                table: "EmployeeAppRoleMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Organizations_OrganizationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRoles_Organizations_OrganizationId",
                table: "JobRoles");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "JobRoles",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRoles_OrganizationId",
                table: "JobRoles",
                newName: "IX_JobRoles_TenantId");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Employees",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OrganizationId",
                table: "Employees",
                newName: "IX_Employees_TenantId");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "EmployeeAppRoleMaps",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAppRoleMaps_OrganizationId",
                table: "EmployeeAppRoleMaps",
                newName: "IX_EmployeeAppRoleMaps_TenantId");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "ApplicationRoles",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationRoles_OrganizationId",
                table: "ApplicationRoles",
                newName: "IX_ApplicationRoles_TenantId");

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPerson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Currency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DomainName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSystemTenant = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Locale = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OnboardingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SubscriptionPlanId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SuspendedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TimeZone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "ContactNumber", "ContactPerson", "CreatedAt", "Currency", "DeactivatedAt", "DomainName", "Email", "IsActive", "IsSystemTenant", "Locale", "Name", "OnboardingDate", "Status", "SubscriptionEndDate", "SubscriptionPlanId", "SubscriptionStartDate", "SuspendedAt", "TimeZone", "UpdatedAt" },
                values: new object[] { new Guid("c84c9fae-750c-4327-b3fd-338517be8161"), "Pramod Mahajan", "Pramod Mahajan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "INR", null, "codedev.in", "codedev90@gmail.com", false, true, "en-IN", "Code Dev", new DateTime(2026, 8, 27, 19, 12, 57, 636, DateTimeKind.Utc).AddTicks(5395), 1, null, null, null, null, "Asia/Kolkata", null });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRoles_Tenants_TenantId",
                table: "ApplicationRoles",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppRoleMaps_Tenants_TenantId",
                table: "EmployeeAppRoleMaps",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tenants_TenantId",
                table: "Employees",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRoles_Tenants_TenantId",
                table: "JobRoles",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
