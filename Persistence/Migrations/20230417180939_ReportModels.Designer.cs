﻿﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mineshard.Persistence;
using Mineshard.Persistence.Context;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    [DbContext(typeof(RepoAnalysisContext))]
    [Migration("20230417180939_ReportModels")]
    partial class ReportModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Mineshard.Persistence.Models.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
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

            modelBuilder.Entity("Mineshard.Persistence.Models.Repo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Repo");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("NumCommitsOnMain")
                        .HasColumnType("integer");

                    b.Property<Guid>("RepoId")
                        .HasColumnType("uuid");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RepoId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Branch", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Committer", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.MonthlyLoad", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Mineshard.Persistence.Models.Report", b =>
                {
                    b.HasOne("Mineshard.Persistence.Models.Repo", "Repo")
                        .WithMany()
                        .HasForeignKey("RepoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repo");
                });
#pragma warning restore 612, 618
        }
    }
}
