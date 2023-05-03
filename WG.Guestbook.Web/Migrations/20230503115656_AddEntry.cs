using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c11ac09-0227-4e95-bf38-6d8da15b9359");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec885eeb-ac95-45ac-a75d-00a74b080e51");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b55a24b3-85d4-45e3-a804-45885315db11", "44ef2116-1556-4901-95b2-75796c5d52c5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b55a24b3-85d4-45e3-a804-45885315db11");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44ef2116-1556-4901-95b2-75796c5d52c5");

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Entries_AuthorId",
                table: "Entries",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43933f7d-b4f1-49c6-a054-a7e9153635a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57a633b7-c1b0-487c-881d-eba774e26b39");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c11ac09-0227-4e95-bf38-6d8da15b9359", null, "Roommate", "ROOMMATE" },
                    { "b55a24b3-85d4-45e3-a804-45885315db11", null, "Admin", "ADMIN" },
                    { "ec885eeb-ac95-45ac-a75d-00a74b080e51", null, "Guest", "GUEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "44ef2116-1556-4901-95b2-75796c5d52c5", 0, "bd03275a-40d6-4731-a48a-5d0df9425d4a", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEIGLiw3CxT6IuDAvU0upllR/F8rs+bFP1LI2bhHL6I8qlDInHSLYbBQbhFjtNCIBkQ==", null, false, "eedf8b8a-8daa-475a-90bd-c228fc77a860", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b55a24b3-85d4-45e3-a804-45885315db11", "44ef2116-1556-4901-95b2-75796c5d52c5" });
        }
    }
}
