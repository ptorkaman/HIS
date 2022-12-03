using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Migrations
{
    public partial class removeRelationIncontactMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactModes_AdministrativeSexes_AdministrativeSexId",
                schema: "Lookup",
                table: "ContactModes");

            migrationBuilder.DropIndex(
                name: "IX_ContactModes_AdministrativeSexId",
                schema: "Lookup",
                table: "ContactModes");

            migrationBuilder.DropColumn(
                name: "AdministrativeSexId",
                schema: "Lookup",
                table: "ContactModes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdministrativeSexId",
                schema: "Lookup",
                table: "ContactModes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactModes_AdministrativeSexId",
                schema: "Lookup",
                table: "ContactModes",
                column: "AdministrativeSexId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactModes_AdministrativeSexes_AdministrativeSexId",
                schema: "Lookup",
                table: "ContactModes",
                column: "AdministrativeSexId",
                principalSchema: "Lookup",
                principalTable: "AdministrativeSexes",
                principalColumn: "Id");
        }
    }
}
