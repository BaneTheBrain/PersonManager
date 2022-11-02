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
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonSocialMediaAccounts_SocialMediaAccounts_SocialMediaAccountId",
                        column: x => x.SocialMediaAccountId,
                        principalSchema: "dbo",
                        principalTable: "SocialMediaAccounts",
                        principalColumn: "SocialMediaAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Persons",
                columns: new[] { "PersonId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("4e131d98-f5d1-4a18-95b7-bb9ec8bd4c7a"), "Pera", "Zdera" },
                    { new Guid("e590b2a7-fca2-496a-b863-9917ceb21001"), "Mitar", "Miric" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SocialMediaAccounts",
                columns: new[] { "SocialMediaAccountId", "Type" },
                values: new object[,]
                {
                    { new Guid("2283f630-2ed0-4444-b238-ae3eb59ef51e"), "Telegram" },
                    { new Guid("5f2fb333-4da7-4742-b0ba-f568116d726e"), "Facebook" },
                    { new Guid("79df559b-e2f6-4612-ad48-21229ea2eb75"), "Twitter" },
                    { new Guid("f574a1b6-caa9-43af-a018-a98d9a5c8d00"), "LinkedIn" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSkills",
                columns: new[] { "PersonSkillId", "Name", "PersonId" },
                values: new object[,]
                {
                    { new Guid("5984f429-93d9-4650-8d5c-7902bcacf866"), "debeo", new Guid("4e131d98-f5d1-4a18-95b7-bb9ec8bd4c7a") },
                    { new Guid("7e09b932-231b-43e0-bd6c-440b9e750211"), "spor", new Guid("4e131d98-f5d1-4a18-95b7-bb9ec8bd4c7a") },
                    { new Guid("a17b80a3-05d5-454f-b6d4-04e77d43001d"), "brz", new Guid("e590b2a7-fca2-496a-b863-9917ceb21001") },
                    { new Guid("c8691848-e94b-46ae-b49a-219343e29a88"), "peva", new Guid("e590b2a7-fca2-496a-b863-9917ceb21001") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSocialMediaAccounts",
                columns: new[] { "PersonId", "SocialMediaAccountId", "Address" },
                values: new object[,]
                {
                    { new Guid("4e131d98-f5d1-4a18-95b7-bb9ec8bd4c7a"), new Guid("5f2fb333-4da7-4742-b0ba-f568116d726e"), "pera@fb" },
                    { new Guid("e590b2a7-fca2-496a-b863-9917ceb21001"), new Guid("5f2fb333-4da7-4742-b0ba-f568116d726e"), "mita@fb" },
                    { new Guid("e590b2a7-fca2-496a-b863-9917ceb21001"), new Guid("79df559b-e2f6-4612-ad48-21229ea2eb75"), "mita@tw" }
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
