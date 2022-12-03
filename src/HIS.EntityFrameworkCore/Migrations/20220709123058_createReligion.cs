using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Migrations
{
    public partial class createReligion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Religions",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    IsDefaultIndicator = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemIndicator = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Religions",
                schema: "Lookup");
        }
    }
}
