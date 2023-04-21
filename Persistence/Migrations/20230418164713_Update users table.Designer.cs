﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mineshard.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    [DbContext(typeof(RepoAnalysisContext))]
    [Migration("20230418164713_Update users table")]
    partial class Updateuserstable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Mineshard.Persistence.Models.Auth.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Auth.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Committer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumCommits")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Commiters");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.MonthlyLoad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("NumCommits")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("MonthlyLoads");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Provider", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("NumCommitsOnMain")
                        .HasColumnType("integer");

                    b.Property<Guid>("RepositoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Repository", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProviderUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RequestorId")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("ProviderId");

                    b.HasIndex("RequestorId");

                    b.ToTable("Repository");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Auth.User", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Auth.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Branch", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Report", "Report")
                        .WithMany("Branches")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Committer", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Report", "Report")
                        .WithMany("Committers")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.MonthlyLoad", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Report", "Report")
                        .WithMany("CommitsPerMonth")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Report", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Repository", "Repository")
                        .WithMany("Reports")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Repository", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mineshard.Persistence.Models.Auth.User", "Requestor")
                        .WithMany()
                        .HasForeignKey("RequestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("Requestor");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Report", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("CommitsPerMonth");

                    b.Navigation("Committers");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Repository", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
