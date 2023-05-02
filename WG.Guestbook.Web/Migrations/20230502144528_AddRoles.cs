using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { "34aa7a1f-58bd-4cf5-b6dc-fa772608507b", null, "Admin", "ADMIN" },
                    { "4461c996-6579-4f97-847a-48b1f1825ef4", null, "Roommate", "ROOMMATE" },
                    { "8a95ea6f-8095-4526-95b6-ea1ed4260752", null, "Guest", "GUEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e1940b25-256d-491c-8c6b-9f7bbf566135", 0, "c96913ea-be5d-41d4-95a6-ab4ce8a4950d", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEFqdojHWOKHFHE3UMvwhPhSvdbTDbyKbpfEhlidDRlcirUvN8i1Yjm1nbpmuXtfxrw==", null, false, "e815946b-cad9-44c1-bd75-25fea01cbc39", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "34aa7a1f-58bd-4cf5-b6dc-fa772608507b", "e1940b25-256d-491c-8c6b-9f7bbf566135" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4461c996-6579-4f97-847a-48b1f1825ef4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a95ea6f-8095-4526-95b6-ea1ed4260752");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "34aa7a1f-58bd-4cf5-b6dc-fa772608507b", "e1940b25-256d-491c-8c6b-9f7bbf566135" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34aa7a1f-58bd-4cf5-b6dc-fa772608507b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1940b25-256d-491c-8c6b-9f7bbf566135");

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
    }
}
