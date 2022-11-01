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
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Persons");
            entity.HasKey(e => e.PersonId);
            entity.Property(e => e.PersonId).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
        });

        modelBuilder.Entity<SocialMediaAccount>(entity =>
        {
            entity.ToTable("SocialMediaAccounts");
            entity.HasKey(e => e.SocialMediaAccountId);
            entity.Property(e => e.SocialMediaAccountId).ValueGeneratedOnAdd();
            entity.Property(e => e.Type).IsRequired();
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
                .IsRequired(false);
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
                .IsRequired(false);

            entity
                .HasOne(psma => psma.SocialMediaAccount)
                .WithMany(sma => sma.PersonSocialMediaAccounts)
                .HasForeignKey(psma => psma.SocialMediaAccountId)
                .IsRequired(false);
        });
    }
    #endregion
}
