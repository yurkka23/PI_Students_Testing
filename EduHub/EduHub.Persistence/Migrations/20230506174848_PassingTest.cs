using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHub.Persistence.Migrations
{
    public partial class PassingTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_teacherRequests",
                table: "teacherRequests");

          

            migrationBuilder.RenameTable(
                name: "teacherRequests",
                newName: "TeacherRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherRequests",
                table: "TeacherRequests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PassingTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentStartedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StudentFinishedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassingTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassingTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_PassingTests_PassingTestId",
                        column: x => x.PassingTestId,
                        principalTable: "PassingTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

         

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_PassingTestId",
                table: "QuestionAnswers",
                column: "PassingTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "PassingTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherRequests",
                table: "TeacherRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c02beee-60fa-41f7-baaa-3a99c98ec8ad"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5afdd513-e261-43e8-ad19-300500dcd8dd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c7f02517-5a13-4e5d-a744-312efb666519"));

            migrationBuilder.RenameTable(
                name: "TeacherRequests",
                newName: "teacherRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teacherRequests",
                table: "teacherRequests",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1ed183d6-bc0a-41ec-881a-25beda4e986f"), "c63df6d7-ddf8-4f7c-b4b8-9f2a21cb2923", "teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("6386fefe-5a01-4fff-993b-66084327d861"), "0496daa5-a4ac-4a8e-9532-e58937c16138", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("93698adc-35d7-4f35-92e7-00918f11a0e7"), "befb9d68-e43f-4299-a41f-b3fac0d481ba", "student", "STUDENT" });
        }
    }
}
