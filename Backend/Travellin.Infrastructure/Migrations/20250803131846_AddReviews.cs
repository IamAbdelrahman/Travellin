using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewPeriodEnd",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewPeriodStart",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "2fca2c7e-263b-4d7e-99e7-0c1c3ad2aa08",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "66d2f0d9-1f1f-4a02-81d6-0ecabc5215e6",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "72b3d68d-234a-4ed7-b7f7-e07fc82f58ef",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "84c03e84-cd8b-4dbf-a0f4-48ed3dd0b0aa",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "a54b86b1-65e2-426b-81ef-c65c71e5b8d0",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "e62cd505-8d60-430b-8b52-16d40902a303",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "fca2e08b-0436-4f3f-8261-f69cf3eaa579",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "ffc234ae-2820-4fd6-b9d7-6b315d91a790",
                columns: new[] { "IsAnonymous", "IsPublic", "ReviewPeriodEnd", "ReviewPeriodStart", "Status", "Type" },
                values: new object[] { false, true, null, null, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewPeriodEnd",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewPeriodStart",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reviews");
        }
    }
}
