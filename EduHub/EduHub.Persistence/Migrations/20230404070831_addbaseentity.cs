using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHub.Persistence.Migrations
{
    public partial class addbaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "teacherRequests",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "teacherRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "teacherRequests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "teacherRequests",
                type: "uniqueidentifier",
                nullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "teacherRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "teacherRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "teacherRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "teacherRequests");

         
        }
    }
}
