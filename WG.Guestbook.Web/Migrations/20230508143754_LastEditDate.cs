using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class LastEditDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a332336-0be3-441b-8262-a94400ae6834");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd3e9206-d2c1-40b6-ada2-c395d9d2f402");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "43933f7d-b4f1-49c6-a054-a7e9153635a3", "57a633b7-c1b0-487c-881d-eba774e26b39" });

            migrationBuilder.DeleteData(
                table: "Entries",
                keyColumn: "Id",
                keyValue: "b46e6cdd-1b79-403a-87e7-2b1f98b65fcb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43933f7d-b4f1-49c6-a054-a7e9153635a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57a633b7-c1b0-487c-881d-eba774e26b39");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditDate",
                table: "Entries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00146569-45c1-4b2e-b259-2dc89cca2c02", null, "Admin", "ADMIN" },
                    { "5bf3a8a1-50bb-4003-ba7e-127e3d221ec7", null, "Guest", "GUEST" },
                    { "963d2479-936e-49d8-b49e-dd2aa73cbb92", null, "Roommate", "ROOMMATE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "77f53fdb-d19e-4b10-8791-209c6b8ed157", 0, "e597a67c-f8dc-4d73-9d32-61f6f02c82c0", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEEtCB/bsyeag1XMHoCR6f47ZaOVwwnRXnEueGNrdltAXLD82QgDxQs5bXsjwnByp2w==", null, false, "7db93378-0b1c-4c83-a7e8-6f38d9f25404", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "00146569-45c1-4b2e-b259-2dc89cca2c02", "77f53fdb-d19e-4b10-8791-209c6b8ed157" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bf3a8a1-50bb-4003-ba7e-127e3d221ec7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "963d2479-936e-49d8-b49e-dd2aa73cbb92");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00146569-45c1-4b2e-b259-2dc89cca2c02", "77f53fdb-d19e-4b10-8791-209c6b8ed157" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00146569-45c1-4b2e-b259-2dc89cca2c02");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77f53fdb-d19e-4b10-8791-209c6b8ed157");

            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "Entries");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43933f7d-b4f1-49c6-a054-a7e9153635a3", null, "Admin", "ADMIN" },
                    { "6a332336-0be3-441b-8262-a94400ae6834", null, "Guest", "GUEST" },
                    { "cd3e9206-d2c1-40b6-ada2-c395d9d2f402", null, "Roommate", "ROOMMATE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "57a633b7-c1b0-487c-881d-eba774e26b39", 0, "a90de4b6-da3b-4bde-83bd-1f858a929b62", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEKIv/PMaY3+5r3+s4RG2GX9oj4tXm7Y+ANBlgynvYiAYsP/r/cejvSfAUsKhuiz5eg==", null, false, "1e24fb3c-d990-48d3-bd8c-f1b5e350323a", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "43933f7d-b4f1-49c6-a054-a7e9153635a3", "57a633b7-c1b0-487c-881d-eba774e26b39" });

            migrationBuilder.InsertData(
                table: "Entries",
                columns: new[] { "Id", "AuthorId", "Content", "CreateDate", "VisitDate" },
                values: new object[] { "b46e6cdd-1b79-403a-87e7-2b1f98b65fcb", "57a633b7-c1b0-487c-881d-eba774e26b39", "Hello World!", new DateTime(2023, 1, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
