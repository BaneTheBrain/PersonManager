using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagerService.Infrastructure.Migrations
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
                    { new Guid("66933f1a-d11e-4813-b1df-51f9e1ed584d"), "Pera", "Zdera" },
                    { new Guid("98ede1ea-68a5-4ba0-8976-b176eed0d2a4"), "Zika", "Klinika" },
                    { new Guid("bf78be81-691d-4b0d-a99c-58d9b2d35950"), "Mita", "Brzi" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SocialMediaAccounts",
                columns: new[] { "SocialMediaAccountId", "Type" },
                values: new object[,]
                {
                    { new Guid("3d6935ac-1b0d-4c92-9fdf-c649d390bc83"), "Twitter" },
                    { new Guid("708abcd6-3f39-4870-89e9-a4a316056dbc"), "Telegram" },
                    { new Guid("aeb97361-b3ec-4bd4-ab5f-ba4f47cebb30"), "Facebook" },
                    { new Guid("ce27c608-a53c-407b-9722-ad48559cc984"), "LinkedIn" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSkills",
                columns: new[] { "PersonSkillId", "Name", "PersonId" },
                values: new object[,]
                {
                    { new Guid("27eadae8-6ea4-4ed4-aff2-57e526df96a0"), "spor", new Guid("66933f1a-d11e-4813-b1df-51f9e1ed584d") },
                    { new Guid("78c93bcb-7c8f-4c22-b2eb-9bfaf62978a1"), "jak", new Guid("bf78be81-691d-4b0d-a99c-58d9b2d35950") },
                    { new Guid("9f88b16a-c278-4e80-9185-7ecc5ae30fa0"), "brz", new Guid("bf78be81-691d-4b0d-a99c-58d9b2d35950") },
                    { new Guid("c33961d6-8831-4075-b207-9f18469bc423"), "debeo", new Guid("66933f1a-d11e-4813-b1df-51f9e1ed584d") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSocialMediaAccounts",
                columns: new[] { "PersonId", "SocialMediaAccountId", "Address" },
                values: new object[,]
                {
                    { new Guid("66933f1a-d11e-4813-b1df-51f9e1ed584d"), new Guid("aeb97361-b3ec-4bd4-ab5f-ba4f47cebb30"), "pera@fb" },
                    { new Guid("98ede1ea-68a5-4ba0-8976-b176eed0d2a4"), new Guid("3d6935ac-1b0d-4c92-9fdf-c649d390bc83"), "zika@tw" },
                    { new Guid("bf78be81-691d-4b0d-a99c-58d9b2d35950"), new Guid("3d6935ac-1b0d-4c92-9fdf-c649d390bc83"), "mita@tw" },
                    { new Guid("bf78be81-691d-4b0d-a99c-58d9b2d35950"), new Guid("aeb97361-b3ec-4bd4-ab5f-ba4f47cebb30"), "mita@fb" }
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
