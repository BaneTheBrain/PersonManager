using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagerService.Infrastructure.Migrations
{
    public partial class init : Migration
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
                    PersonSocialMediaAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialMediaAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSocialMediaAccounts", x => x.PersonSocialMediaAccountId);
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
                    { new Guid("0273b148-e1ba-4697-89ef-7df4a632c913"), "Mitar", "Mitrovic" },
                    { new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7"), "Pera", "Zdera" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SocialMediaAccounts",
                columns: new[] { "SocialMediaAccountId", "Type" },
                values: new object[,]
                {
                    { new Guid("0795d1c7-4ffc-4be7-a8eb-362b78a9e232"), "Telegram" },
                    { new Guid("626fe0fc-7877-4a50-b5df-30c075eb251f"), "Twitter" },
                    { new Guid("7af524e3-2d31-422e-845f-54e3de69c767"), "LinkedIn" },
                    { new Guid("c3fd6cd5-de6f-4280-9fe2-b650e63716fb"), "Facebook" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSkills",
                columns: new[] { "PersonSkillId", "Name", "PersonId" },
                values: new object[,]
                {
                    { new Guid("240d37fc-0bfd-4004-a775-e3060bbb312a"), "jak", new Guid("0273b148-e1ba-4697-89ef-7df4a632c913") },
                    { new Guid("4f09b36f-0b1f-40a6-8f33-39026d8e8b9c"), "debeo", new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7") },
                    { new Guid("8d484b2d-2097-421c-a069-5b24c8992400"), "tanak", new Guid("0273b148-e1ba-4697-89ef-7df4a632c913") },
                    { new Guid("9de7d1cc-3407-4d4b-9a10-9f65702564f6"), "spor", new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PersonSocialMediaAccounts",
                columns: new[] { "PersonSocialMediaAccountId", "Address", "PersonId", "SocialMediaAccountId" },
                values: new object[,]
                {
                    { new Guid("34311ed3-31df-4e65-8785-a40d4c45581a"), "pera@tw", new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7"), new Guid("626fe0fc-7877-4a50-b5df-30c075eb251f") },
                    { new Guid("ad4d8bfb-c9ed-4102-b6aa-a658bc3b03d1"), "mita@fb", new Guid("0273b148-e1ba-4697-89ef-7df4a632c913"), new Guid("c3fd6cd5-de6f-4280-9fe2-b650e63716fb") },
                    { new Guid("c0b0d53b-a44e-41dd-8d01-15cf00fde2d4"), "mita@tw", new Guid("0273b148-e1ba-4697-89ef-7df4a632c913"), new Guid("626fe0fc-7877-4a50-b5df-30c075eb251f") },
                    { new Guid("cc0c20a1-c5b2-4d30-b950-95d9d04db362"), "pera@fb", new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7"), new Guid("c3fd6cd5-de6f-4280-9fe2-b650e63716fb") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkills_PersonId",
                schema: "dbo",
                table: "PersonSkills",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSocialMediaAccounts_PersonId_SocialMediaAccountId",
                schema: "dbo",
                table: "PersonSocialMediaAccounts",
                columns: new[] { "PersonId", "SocialMediaAccountId" });

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
