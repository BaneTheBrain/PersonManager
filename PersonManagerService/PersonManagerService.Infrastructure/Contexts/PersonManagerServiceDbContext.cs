using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Infrastructure.Contexts;

public class PersonManagerServiceDbContext : DbContext
{
    public PersonManagerServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<PersonSkill> PersonSkills { get; set; }
    public virtual DbSet<PersonSocialMediaAccount> PersonSocialMediaAccounts { get; set; }

    #region Configuration

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var personAId = Guid.Parse("D1BC0E4D-B4A5-411F-B69A-47F877B2C4E7");
        var personBId = Guid.Parse("0273B148-E1BA-4697-89EF-7DF4A632C913");

        var fbAccountId = Guid.NewGuid();
        var twitterAccountId = Guid.NewGuid();
        var accountIds = Enumerable.Empty<Guid>();

        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Persons");
            entity.HasKey(e => e.PersonId);
            entity.Property(e => e.PersonId).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();

            entity.HasData(
                 new Person { PersonId = personAId, FirstName = "Pera", LastName = "Zdera" },
                 new Person { PersonId = personBId, FirstName = "Mitar", LastName = "Mitrovic" }
             );
        });

        modelBuilder.Entity<SocialMediaAccount>(entity =>
        {
            entity.ToTable("SocialMediaAccounts");
            entity.HasKey(e => e.SocialMediaAccountId);
            entity.Property(e => e.SocialMediaAccountId).ValueGeneratedOnAdd();
            entity.Property(e => e.Type).IsRequired();
            
            entity.HasData(
                new SocialMediaAccount { SocialMediaAccountId = fbAccountId, Type = "Facebook" },
                new SocialMediaAccount { SocialMediaAccountId = twitterAccountId, Type = "Twitter" },
                new SocialMediaAccount { SocialMediaAccountId = Guid.NewGuid(), Type = "LinkedIn" },
                new SocialMediaAccount { SocialMediaAccountId = Guid.NewGuid(), Type = "Telegram" }
                );
        });

        modelBuilder.Entity<PersonSkill>(entity =>
        {
            entity.ToTable("PersonSkills");
            entity.HasKey(e => e.PersonSkillId);
            entity.Property(e => e.PersonSkillId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();

            entity
                .HasOne(ss => ss.Person)
                .WithMany(p => p.PersonSkills)
                .HasForeignKey(p => p.PersonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new PersonSkill { PersonId = personAId, Name = "debeo", PersonSkillId = Guid.NewGuid() },
                new PersonSkill { PersonId = personAId, Name = "spor", PersonSkillId = Guid.NewGuid() },
                new PersonSkill { PersonId = personBId, Name = "tanak", PersonSkillId = Guid.NewGuid() },
                new PersonSkill { PersonId = personBId, Name = "jak", PersonSkillId = Guid.NewGuid() }
            );
        });

        modelBuilder.Entity<PersonSocialMediaAccount>(entity =>
        {
            entity.ToTable("PersonSocialMediaAccounts");
            entity.HasKey(e => e.PersonSocialMediaAccountId);
            entity.HasIndex(e => new { e.PersonId, e.SocialMediaAccountId });
            entity.Property(e => e.Address).IsRequired();

            entity
                .HasOne(psma => psma.Person)
                .WithMany(p => p.PersonSocialMediaAccounts)
                .HasForeignKey(psma => psma.PersonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(psma => psma.SocialMediaAccount)
                .WithMany(sma => sma.PersonSocialMediaAccounts)
                .HasForeignKey(psma => psma.SocialMediaAccountId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                  new PersonSocialMediaAccount { PersonSocialMediaAccountId = Guid.NewGuid(), PersonId = personAId, SocialMediaAccountId = fbAccountId, Address = "pera@fb" },
                  new PersonSocialMediaAccount { PersonSocialMediaAccountId = Guid.NewGuid(), PersonId = personBId, SocialMediaAccountId = fbAccountId, Address = "mita@fb" },
                  new PersonSocialMediaAccount { PersonSocialMediaAccountId = Guid.NewGuid(), PersonId = personAId, SocialMediaAccountId = twitterAccountId, Address = "pera@tw" },
                  new PersonSocialMediaAccount { PersonSocialMediaAccountId = Guid.NewGuid(), PersonId = personBId, SocialMediaAccountId = twitterAccountId, Address = $"mita@tw" }
                );
        });
    }
    #endregion
}
