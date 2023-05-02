using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac09b6ef-d562-4be1-bfe2-9ecfde995690", null, "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3309c2bb-4ee6-4e7e-bdf1-160f7fea6ae7", 0, "82d15ab6-dcd4-4994-8cfd-2c992b0cf5d4", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEKqHMZI1v89VIEnIO3o04zu5P7z5yo0S9Tjf8RXTGZYV5mrRZyDYJTB+OqWK7pV1Xg==", null, false, "9e068e18-f874-4a47-ac13-55b785d7e0c1", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ac09b6ef-d562-4be1-bfe2-9ecfde995690", "3309c2bb-4ee6-4e7e-bdf1-160f7fea6ae7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ac09b6ef-d562-4be1-bfe2-9ecfde995690", "3309c2bb-4ee6-4e7e-bdf1-160f7fea6ae7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac09b6ef-d562-4be1-bfe2-9ecfde995690");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3309c2bb-4ee6-4e7e-bdf1-160f7fea6ae7");

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
    }
}
