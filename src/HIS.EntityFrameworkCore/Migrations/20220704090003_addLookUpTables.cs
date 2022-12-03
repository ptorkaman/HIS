using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Migrations
{
    public partial class addLookUpTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.CreateTable(
                name: "AdministrativeSexes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LongDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Comments = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_AdministrativeSexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NamePrefixes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Comments = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AdministrativeSexId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_NamePrefixes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamePrefixes_AdministrativeSexes_AdministrativeSexId",
                        column: x => x.AdministrativeSexId,
                        principalSchema: "Lookup",
                        principalTable: "AdministrativeSexes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NamePrefixes_AdministrativeSexId",
                schema: "Lookup",
                table: "NamePrefixes",
                column: "AdministrativeSexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NamePrefixes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "AdministrativeSexes",
                schema: "Lookup");
        }
    }
}
