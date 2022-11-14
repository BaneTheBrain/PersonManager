using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain.Models;
using PersonManagerService.Infrastructure.Contexts;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterDatabasesExtension
{
    public static void RegisterDatabases(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetSection("DatabaseOptions:ConnectionString").Value;
        if (dbConnectionString is not null)
        {
            serviceCollection.AddDbContext<PersonManagerServiceDbContext>(options => options.UseSqlServer(dbConnectionString));
        }
        else
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersonManagerServiceDbContext>().UseInMemoryDatabase(databaseName: "PersonDbInMemory");
            using (var context = new PersonManagerServiceDbContext(optionsBuilder.Options))
            {
                var personAId = new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1");
                var personBId = new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f");
                var fbAccountId = new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c");
                var twitterAccountId = new Guid("86d3674f-6778-4cc6-a593-55e7f5c4dd8b");

                context.Persons.Add(new Person { PersonId = personAId, FirstName = "Pera", LastName = "Zdera" });
                context.Persons.Add(new Person { PersonId = personBId, FirstName = "Mitar", LastName = "Miric" });


                context.SocialMediaAccounts.Add( new SocialMediaAccount { SocialMediaAccountId = fbAccountId, Type = "Facebook" });
                context.SocialMediaAccounts.Add(new SocialMediaAccount { SocialMediaAccountId = twitterAccountId, Type = "Twitter" });
                context.SocialMediaAccounts.Add(new SocialMediaAccount { SocialMediaAccountId = Guid.NewGuid(), Type = "LinkedIn" });
                context.SocialMediaAccounts.Add(new SocialMediaAccount { SocialMediaAccountId = Guid.NewGuid(), Type = "Telegram" });


                context.PersonSkills.Add(new PersonSkill { PersonId = personAId, Name = "debeo", PersonSkillId = Guid.NewGuid() });
                context.PersonSkills.Add(new PersonSkill { PersonId = personAId, Name = "spor", PersonSkillId = Guid.NewGuid() });
                context.PersonSkills.Add(new PersonSkill { PersonId = personBId, Name = "brz", PersonSkillId = Guid.NewGuid() });
                context.PersonSkills.Add(new PersonSkill { PersonId = personBId, Name = "peva", PersonSkillId = Guid.NewGuid()});

                context.PersonSocialMediaAccounts.Add( new PersonSocialMediaAccount { PersonId = personAId, SocialMediaAccountId = fbAccountId, Address = "pera@fb" });
                context.PersonSocialMediaAccounts.Add(new PersonSocialMediaAccount { PersonId = personBId, SocialMediaAccountId = fbAccountId, Address = "mita@fb" });
                context.PersonSocialMediaAccounts.Add(new PersonSocialMediaAccount { PersonId = personBId, SocialMediaAccountId = twitterAccountId, Address = "mita@tw" });

                context.SaveChanges();
            }

            serviceCollection.AddDbContext<PersonManagerServiceDbContext>(c => c.UseInMemoryDatabase(databaseName: "PersonDbInMemory"));
        }
    }
}
