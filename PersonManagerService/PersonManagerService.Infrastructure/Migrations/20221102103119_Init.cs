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
                    { new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f"), "Mitar", "Miric" },
                    { new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1"), "Pera", "Zdera" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SocialMediaAccounts",
                columns: new[] { "SocialMediaAccountId", "Type" },
                values: new object[,]
                {
                    { new Guid("67a2aaf3-329a-46e0-87b1-0acb746d4c8b"), "Telegram" },
                    { new Guid("86d3674f-6778-4cc6-a593-55e7f5c4dd8b"), "Twitter" },
                    { new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c"), "Facebook" },
                    { new Guid("c7a9581b-f0e0-4c3a-b90f-1ccf49dd360f"), "LinkedIn" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSkills",
                columns: new[] { "PersonSkillId", "Name", "PersonId" },
                values: new object[,]
                {
                    { new Guid("230b0b87-7447-4287-a91b-5f821b001b23"), "spor", new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1") },
                    { new Guid("3d709ff0-f616-4b6c-b62b-c8cff4fcecab"), "peva", new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f") },
                    { new Guid("93087685-580c-4780-bd05-42087851ea93"), "debeo", new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1") },
                    { new Guid("e92139de-1ddd-4c12-8972-4ed61c98a507"), "brz", new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSocialMediaAccounts",
                columns: new[] { "PersonId", "SocialMediaAccountId", "Address" },
                values: new object[,]
                {
                    { new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f"), new Guid("86d3674f-6778-4cc6-a593-55e7f5c4dd8b"), "mita@tw" },
                    { new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f"), new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c"), "mita@fb" },
                    { new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1"), new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c"), "pera@fb" }
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
