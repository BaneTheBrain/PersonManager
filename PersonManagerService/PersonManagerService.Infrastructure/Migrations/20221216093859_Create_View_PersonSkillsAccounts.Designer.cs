// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonManagerService.Infrastructure.Contexts;

#nullable disable

namespace PersonManagerService.Infrastructure.Migrations
{
    [DbContext(typeof(PersonManagerServiceDbContext))]
    [Migration("20221216093859_Create_View_PersonSkillsAccounts")]
    partial class Create_View_PersonSkillsAccounts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PersonManagerService.Domain.Models.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons", "dbo");

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7"),
                            FirstName = "Pera",
                            LastName = "Zdera"
                        },
                        new
                        {
                            PersonId = new Guid("0273b148-e1ba-4697-89ef-7df4a632c913"),
                            FirstName = "Mitar",
                            LastName = "Mitrovic"
                        });
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.PersonSkill", b =>
                {
                    b.Property<Guid>("PersonSkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonSkillId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonSkills", "dbo");

                    b.HasData(
                        new
                        {
                            PersonSkillId = new Guid("52a892e6-8283-4840-a8c4-50d0389041e2"),
                            Name = "debeo",
                            PersonId = new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7")
                        },
                        new
                        {
                            PersonSkillId = new Guid("4aef2a23-82af-4823-b4b2-3f2fbf8daf2e"),
                            Name = "spor",
                            PersonId = new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7")
                        },
                        new
                        {
                            PersonSkillId = new Guid("a3678305-27a1-41f4-8d0a-bc802933cb52"),
                            Name = "tanak",
                            PersonId = new Guid("0273b148-e1ba-4697-89ef-7df4a632c913")
                        },
                        new
                        {
                            PersonSkillId = new Guid("6966e38a-ebc3-433c-8cce-7aeff676a5e0"),
                            Name = "jak",
                            PersonId = new Guid("0273b148-e1ba-4697-89ef-7df4a632c913")
                        });
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.PersonSocialMediaAccount", b =>
                {
                    b.Property<Guid>("PersonSocialMediaAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SocialMediaAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonSocialMediaAccountId");

                    b.HasIndex("SocialMediaAccountId");

                    b.HasIndex("PersonId", "SocialMediaAccountId");

                    b.ToTable("PersonSocialMediaAccounts", "dbo");

                    b.HasData(
                        new
                        {
                            PersonSocialMediaAccountId = new Guid("b8e46a7a-bd64-4c0d-9bdc-6817c5bb8c03"),
                            Address = "pera@fb",
                            PersonId = new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7"),
                            SocialMediaAccountId = new Guid("0df6a7ec-4489-4f86-b470-38c801f36691")
                        },
                        new
                        {
                            PersonSocialMediaAccountId = new Guid("78da84d3-67f6-4e1c-b408-b72a168c4fb7"),
                            Address = "mita@fb",
                            PersonId = new Guid("0273b148-e1ba-4697-89ef-7df4a632c913"),
                            SocialMediaAccountId = new Guid("0df6a7ec-4489-4f86-b470-38c801f36691")
                        },
                        new
                        {
                            PersonSocialMediaAccountId = new Guid("0e8df4d5-a53d-4607-ba64-24733bf01ac1"),
                            Address = "pera@tw",
                            PersonId = new Guid("d1bc0e4d-b4a5-411f-b69a-47f877b2c4e7"),
                            SocialMediaAccountId = new Guid("7cdd0cb0-8f63-4e50-94f5-b63ce3866b6b")
                        },
                        new
                        {
                            PersonSocialMediaAccountId = new Guid("e3ed045b-5fb8-43fe-b126-a06a3e5b1d43"),
                            Address = "mita@tw",
                            PersonId = new Guid("0273b148-e1ba-4697-89ef-7df4a632c913"),
                            SocialMediaAccountId = new Guid("7cdd0cb0-8f63-4e50-94f5-b63ce3866b6b")
                        });
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.SocialMediaAccount", b =>
                {
                    b.Property<Guid>("SocialMediaAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialMediaAccountId");

                    b.ToTable("SocialMediaAccounts", "dbo");

                    b.HasData(
                        new
                        {
                            SocialMediaAccountId = new Guid("0df6a7ec-4489-4f86-b470-38c801f36691"),
                            Type = "Facebook"
                        },
                        new
                        {
                            SocialMediaAccountId = new Guid("7cdd0cb0-8f63-4e50-94f5-b63ce3866b6b"),
                            Type = "Twitter"
                        },
                        new
                        {
                            SocialMediaAccountId = new Guid("25f0edf3-8aa7-42ef-97eb-083b965997ed"),
                            Type = "LinkedIn"
                        },
                        new
                        {
                            SocialMediaAccountId = new Guid("0680a970-846a-4ee3-9679-e6dbb6ed66e8"),
                            Type = "Telegram"
                        });
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.PersonSkill", b =>
                {
                    b.HasOne("PersonManagerService.Domain.Models.Person", "Person")
                        .WithMany("PersonSkills")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Person");
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.PersonSocialMediaAccount", b =>
                {
                    b.HasOne("PersonManagerService.Domain.Models.Person", "Person")
                        .WithMany("PersonSocialMediaAccounts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PersonManagerService.Domain.Models.SocialMediaAccount", "SocialMediaAccount")
                        .WithMany("PersonSocialMediaAccounts")
                        .HasForeignKey("SocialMediaAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Person");

                    b.Navigation("SocialMediaAccount");
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.Person", b =>
                {
                    b.Navigation("PersonSkills");

                    b.Navigation("PersonSocialMediaAccounts");
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.SocialMediaAccount", b =>
                {
                    b.Navigation("PersonSocialMediaAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
