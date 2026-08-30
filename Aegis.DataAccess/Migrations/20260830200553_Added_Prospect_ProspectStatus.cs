using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Added_Prospect_ProspectStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProspectsStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectsStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prospects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EstimatedValue = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ExpectedDecisionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrganizationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prospects_Employees_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prospects_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prospects_ProspectsStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ProspectsStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 30, 20, 5, 52, 508, DateTimeKind.Utc).AddTicks(8313));

            migrationBuilder.InsertData(
                table: "ProspectsStatus",
                columns: new[] { "Id", "Code", "Description", "DisplayOrder", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("1006820d-883e-4a5f-b607-79f368097f50"), "DISQUALIFIED", "Not a suitable business prospect", 7, true, "Disqualified" },
                    { new Guid("1d3cb190-8420-4abd-af1f-1d1fb051be83"), "FOLLOW_UP", "Waiting for a response / next interaction", 3, true, "Follow Up" },
                    { new Guid("41474b0b-659a-4086-af0e-52a3d885dce0"), "QUALIFICATION", "Salesperson is checking whether it is a genuine opportunity", 4, true, "Qualification" },
                    { new Guid("506e3866-7f08-4ca5-aaad-8125bc7bb9a7"), "DORMANT", "No current activity, but not rejected", 6, true, "Dormant" },
                    { new Guid("5a311c2d-671f-4d6a-bc66-d986cba061e7"), "NEW", "Salesperson is actively working on it", 1, true, "New" },
                    { new Guid("acb3fe0e-ab51-4303-87e9-26cb6806e81a"), "ACTIVE", "Salesperson is actively working on it", 2, true, "Active" },
                    { new Guid("be1dde6e-7e16-4b75-8420-ad4e65eaae30"), "QUALIFIED", "Project is confirmed as worth pursuing", 5, true, "Qualified" },
                    { new Guid("c5c108f2-a764-41e3-9e63-9afb7f21ea23"), "CONVERTED", "Prospect has been converted into an Opportunity", 8, true, "Converted" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_CreatedById",
                table: "Prospects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_OrganizationId",
                table: "Prospects",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_StatusId",
                table: "Prospects",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prospects");

            migrationBuilder.DropTable(
                name: "ProspectsStatus");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 28, 20, 33, 11, 658, DateTimeKind.Utc).AddTicks(3316));
        }
    }
}
