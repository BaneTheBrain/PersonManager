using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagerService.Persistance.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaAccounts",
                schema: "dbo",
                columns: table => new
                {
                    SocialMediaAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaAccounts", x => x.SocialMediaAccountId);
                });

            migrationBuilder.CreateTable(
                name: "PersonSkills",
                schema: "dbo",
                columns: table => new
                {
                    PersonSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSkills", x => x.PersonSkillId);
                    table.ForeignKey(
                        name: "FK_PersonSkills_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "PersonSocialMediaAccounts",
                schema: "dbo",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialMediaAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSocialMediaAccounts", x => new { x.PersonId, x.SocialMediaAccountId });
                    table.ForeignKey(
                        name: "FK_PersonSocialMediaAccounts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_PersonSocialMediaAccounts_SocialMediaAccounts_SocialMediaAccountId",
                        column: x => x.SocialMediaAccountId,
                        principalSchema: "dbo",
                        principalTable: "SocialMediaAccounts",
                        principalColumn: "SocialMediaAccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkills_PersonId",
                schema: "dbo",
                table: "PersonSkills",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSocialMediaAccounts_SocialMediaAccountId",
                schema: "dbo",
                table: "PersonSocialMediaAccounts",
                column: "SocialMediaAccountId");


            migrationBuilder
               .InsertData(
                   schema: "dbo",
                   table: "SocialMediaAccounts",
                   columns: new[] { "SocialMediaAccountId", "Type" },
                   values: new object[]
                   {
                        "7425e42b-09a2-42c1-9f58-0ede8ff036de",
                        "Facebook"
                   });

            migrationBuilder
                .InsertData(
                    schema: "dbo",
                    table: "SocialMediaAccounts",
                    columns: new[] { "SocialMediaAccountId", "Type" },
                    values: new object[]
                    {
                        "8284d5e8-86f9-453f-a0cd-38d2500734c8",
                        "LinkedIn"
                    });

            migrationBuilder
                .InsertData(
                    schema: "dbo",
                    table: "SocialMediaAccounts",
                    columns: new[] { "SocialMediaAccountId", "Type" },
                    values: new object[]
                    {
                        "ee49d702-4b3d-4892-9889-0e787627cfa1",
                        "Twitter"
                    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonSkills",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonSocialMediaAccounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SocialMediaAccounts",
                schema: "dbo");
        }
    }
}
