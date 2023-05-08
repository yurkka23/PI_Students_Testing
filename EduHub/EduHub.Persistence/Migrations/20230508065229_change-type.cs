using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHub.Persistence.Migrations
{
    public partial class changetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "QuestionAnswers");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "QuestionAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "QuestionAnswers");

            migrationBuilder.AddColumn<Guid>(
                name: "AnswerId",
                table: "QuestionAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

          
        }
    }
}
