using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aegis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seed_permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "ApplicationRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Description", "Key", "Name" },
                values: new object[] { new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Manage potential customers and business opportunities.", "client", "Clients" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 24, 17, 41, 56, 676, DateTimeKind.Utc).AddTicks(8739));

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Description", "IsActive", "Key", "ModuleId", "Name" },
                values: new object[,]
                {
                    { new Guid("3a5f1730-e80a-4502-80d3-9cb07d30891b"), "Create, view, update and manage client activites.", false, "client_activities", new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Client Activities" },
                    { new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"), "Create, view, update and manage client.", false, "client_management", new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Client Management" },
                    { new Guid("eea8aad9-986e-4372-89ee-9027c0c6ded5"), "Create, view, update and manage client relation.", false, "client_relationship", new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"), "Client Relationship" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeaturePermissions",
                keyColumn: "Id",
                keyValue: new Guid("1030e239-23a1-47c7-bdb6-92697d119ecf"));

            migrationBuilder.DeleteData(
                table: "FeaturePermissions",
                keyColumn: "Id",
                keyValue: new Guid("90d434d9-e3cb-4c79-b155-b1101a584f32"));

            migrationBuilder.DeleteData(
                table: "FeaturePermissions",
                keyColumn: "Id",
                keyValue: new Guid("b0d0e81f-7ca5-49e4-81f9-52174674c766"));

            migrationBuilder.DeleteData(
                table: "FeaturePermissions",
                keyColumn: "Id",
                keyValue: new Guid("b67d67a8-1acf-4cef-b15d-0bda20ab0212"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("3a5f1730-e80a-4502-80d3-9cb07d30891b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("eea8aad9-986e-4372-89ee-9027c0c6ded5"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("604fb10f-8b94-45d4-a2fc-914a4e4fe021"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd"));

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "ApplicationRoles");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c84c9fae-750c-4327-b3fd-338517be8161"),
                column: "OnboardingDate",
                value: new DateTime(2026, 8, 21, 20, 24, 34, 178, DateTimeKind.Utc).AddTicks(3415));
        }
    }
}
