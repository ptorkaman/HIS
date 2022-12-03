using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Migrations
{
    public partial class addCommentToEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                schema: "Lookup",
                table: "Educations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                schema: "Lookup",
                table: "Educations");
        }
    }
}
