using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyIdToConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "Conversations",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PropertyId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_PropertyId",
                table: "Conversations",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Properties_PropertyId",
                table: "Conversations",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Properties_PropertyId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_PropertyId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Conversations");
        }
    }
}
