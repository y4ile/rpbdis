﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lab4.Models;

#nullable disable

namespace lab4.Migrations
{
    [DbContext(typeof(PlansContext))]
    partial class PlansContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("lab4.Models.DevelopmentDirection", b =>
                {
                    b.Property<int>("DirectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DirectionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirectionID"));

                    b.Property<string>("DirectionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DirectionID")
                        .HasName("PK__Developm__876846264ED3BABD");

                    b.ToTable("DevelopmentDirections");
                });

            modelBuilder.Entity("lab4.Models.PlanStage", b =>
                {
                    b.Property<int>("StageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StageID"));

                    b.Property<DateOnly?>("CloseDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("OpenDate")
                        .HasColumnType("date");

                    b.Property<int>("PlanId")
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    b.HasKey("StageID")
                        .HasName("PK__PlanStag__03EB7AF86433D57F");

                    b.HasIndex("PlanId");

                    b.HasIndex("StatusId");

                    b.ToTable("PlanStages");
                });

            modelBuilder.Entity("lab4.Models.PlanStagesWithInfo", b =>
                {
                    b.Property<DateOnly?>("CloseDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("OpenDate")
                        .HasColumnType("date");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("StageID")
                        .HasColumnType("int")
                        .HasColumnName("StageID");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.ToTable((string)null);

                    b.ToView("PlanStagesWithInfo", (string)null);
                });

            modelBuilder.Entity("lab4.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StatusId")
                        .HasName("PK__Statuses__C8EE20431F0BE42F");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("lab4.Models.StudyPlan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanID"));

                    b.Property<int>("DirectionId")
                        .HasColumnType("int")
                        .HasColumnName("DirectionID");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("PlanID")
                        .HasName("PK__StudyPla__755C22D7C9CFBA5E");

                    b.HasIndex("DirectionId");

                    b.HasIndex("UserId");

                    b.ToTable("StudyPlans");
                });

            modelBuilder.Entity("lab4.Models.StudyPlanWithUserInfo", b =>
                {
                    b.Property<string>("DirectionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PlanID")
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable((string)null);

                    b.ToView("StudyPlanWithUserInfo", (string)null);
                });

            modelBuilder.Entity("lab4.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserID")
                        .HasName("PK__Users__1788CCAC1D507487");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("lab4.Models.UsersWithStudyPlan", b =>
                {
                    b.Property<string>("DirectionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PlanID")
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.ToTable((string)null);

                    b.ToView("UsersWithStudyPlans", (string)null);
                });

            modelBuilder.Entity("lab4.Models.PlanStage", b =>
                {
                    b.HasOne("lab4.Models.StudyPlan", "Plan")
                        .WithMany("PlanStages")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PlanStages_To_StudyPlans");

                    b.HasOne("lab4.Models.Status", "Status")
                        .WithMany("PlanStages")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PlanStages_To_Statuses");

                    b.Navigation("Plan");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("lab4.Models.StudyPlan", b =>
                {
                    b.HasOne("lab4.Models.DevelopmentDirection", "Direction")
                        .WithMany("StudyPlans")
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_StudyPlans_To_DevelopmentDirections");

                    b.HasOne("lab4.Models.User", "User")
                        .WithMany("StudyPlans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_StudyPlans_To_Users");

                    b.Navigation("Direction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("lab4.Models.DevelopmentDirection", b =>
                {
                    b.Navigation("StudyPlans");
                });

            modelBuilder.Entity("lab4.Models.Status", b =>
                {
                    b.Navigation("PlanStages");
                });

            modelBuilder.Entity("lab4.Models.StudyPlan", b =>
                {
                    b.Navigation("PlanStages");
                });

            modelBuilder.Entity("lab4.Models.User", b =>
                {
                    b.Navigation("StudyPlans");
                });
#pragma warning restore 612, 618
        }
    }
}
