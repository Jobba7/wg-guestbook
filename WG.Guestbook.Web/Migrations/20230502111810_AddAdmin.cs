using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dfaf4ecb-bf0b-4b85-aebd-225cd4c7d1df");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59a6f57d-f425-4fca-b5a6-660119474022", null, "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9e9617b8-a581-4619-9b37-8338df4b98a5", 0, "d095778b-8611-489d-8cb9-5d3848284e9d", null, false, false, null, null, null, "AQAAAAIAAYagAAAAEDTeCgnB4KokjbyZFCs7nzl8I4VLvghLHQLXNaL77OmtJj4ZvvKPPl00wl45w2XTdg==", null, false, "8ecaca52-095f-4540-9b35-366134df973b", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "59a6f57d-f425-4fca-b5a6-660119474022", "9e9617b8-a581-4619-9b37-8338df4b98a5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "59a6f57d-f425-4fca-b5a6-660119474022", "9e9617b8-a581-4619-9b37-8338df4b98a5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59a6f57d-f425-4fca-b5a6-660119474022");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e9617b8-a581-4619-9b37-8338df4b98a5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dfaf4ecb-bf0b-4b85-aebd-225cd4c7d1df", 0, "09bbd97a-6b53-4e1e-bf53-7d2fa544a7dd", null, false, false, null, null, null, null, null, false, "85d6c5e6-765b-4e2b-a9f3-a1e6f8b99f68", false, "Admin" });
        }
    }
}
