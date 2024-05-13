﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeachMate.Services;

#nullable disable

namespace TeachMate.Services.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240513043853_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LearnerLearningSession", b =>
                {
                    b.Property<Guid>("EnrolledLearnersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EnrolledSessionsId")
                        .HasColumnType("int");

                    b.HasKey("EnrolledLearnersId", "EnrolledSessionsId");

                    b.HasIndex("EnrolledSessionsId");

                    b.ToTable("LearnerLearningSession");
                });

            modelBuilder.Entity("TeachMate.Domain.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("TeachMate.Domain.Learner", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Learners");
                });

            modelBuilder.Entity("TeachMate.Domain.LearningSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaximumLearners")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Subject")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.ToTable("LearningSessions");
                });

            modelBuilder.Entity("TeachMate.Domain.Tutor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("LearnerLearningSession", b =>
                {
                    b.HasOne("TeachMate.Domain.Learner", null)
                        .WithMany()
                        .HasForeignKey("EnrolledLearnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachMate.Domain.LearningSession", null)
                        .WithMany()
                        .HasForeignKey("EnrolledSessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeachMate.Domain.Learner", b =>
                {
                    b.HasOne("TeachMate.Domain.AppUser", "AppUser")
                        .WithOne("Learner")
                        .HasForeignKey("TeachMate.Domain.Learner", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TeachMate.Domain.LearningSession", b =>
                {
                    b.HasOne("TeachMate.Domain.Tutor", "Tutor")
                        .WithMany("CreatedSessions")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("TeachMate.Domain.Tutor", b =>
                {
                    b.HasOne("TeachMate.Domain.AppUser", "AppUser")
                        .WithOne("Tutor")
                        .HasForeignKey("TeachMate.Domain.Tutor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TeachMate.Domain.AppUser", b =>
                {
                    b.Navigation("Learner");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("TeachMate.Domain.Tutor", b =>
                {
                    b.Navigation("CreatedSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
