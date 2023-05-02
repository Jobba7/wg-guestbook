using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "1cb86bb5-8953-47eb-9ac1-d246ebef5778", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8ad5f657-ea6e-4df7-b7b2-d847baf75f52", 0, "36255d49-666d-4d6b-a5ef-d8641ee33cc0", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEKhZP2jOWmh+P7e0NeYSTRRvJdsGO+w83eMZ/5l8xnalXHQSSzaNmJb7W+5nTx5sgw==", null, false, "761b6dd7-ff55-4599-bb95-f6a12454d2a1", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1cb86bb5-8953-47eb-9ac1-d246ebef5778", "8ad5f657-ea6e-4df7-b7b2-d847baf75f52" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1cb86bb5-8953-47eb-9ac1-d246ebef5778", "8ad5f657-ea6e-4df7-b7b2-d847baf75f52" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cb86bb5-8953-47eb-9ac1-d246ebef5778");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8ad5f657-ea6e-4df7-b7b2-d847baf75f52");

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
    }
}
