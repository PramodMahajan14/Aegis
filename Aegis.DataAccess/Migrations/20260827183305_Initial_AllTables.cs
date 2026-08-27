using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial_AllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsRootUser = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tenants",
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
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSystem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JobRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FeaturePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FeatureId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DefaultValue = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturePermissions_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSystem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    JobRoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_JobRoles_JobRoleId",
                        column: x => x.JobRoleId,
                        principalTable: "JobRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplicationRolePermissons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ApplicationRoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FeaturePermissionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRolePermissons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermissons_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermissons_FeaturePermissions_FeaturePermissi~",
                        column: x => x.FeaturePermissionId,
                        principalTable: "FeaturePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeAppRoleMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppRoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AssignedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UnassignedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UnassignedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppRoleMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAppRoleMaps_ApplicationRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppRoleMaps_Employees_AssignedById",
                        column: x => x.AssignedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppRoleMaps_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppRoleMaps_Employees_UnassignedById",
                        column: x => x.UnassignedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppRoleMaps_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Description", "Key", "Name" },
                values: new object[,]
                {
                    { new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Manage field visits and customer visits.", "site_visits", "Site Visits" },
                    { new Guid("1f65bb9f-4f28-4530-99f1-e20cbaf9b344"), "Manage people and customer relationships.", "contacts", "Contacts" },
                    { new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Manage potential customers and business opportunities.", "leads", "Leads" },
                    { new Guid("78d7bee8-a5d0-4689-91f7-2b350f22696a"), "Manage assigned work and follow-up activities.", "tasks", "Tasks" },
                    { new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Analyze CRM data and business performance.", "reports", "Reports" },
                    { new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Track interactions and business activities.", "activities", "Activities" },
                    { new Guid("93d57136-92fc-4a0c-9f36-9fd9f85e23b5"), "Manage campaigns and lead acquisition activities.", "campaigns", "Campaigns" },
                    { new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Capture and manage business and customer requirements.", "requirements", "Requirements" },
                    { new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Manage qualified business opportunities through their lifecycle.", "opportunities", "Opportunities" },
                    { new Guid("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc"), "Schedule and manage business meetings.", "meetings", "Meetings" },
                    { new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Manage business pipelines, stages and opportunity progression.", "pipeline", "Pipeline" },
                    { new Guid("c6167d9f-0c7f-4271-a325-cbbcb42c0298"), "Manage documents associated with CRM records.", "documents", "Documents" },
                    { new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Manage potential customers and business opportunities.", "client", "Clients" },
                    { new Guid("fe25290f-1402-4401-8933-c9aad35dfaec"), "Manage CRM notifications and reminders.", "notifications", "Notifications" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "ContactNumber", "ContactPerson", "CreatedAt", "Currency", "DeactivatedAt", "DomainName", "Email", "IsActive", "IsSystemTenant", "Locale", "Name", "OnboardingDate", "Status", "SubscriptionEndDate", "SubscriptionPlanId", "SubscriptionStartDate", "SuspendedAt", "TimeZone", "UpdatedAt" },
                values: new object[] { new Guid("c84c9fae-750c-4327-b3fd-338517be8161"), "Pramod Mahajan", "Pramod Mahajan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "INR", null, "codedev.in", "codedev90@gmail.com", false, true, "en-IN", "Code Dev", new DateTime(2026, 8, 27, 18, 33, 5, 267, DateTimeKind.Utc).AddTicks(6976), 1, null, null, null, null, "Asia/Kolkata", null });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Description", "IsActive", "Key", "ModuleId", "Name" },
                values: new object[,]
                {
                    { new Guid("01a7613f-e840-4932-a3da-5cdafeb43198"), "Create, view, update and manage campaigns.", false, "campaign_management", new Guid("93d57136-92fc-4a0c-9f36-9fd9f85e23b5"), "Campaign Management" },
                    { new Guid("04f070f0-4449-47c4-8f10-e9eb319cc4d1"), "Track changes and movement across pipeline stages.", false, "pipeline_history", new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Pipeline History" },
                    { new Guid("06a470df-66d0-46e3-a7e4-407be3c6205f"), "Generate reports for sales activities.", false, "activity_reports", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Activity Reports" },
                    { new Guid("0cc30bd0-22a2-49f6-8bb2-4747283a5b63"), "Export reports in supported formats.", false, "report_export", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Report Export" },
                    { new Guid("113179e6-2038-4ba4-ab22-47e131e80b75"), "View and manage pipeline records using a Kanban board.", false, "pipeline_kanban", new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Pipeline Kanban" },
                    { new Guid("11e87daf-0b2d-474f-91f8-1f639529bcac"), "Generate reports related to pipeline performance.", false, "pipeline_reports", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Pipeline Reports" },
                    { new Guid("128b4c10-2bd0-4f30-971c-6f51e3891eb7"), "Record notes and observations from site visits.", false, "site_visit_notes", new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Site Visit Notes" },
                    { new Guid("160bd4df-d18e-405d-989b-b5bdba1e8528"), "View the history and timeline of contact interactions.", false, "contact_timeline", new Guid("1f65bb9f-4f28-4530-99f1-e20cbaf9b344"), "Contact Timeline" },
                    { new Guid("17e69f35-bf9a-4b68-815a-16ab7d8da183"), "Create and manage requirement questions.", false, "requirement_questions", new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Requirement Questions" },
                    { new Guid("19c4c6af-fd77-4b97-99ee-de1583dcdb38"), "Manage checklists for site visits.", false, "site_visit_checklists", new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Site Visit Checklists" },
                    { new Guid("1a7b5f4a-68bb-48f7-b4b9-d8383ae23d4c"), "Convert qualified leads into opportunities and contacts.", false, "lead_conversion", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Lead Conversion" },
                    { new Guid("1bd1b732-4d31-4be4-a23a-e2b08619bcc2"), "Create and manage email activities.", false, "email_activity", new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Email Activity" },
                    { new Guid("2353aac8-0935-423d-8ee9-b6981f30111a"), "Track campaign attribution and lead sources.", false, "campaign_attribution", new Guid("93d57136-92fc-4a0c-9f36-9fd9f85e23b5"), "Campaign Attribution" },
                    { new Guid("26414dc5-a6ba-4825-87c1-db1636873ce8"), "Create, view, update and manage activities.", false, "activity_management", new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Activity Management" },
                    { new Guid("27a56e31-5a1a-4992-b01f-1e309220e9cb"), "Manage relationships between contacts and business records.", false, "contact_relationships", new Guid("1f65bb9f-4f28-4530-99f1-e20cbaf9b344"), "Contact Relationships" },
                    { new Guid("27f42f6e-dbd4-4285-b366-9f7cd2bf43d8"), "Forecast pipeline performance and expected revenue.", false, "pipeline_forecasting", new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Pipeline Forecasting" },
                    { new Guid("2aa7e974-0721-4837-8c4c-4c6f35b565a7"), "Manage document versions and revisions.", false, "document_versioning", new Guid("c6167d9f-0c7f-4271-a325-cbbcb42c0298"), "Document Versioning" },
                    { new Guid("2b3a902c-4357-4f28-ad7a-474ed9623981"), "Forecast opportunity revenue and expected closures.", false, "opportunity_forecasting", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Forecasting" },
                    { new Guid("2e54a66d-9100-4dc1-8cf2-4db40c4a927e"), "Configure reminders for scheduled meetings.", false, "meeting_reminders", new Guid("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc"), "Meeting Reminders" },
                    { new Guid("3a5f1730-e80a-4502-80d3-9cb07d30891b"), "Create, view, update and manage client activites.", false, "client_activities", new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Client Activities" },
                    { new Guid("3c2e8541-4273-42c3-bfef-5492454edeee"), "Assign tasks to users and teams.", false, "task_assignment", new Guid("78d7bee8-a5d0-4689-91f7-2b350f22696a"), "Task Assignment" },
                    { new Guid("450a0e10-1f35-4883-b2a2-1cc45110b6af"), "Manage probability of opportunity closure.", false, "opportunity_probability", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Probability" },
                    { new Guid("48795578-ec33-4f11-b620-c990a79a0bcd"), "Record and manage meeting notes.", false, "meeting_notes", new Guid("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc"), "Meeting Notes" },
                    { new Guid("491e70af-d8bc-41ad-983c-0c38b24cebb5"), "Manage meeting attendees and participants.", false, "meeting_attendees", new Guid("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc"), "Meeting Attendees" },
                    { new Guid("4a0882ae-2e67-4484-8f4b-c0954b730364"), "Schedule reports for automatic generation and delivery.", false, "scheduled_reports", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Scheduled Reports" },
                    { new Guid("50ab36d8-5211-495d-88a9-db3de9d7fd2f"), "Create, view, update and manage opportunities.", false, "opportunity_management", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Management" },
                    { new Guid("569d198b-93e8-490f-a419-ec604c23487a"), "Manage checklists within tasks.", false, "task_checklist", new Guid("78d7bee8-a5d0-4689-91f7-2b350f22696a"), "Task Checklist" },
                    { new Guid("5ca16678-e0d3-4bf4-a4fe-36cd965526a8"), "Create, view, update and manage site visits.", false, "site_visit_management", new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Site Visit Management" },
                    { new Guid("5d3033e0-7909-4bef-b920-ed6a4c0f4764"), "Organize requirements into sections.", false, "requirement_sections", new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Requirement Sections" },
                    { new Guid("5f2bad79-cef6-4a48-bd7e-d935c767abcf"), "Configure and manage pipeline stages.", false, "pipeline_stages", new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Pipeline Stages" },
                    { new Guid("6030081d-3ab0-4f5d-a477-b357cfc7f6b6"), "Upload and manage documents.", false, "document_upload", new Guid("c6167d9f-0c7f-4271-a325-cbbcb42c0298"), "Document Upload" },
                    { new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"), "Create, view, update and manage client.", false, "client_management", new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Client Management" },
                    { new Guid("62e0172e-32d8-4d49-b680-3dea19c05281"), "Record site visit check-in and check-out.", false, "site_visit_checkin", new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Site Visit Check-in" },
                    { new Guid("6327f4df-4a2e-4268-962a-481c1ab9b93e"), "Capture leads from different sources.", false, "lead_capture", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Lead Capture" },
                    { new Guid("6b97f241-5b4f-4e79-a769-d3291f31dc19"), "Create, view, update and manage tasks.", false, "task_management", new Guid("78d7bee8-a5d0-4689-91f7-2b350f22696a"), "Task Management" },
                    { new Guid("6eb3c2c6-cf82-4b7d-9aed-e41c49b62741"), "Manage reminders for upcoming activities and tasks.", false, "reminder_notifications", new Guid("fe25290f-1402-4401-8933-c9aad35dfaec"), "Reminder Notifications" },
                    { new Guid("6f25bf12-7b11-44bc-aecc-3f03b9567bd8"), "Create and configure reports.", false, "report_builder", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Report Builder" },
                    { new Guid("775c0750-9dae-4bf7-a5e0-c77c50d0866a"), "Manage opportunity stages and progression.", false, "opportunity_stages", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Stages" },
                    { new Guid("7d41fe16-f08f-48a8-a7e1-0873572f8a90"), "Create, view and manage sales pipelines.", false, "pipeline_management", new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Pipeline Management" },
                    { new Guid("7e730dd8-42f3-4382-8ff2-a5b6b042630d"), "Create and manage meeting agendas.", false, "meeting_agenda", new Guid("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc"), "Meeting Agenda" },
                    { new Guid("7ff9cbed-25e1-4bcb-adee-391be9e558e1"), "Manage campaign sources and acquisition channels.", false, "campaign_sources", new Guid("93d57136-92fc-4a0c-9f36-9fd9f85e23b5"), "Campaign Sources" },
                    { new Guid("83144137-5268-4bc5-953f-df5223933abb"), "Generate reports for lead and opportunity conversions.", false, "conversion_reports", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Conversion Reports" },
                    { new Guid("83e49858-6246-4b88-b9f3-36fe5021544c"), "Filter report data using configurable criteria.", false, "report_filters", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Report Filters" },
                    { new Guid("88293270-5873-4b8f-a4e4-1065ce494ae0"), "Create, view, update and manage contacts.", false, "contact_management", new Guid("1f65bb9f-4f28-4530-99f1-e20cbaf9b344"), "Contact Management" },
                    { new Guid("922624d0-7daf-4917-a535-61885a6d32f1"), "Create and manage requirement templates.", false, "requirement_templates", new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Requirement Templates" },
                    { new Guid("932f5a14-0cc3-4012-b030-926578c48017"), "Define and manage campaign audiences.", false, "campaign_audiences", new Guid("93d57136-92fc-4a0c-9f36-9fd9f85e23b5"), "Campaign Audiences" },
                    { new Guid("94b04436-fccd-46ae-847f-04edfc5da6c7"), "Manage opportunity value and expected revenue.", false, "opportunity_value", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Value" },
                    { new Guid("9ce21b3c-baf9-4ebe-9219-ff08cae22f1b"), "Create, view, update and manage leads.", false, "lead_management", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Lead Management" },
                    { new Guid("9d31cf55-8f81-4ff8-9548-acf8e6ab31a4"), "Display report data using charts and visualizations.", false, "report_charts", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Report Charts" },
                    { new Guid("9e70fec6-9a25-48a2-b21a-cce5c17c7d3e"), "Notify users when they are mentioned in records or activities.", false, "mention_notifications", new Guid("fe25290f-1402-4401-8933-c9aad35dfaec"), "Mention Notifications" },
                    { new Guid("a104ef97-1766-45af-bd54-b9df4118d165"), "Create and manage call activities.", false, "call_activity", new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Call Activity" },
                    { new Guid("a767bedb-1089-4a1a-b5d2-03205885262a"), "Qualify leads based on business criteria.", false, "lead_qualification", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Lead Qualification" },
                    { new Guid("ab456a9c-806e-4f0c-92d9-1e8e136f5e5f"), "Manage task statuses.", false, "task_status", new Guid("78d7bee8-a5d0-4689-91f7-2b350f22696a"), "Task Status" },
                    { new Guid("ad89af4a-c7f3-45a3-8426-df1cff0f4f21"), "Manage requirements associated with opportunities.", false, "opportunity_requirements", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Requirements" },
                    { new Guid("afe14019-14bb-4804-aa35-b78b7fe378f4"), "Create and manage notes and activity notes.", false, "note_activity", new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Note Activity" },
                    { new Guid("b14e3341-795c-416a-9d65-d25f4de688b0"), "Manage lead sources and acquisition channels.", false, "lead_sources", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Lead Sources" },
                    { new Guid("b4ae0775-f77c-45bd-8329-621f515bb9d8"), "Share documents with authorized users.", false, "document_sharing", new Guid("c6167d9f-0c7f-4271-a325-cbbcb42c0298"), "Document Sharing" },
                    { new Guid("b4e7c404-78f8-4a9d-8471-6174dd8e5ce3"), "Create, view, update and manage meetings.", false, "meeting_management", new Guid("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc"), "Meeting Management" },
                    { new Guid("b528b3e4-ca9b-4e0a-adfe-32f5722a6992"), "Manage activities related to opportunities.", false, "opportunity_activities", new Guid("a009cd33-faf4-4e8a-bde4-fc2562c2198e"), "Opportunity Activities" },
                    { new Guid("b54fe2e2-3563-48e8-aeda-509e52030007"), "Create and manage meeting activities.", false, "meeting_activity", new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Meeting Activity" },
                    { new Guid("b837a859-ed3d-4c03-95a1-72864e4fb2eb"), "Create, view, update and manage documents.", false, "document_management", new Guid("c6167d9f-0c7f-4271-a325-cbbcb42c0298"), "Document Management" },
                    { new Guid("babc7309-b920-41ea-b3fa-57957a683c3e"), "Generate reports for team performance.", false, "team_reports", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Team Reports" },
                    { new Guid("bb7972f1-63b0-4c6f-859c-7bf28abf98fc"), "Analyze campaign performance and results.", false, "campaign_performance", new Guid("93d57136-92fc-4a0c-9f36-9fd9f85e23b5"), "Campaign Performance" },
                    { new Guid("be652534-efba-466a-98a7-372ce2846460"), "Create and manage custom activity types.", false, "custom_activity", new Guid("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a"), "Custom Activity" },
                    { new Guid("c3fe64f5-1604-4d26-9477-a0039aa58004"), "Group report data by selected fields.", false, "report_grouping", new Guid("82d77160-b060-480d-9d22-244a3b011b8b"), "Report Grouping" },
                    { new Guid("c84e8997-8763-410f-b5a9-80145d12515c"), "Manage documents associated with contacts.", false, "contact_documents", new Guid("1f65bb9f-4f28-4530-99f1-e20cbaf9b344"), "Contact Documents" },
                    { new Guid("ca63a567-711a-4d00-a8dc-4377aed508c6"), "Manage task priorities.", false, "task_priority", new Guid("78d7bee8-a5d0-4689-91f7-2b350f22696a"), "Task Priority" },
                    { new Guid("ccd21a8b-e3c7-49ae-80a6-284dc1317b3d"), "Manage multiple sales pipelines.", false, "multiple_pipelines", new Guid("c50e290a-672b-430a-8dad-58fbb574415a"), "Multiple Pipelines" },
                    { new Guid("d39f9687-8482-4fba-adf2-b269a09400e7"), "Upload and manage requirement attachments.", false, "requirement_attachments", new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Requirement Attachments" },
                    { new Guid("d5ca37f4-5a2d-44f2-8415-62aeb2d58893"), "Manage follow-up actions after site visits.", false, "site_visit_followup", new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Site Visit Follow-up" },
                    { new Guid("d84def16-0452-4869-bff8-c7e8578223b2"), "Create, view and manage notifications.", false, "notification_management", new Guid("fe25290f-1402-4401-8933-c9aad35dfaec"), "Notification Management" },
                    { new Guid("e0e0dcac-9dda-4ba1-82b5-94629f54dc08"), "Upload and manage photos and media from site visits.", false, "site_visit_media", new Guid("0a539ca7-0233-49f9-b969-c841f7d3c43b"), "Site Visit Media" },
                    { new Guid("e1900440-3625-41ed-bce6-eec1f924d3df"), "Capture and manage responses to requirements.", false, "requirement_responses", new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Requirement Responses" },
                    { new Guid("e3252074-231b-49f3-8286-1fb2220c0c85"), "Notify users when records or tasks are assigned.", false, "assignment_notifications", new Guid("fe25290f-1402-4401-8933-c9aad35dfaec"), "Assignment Notifications" },
                    { new Guid("e9f2a460-0cac-4153-83b3-f0ec1a9c4c01"), "Score and prioritize leads.", false, "lead_scoring", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Lead Scoring" },
                    { new Guid("ee528c96-89ee-4728-9f27-ade7a179d405"), "Detect and manage duplicate leads.", false, "lead_duplicate_detection", new Guid("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638"), "Duplicate Detection" },
                    { new Guid("eea8aad9-986e-4372-89ee-9027c0c6ded5"), "Create, view, update and manage client relation.", false, "client_relationship", new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Client Relationship" },
                    { new Guid("f974510b-51b1-4e1b-9626-c288ee965dee"), "Manage activities related to contacts.", false, "contact_activities", new Guid("1f65bb9f-4f28-4530-99f1-e20cbaf9b344"), "Contact Activities" },
                    { new Guid("fecbedae-c392-499b-bc9c-ac9dc0a23eaf"), "Create, view, update and manage requirements.", false, "requirement_management", new Guid("97a3673c-5760-448a-b248-e3688ad193fb"), "Requirement Management" }
                });

            migrationBuilder.InsertData(
                table: "FeaturePermissions",
                columns: new[] { "Id", "DefaultValue", "Description", "FeatureId", "IsEnabled", "Key", "Name" },
                values: new object[,]
                {
                    { new Guid("1030e239-23a1-47c7-bdb6-92697d119ecf"), true, "Update Client", new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"), true, "client.update", "Client Update" },
                    { new Guid("90d434d9-e3cb-4c79-b155-b1101a584f32"), true, "Create Client", new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"), true, "client.create", "Client Create" },
                    { new Guid("b0d0e81f-7ca5-49e4-81f9-52174674c766"), true, "View Client", new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"), true, "client.view", "Client View" },
                    { new Guid("b67d67a8-1acf-4cef-b15d-0bda20ab0212"), true, "Delete Client", new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"), true, "client.update", "Client Delete" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissons_ApplicationRoleId",
                table: "ApplicationRolePermissons",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissons_FeaturePermissionId",
                table: "ApplicationRolePermissons",
                column: "FeaturePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoles_TenantId",
                table: "ApplicationRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppRoleMaps_AppRoleId",
                table: "EmployeeAppRoleMaps",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppRoleMaps_AssignedById",
                table: "EmployeeAppRoleMaps",
                column: "AssignedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppRoleMaps_EmployeeId",
                table: "EmployeeAppRoleMaps",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppRoleMaps_TenantId",
                table: "EmployeeAppRoleMaps",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppRoleMaps_UnassignedById",
                table: "EmployeeAppRoleMaps",
                column: "UnassignedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobRoleId",
                table: "Employees",
                column: "JobRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TenantId",
                table: "Employees",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeaturePermissions_FeatureId",
                table: "FeaturePermissions",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ModuleId",
                table: "Features",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoles_TenantId",
                table: "JobRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRolePermissons");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeAppRoleMaps");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "FeaturePermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JobRoles");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
