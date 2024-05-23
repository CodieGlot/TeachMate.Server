﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeachMate.Services;

#nullable disable

namespace TeachMate.Services.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LearnerLearningModule", b =>
                {
                    b.Property<Guid>("EnrolledLearnersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EnrolledModulesId")
                        .HasColumnType("int");

                    b.HasKey("EnrolledLearnersId", "EnrolledModulesId");

                    b.HasIndex("EnrolledModulesId");

                    b.ToTable("LearnerLearningModule");
                });

            modelBuilder.Entity("TeachMate.Domain.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
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

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Learners");
                });

            modelBuilder.Entity("TeachMate.Domain.LearningModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("GradeLevel")
                        .HasColumnType("int");

                    b.Property<int>("MaximumLearners")
                        .HasColumnType("int");

                    b.Property<int>("ModuleType")
                        .HasColumnType("int");

                    b.Property<int>("NumOfWeeks")
                        .HasColumnType("int");

                    b.Property<string>("SerializedSchedule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("Subject")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("WeeklyScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.HasIndex("WeeklyScheduleId");

                    b.ToTable("LearningModules");
                });

            modelBuilder.Entity("TeachMate.Domain.LearningModuleRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<Guid?>("LearnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LearningModuleId")
                        .HasColumnType("int");

                    b.Property<string>("RequesterDisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequesterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SerializedSchedule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Subject")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorDisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LearnerId");

                    b.HasIndex("LearningModuleId");

                    b.ToTable("LearningModuleRequests");
                });


            modelBuilder.Entity("TeachMate.Domain.LearningSession", b =>

            modelBuilder.Entity("TeachMate.Domain.PushNotification", b =>

                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("LinkMeet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("LearningSessions");
                }));

            modelBuilder.Entity("TeachMate.Domain.Models.Schedule.WeeklySchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumberOfSlot")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeeklySchedules");
                });

            modelBuilder.Entity("TeachMate.Domain.Models.Schedule.WeeklySlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("WeeklySlots");
                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorDisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PushNotifications");
                });

            modelBuilder.Entity("TeachMate.Domain.PushNotificationReceiver", b =>
                {
                    b.Property<int>("PushNotificationId")
                        .HasColumnType("int");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PushNotificationId", "ReceiverId");

                    b.ToTable("PushNotificationsReceivers");
                });

            modelBuilder.Entity("TeachMate.Domain.Tutor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradeLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("LearnerLearningModule", b =>
                {
                    b.HasOne("TeachMate.Domain.Learner", null)
                        .WithMany()
                        .HasForeignKey("EnrolledLearnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachMate.Domain.LearningModule", null)
                        .WithMany()
                        .HasForeignKey("EnrolledModulesId")
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

            modelBuilder.Entity("TeachMate.Domain.LearningModule", b =>
                {
                    b.HasOne("TeachMate.Domain.Tutor", "Tutor")
                        .WithMany("CreatedModules")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachMate.Domain.Models.Schedule.WeeklySchedule", "WeeklySchedule")
                        .WithMany()
                        .HasForeignKey("WeeklyScheduleId");

                    b.Navigation("Tutor");

                    b.Navigation("WeeklySchedule");
                });

            modelBuilder.Entity("TeachMate.Domain.LearningModuleRequest", b =>
                {
                    b.HasOne("TeachMate.Domain.Learner", null)
                        .WithMany("LearningModuleRequests")
                        .HasForeignKey("LearnerId");

                    b.HasOne("TeachMate.Domain.LearningModule", "LearningModule")
                        .WithMany("LearningModuleRequests")
                        .HasForeignKey("LearningModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LearningModule");
                });

            modelBuilder.Entity("TeachMate.Domain.PushNotificationReceiver", b =>
                {
                    b.HasOne("TeachMate.Domain.PushNotification", "PushNotification")
                        .WithMany("Receivers")
                        .HasForeignKey("PushNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PushNotification");
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


            modelBuilder.Entity("TeachMate.Domain.Learner", b =>
                {
                    b.Navigation("LearningModuleRequests");
                });

            modelBuilder.Entity("TeachMate.Domain.LearningModule", b =>
                {
                    b.Navigation("LearningModuleRequests");

                    modelBuilder.Entity("TeachMate.Domain.PushNotification", b =>
                        {
                            b.Navigation("Receivers");

                        });

                    modelBuilder.Entity("TeachMate.Domain.Tutor", b =>
                        {
                            b.Navigation("CreatedModules");
                        });
                }
#pragma warning restore 612, 618
                ); }
    }
}
