using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHub.Persistence.Migrations
{
    public partial class seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b5314384-a039-4a7e-b896-8016203cfe58"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33a0ef2c-7da6-45a6-ae36-c9e3dce66c66"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("85ca209b-5904-4135-9ee4-833ab5900e70"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("33a0ef2c-7da6-45a6-ae36-c9e3dce66c66"), "054d2aa9-24a9-423f-9248-479886d22e77", "admin", "ADMIN" },
                    { new Guid("85ca209b-5904-4135-9ee4-833ab5900e70"), "b3cf4120-d9ef-43c1-8590-43b91a42fbf5", "teacher", "TEACHER" },
                    { new Guid("b5314384-a039-4a7e-b896-8016203cfe58"), "1d40614b-381f-4a24-a268-b2d2fe9732d9", "student", "STUDENT" }
                });

 

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisterTime", "SecurityStamp", "TwoFactorEnabled", "UserImgUrl", "UserName", "VerificationCode", "VerificationExpires" },
                values: new object[] { new Guid("8e445865-a24d-4543-a6c6-9443d048cdb8"), null, 0, "3933b6f9-affa-4c92-8691-4a4a1d0e027e", "admin2@gmail.com", true, "admin2", "admin", false, null, "ADMIN2@GMAIL.COM", "ADMIN2", "AQAAAAEAACcQAAAAEDbv2gdENQQEj74VQ3pFfKXFJmAUYXlYNRuIcXMz/qFC2aIFabazxJVkWBgHDCuIvQ==", null, false, new DateTimeOffset(new DateTime(2023, 5, 14, 12, 35, 54, 478, DateTimeKind.Unspecified).AddTicks(3944), new TimeSpan(0, 0, 0, 0, 0)), "2FIUSIDWWXNH7N6KXWVZFFGAICGDPTX7", false, null, "admin2", null, null });

            migrationBuilder.InsertData(
              table: "AspNetUserRoles",
              columns: new[] { "RoleId", "UserId", "Discriminator" },
              values: new object[] { new Guid("33a0ef2c-7da6-45a6-ae36-c9e3dce66c66"), new Guid("8e445865-a24d-4543-a6c6-9443d048cdb8"), "IdentityUserRole<Guid>" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33a0ef2c-7da6-45a6-ae36-c9e3dce66c66"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("85ca209b-5904-4135-9ee4-833ab5900e70"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b5314384-a039-4a7e-b896-8016203cfe58"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("33a0ef2c-7da6-45a6-ae36-c9e3dce66c66"), new Guid("8e445865-a24d-4543-a6c6-9443d048cdb8") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb8"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("4078fbc5-aac2-4ee0-893e-c17b73b0f0b9"), "5dd8ac1d-0407-4bc4-98c2-4703dec9fb14", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("945ae692-7677-474a-a51d-d1dbc66b5156"), "32e9aae9-9434-4b7b-8fd5-be708650becc", "student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("f6b7c0ef-09ea-4301-a2ee-627a66f95b18"), "918514e3-ccf2-4e82-93ca-3ab3325182be", "teacher", "TEACHER" });
        }
    }
}
