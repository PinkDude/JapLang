using Microsoft.EntityFrameworkCore.Migrations;

namespace JapLang.Migrations
{
    public partial class transcription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Transcription",
                table: "WordJapans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transcription",
                table: "WordJapans");
        }
    }
}
