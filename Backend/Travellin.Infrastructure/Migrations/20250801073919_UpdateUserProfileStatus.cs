using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProfileStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserProfiles",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "Active"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserProfiles"
                );
        }
    }
}
