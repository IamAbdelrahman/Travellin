using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "0fe8f9f5-7751-460b-b39f-dab6946c0ba2",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "438d19e1-66fc-4219-9e3d-0519c9c27332",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "49b69c8a-8b4b-4021-85f4-ff273b70c85d",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "7b479ff7-22c5-46ad-85a3-204b502e5d0b",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "7f6b0bb5-e99e-47c7-8d75-b5d46284e241",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "b6d7b477-9b64-4a79-b7a3-b01c45378d5e",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "d2bc71b9-d703-43fc-a90f-bf22f29a7b4e",
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: "e42b9075-d67c-4b5f-8316-bde33ef7272a",
                column: "AppUserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AppUserId",
                table: "Bookings",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_AppUserId",
                table: "Bookings",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_AppUserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AppUserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Bookings");
        }
    }
}
