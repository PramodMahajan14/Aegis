using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adveshta.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changed_prospects_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProspectSource",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProspectTemperature",
                table: "Prospects");

            migrationBuilder.AddColumn<Guid>(
                name: "ProspectSourceId",
                table: "Prospects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProspectTemperatureId",
                table: "Prospects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ProspectSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectSources", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProspectTemperatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectTemperatures", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 12, 17, 50, 45, 983, DateTimeKind.Utc).AddTicks(1414));

            migrationBuilder.InsertData(
                table: "ProspectSources",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("1c2d94b1-88ca-4550-b368-b6aa70ea080b"), "CAMPAIGN ", "Campaign" },
                    { new Guid("2388fcd1-702c-4ba9-aa26-fd47e742126f"), "EXISTING_NETWORK", "Existing Network" },
                    { new Guid("31a6eb48-2cf6-47ff-89cc-9987dd34d0ed"), "MARKET_RESEARCH", "Market Research" },
                    { new Guid("4a358b8e-1517-4935-8c54-9be5f1975636"), "TELE_CALL", "Tele Call" },
                    { new Guid("4db35ae1-ec62-4b61-ab21-adc069e288c9"), "OTHER", "Other" },
                    { new Guid("96492d44-7145-4820-8e05-39be6b573a17"), "REFERRAL", "Referral" },
                    { new Guid("997ae18f-d94f-4b43-a39a-9049def7eb9a"), "SOCIAL_MEDIA ", "Social Media" },
                    { new Guid("a86d9b08-42d7-4e0e-aa3e-12ea6750c3be"), "COLD_CALL", "COLD CALL" },
                    { new Guid("e6f849db-fe3f-4c8d-9856-eadb6e3a356d"), "WEBSITE", "Web Site" },
                    { new Guid("eec202b2-cf01-4520-95e1-129d5267119b"), "SITE_VISIT", "Site Visit" }
                });

            migrationBuilder.InsertData(
                table: "ProspectTemperatures",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("1be20c30-5004-44bb-992d-1a8e0affd8a0"), "HOT", "Hot" },
                    { new Guid("da1b82ba-92f1-40a8-9a8c-9c317efe5c0a"), "WARM", "Warn" },
                    { new Guid("ddc27c6d-343b-483f-a492-62557fc694e5"), "COLD", "Cold" },
                    { new Guid("fa93281c-ce3e-4e58-965b-37e32c2c232e"), "NOT_SET", "Not Set" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_ProspectSourceId",
                table: "Prospects",
                column: "ProspectSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_ProspectTemperatureId",
                table: "Prospects",
                column: "ProspectTemperatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_ProspectSources_ProspectSourceId",
                table: "Prospects",
                column: "ProspectSourceId",
                principalTable: "ProspectSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_ProspectTemperatures_ProspectTemperatureId",
                table: "Prospects",
                column: "ProspectTemperatureId",
                principalTable: "ProspectTemperatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_ProspectSources_ProspectSourceId",
                table: "Prospects");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_ProspectTemperatures_ProspectTemperatureId",
                table: "Prospects");

            migrationBuilder.DropTable(
                name: "ProspectSources");

            migrationBuilder.DropTable(
                name: "ProspectTemperatures");

            migrationBuilder.DropIndex(
                name: "IX_Prospects_ProspectSourceId",
                table: "Prospects");

            migrationBuilder.DropIndex(
                name: "IX_Prospects_ProspectTemperatureId",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProspectSourceId",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ProspectTemperatureId",
                table: "Prospects");

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

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 9, 8, 19, 52, 0, 263, DateTimeKind.Utc).AddTicks(6296));
        }
    }
}
