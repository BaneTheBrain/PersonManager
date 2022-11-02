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
        var personAId = Guid.NewGuid();
        var personBId = Guid.NewGuid();
        var fbAccountId = Guid.NewGuid();
        var twitterAccountId = Guid.NewGuid();

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
                 new Person { PersonId = personBId, FirstName = "Mitar", LastName = "Miric" }
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
                new PersonSkill { PersonId = personBId, Name = "brz", PersonSkillId = Guid.NewGuid() },
                new PersonSkill { PersonId = personBId, Name = "peva", PersonSkillId = Guid.NewGuid() }
            );
        });

        modelBuilder.Entity<PersonSocialMediaAccount>(entity =>
        {
            entity.ToTable("PersonSocialMediaAccounts");
            entity.HasKey(e => new { e.PersonId, e.SocialMediaAccountId });
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
                  new PersonSocialMediaAccount { PersonId = personAId, SocialMediaAccountId = fbAccountId, Address = "pera@fb" },
                  new PersonSocialMediaAccount { PersonId = personBId, SocialMediaAccountId = fbAccountId, Address = "mita@fb" },
                  new PersonSocialMediaAccount { PersonId = personBId, SocialMediaAccountId = twitterAccountId, Address = "mita@tw" }
                );
        });
    }
    #endregion
}
