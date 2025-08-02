using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixConversationCreatedAtDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Conversations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "CreatedAt", "PropertyId", "User1Id", "User2Id" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3dacdb51-fee9-4479-904c-cafe7dca22a7", "4dacdb51-fee9-4479-904c-cafe7dca22a8" });
        }
    }
}
