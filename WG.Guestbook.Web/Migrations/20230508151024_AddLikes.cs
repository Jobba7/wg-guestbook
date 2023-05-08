using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WG.Guestbook.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_AspNetUsers_AuthorId",
                table: "Entries");

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

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Entries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "EntryUser",
                columns: table => new
                {
                    LikedEntriesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryUser", x => new { x.LikedEntriesId, x.LikesId });
                    table.ForeignKey(
                        name: "FK_EntryUser_AspNetUsers_LikesId",
                        column: x => x.LikesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryUser_Entries_LikedEntriesId",
                        column: x => x.LikedEntriesId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e3c251a-e5d5-4fd5-a258-340042ca332a", null, "Roommate", "ROOMMATE" },
                    { "5015e9e3-dda1-41a1-8b06-da63440e6d42", null, "Admin", "ADMIN" },
                    { "f3d953aa-d289-4f26-a320-af39da386273", null, "Guest", "GUEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8417bc14-e07e-4a94-8867-bba480262217", 0, "272ed563-6048-4725-8d0c-679e5820c546", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEBolo4YEaN/JuQ4OUygVGo/pRKk/uH/UcgNGpew6TpQdHltu4nYcoLzr5QXV71IixA==", null, false, "667035bf-4ddb-48ca-946d-c5fbd07c0da7", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5015e9e3-dda1-41a1-8b06-da63440e6d42", "8417bc14-e07e-4a94-8867-bba480262217" });

            migrationBuilder.CreateIndex(
                name: "IX_EntryUser_LikesId",
                table: "EntryUser",
                column: "LikesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_AspNetUsers_AuthorId",
                table: "Entries",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_AspNetUsers_AuthorId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "EntryUser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e3c251a-e5d5-4fd5-a258-340042ca332a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3d953aa-d289-4f26-a320-af39da386273");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5015e9e3-dda1-41a1-8b06-da63440e6d42", "8417bc14-e07e-4a94-8867-bba480262217" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5015e9e3-dda1-41a1-8b06-da63440e6d42");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8417bc14-e07e-4a94-8867-bba480262217");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Entries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_AspNetUsers_AuthorId",
                table: "Entries",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
