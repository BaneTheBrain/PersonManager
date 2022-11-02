﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonManagerService.Infrastructure.Contexts;

#nullable disable

namespace PersonManagerService.Persistance.Migrations
{
    [DbContext(typeof(PersonManagerServiceDbContext))]
    partial class PersonManagerServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            PersonId = new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1"),
                            FirstName = "Pera",
                            LastName = "Zdera"
                        },
                        new
                        {
                            PersonId = new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f"),
                            FirstName = "Mitar",
                            LastName = "Miric"
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
                            PersonSkillId = new Guid("93087685-580c-4780-bd05-42087851ea93"),
                            Name = "debeo",
                            PersonId = new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1")
                        },
                        new
                        {
                            PersonSkillId = new Guid("230b0b87-7447-4287-a91b-5f821b001b23"),
                            Name = "spor",
                            PersonId = new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1")
                        },
                        new
                        {
                            PersonSkillId = new Guid("e92139de-1ddd-4c12-8972-4ed61c98a507"),
                            Name = "brz",
                            PersonId = new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f")
                        },
                        new
                        {
                            PersonSkillId = new Guid("3d709ff0-f616-4b6c-b62b-c8cff4fcecab"),
                            Name = "peva",
                            PersonId = new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f")
                        });
                });

            modelBuilder.Entity("PersonManagerService.Domain.Models.PersonSocialMediaAccount", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SocialMediaAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId", "SocialMediaAccountId");

                    b.HasIndex("SocialMediaAccountId");

                    b.ToTable("PersonSocialMediaAccounts", "dbo");

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("a5d45942-98b8-42e9-9e13-1718fb954fd1"),
                            SocialMediaAccountId = new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c"),
                            Address = "pera@fb"
                        },
                        new
                        {
                            PersonId = new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f"),
                            SocialMediaAccountId = new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c"),
                            Address = "mita@fb"
                        },
                        new
                        {
                            PersonId = new Guid("4c0ff862-187e-4b7c-8e06-44f08e613d8f"),
                            SocialMediaAccountId = new Guid("86d3674f-6778-4cc6-a593-55e7f5c4dd8b"),
                            Address = "mita@tw"
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
                            SocialMediaAccountId = new Guid("acd7419f-73eb-48ef-b042-05de4bb7dc8c"),
                            Type = "Facebook"
                        },
                        new
                        {
                            SocialMediaAccountId = new Guid("86d3674f-6778-4cc6-a593-55e7f5c4dd8b"),
                            Type = "Twitter"
                        },
                        new
                        {
                            SocialMediaAccountId = new Guid("c7a9581b-f0e0-4c3a-b90f-1ccf49dd360f"),
                            Type = "LinkedIn"
                        },
                        new
                        {
                            SocialMediaAccountId = new Guid("67a2aaf3-329a-46e0-87b1-0acb746d4c8b"),
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
