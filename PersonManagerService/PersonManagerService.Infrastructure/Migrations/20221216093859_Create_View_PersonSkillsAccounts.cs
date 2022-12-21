using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagerService.Infrastructure.Migrations
{
    public partial class Create_View_PersonSkillsAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view PersonSkillsAccounts as
                                            select G.PersonId, G.FirstName, G.LastName, G.SocialMediaAccountId as AccountId, G.Address as AccountAddress, Type as AccountType, Name as SkillName
                                            from
                                            (select P.PersonId, P.FirstName, P.LastName, PSMA.Address, PSMA.SocialMediaAccountId from Persons as P left join PersonSocialMediaAccounts as PSMA on P.PersonId = PSMA.PersonId) as G
                                            left join SocialMediaAccounts as SMA
                                            on G.SocialMediaAccountId = SMA.SocialMediaAccountId
                                            left join PersonSkills as PS on PS.PersonId = G.PersonId;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view PersonSkillsAccounts;");
        }
    }
}
