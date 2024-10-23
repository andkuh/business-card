﻿// <auto-generated />
using System;
using BusinessCard.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessCard.Migrations
{
    [DbContext(typeof(Ctx))]
    [Migration("20241022202420_AddEmploymentType")]
    partial class AddEmploymentType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("AssignmentTechnology", b =>
                {
                    b.Property<int>("AssignmentsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TechnologiesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AssignmentsId", "TechnologiesId");

                    b.HasIndex("TechnologiesId");

                    b.ToTable("AssignmentTechnology");
                });

            modelBuilder.Entity("BusinessCard.Employers.Records.Employer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.Property<int>("EmploymentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmploymentId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Duty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Duty");
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Employment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.HasIndex("PersonId");

                    b.ToTable("Employments");
                });

            modelBuilder.Entity("BusinessCard.JobTitles.Records.JobTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmploymentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmploymentId");

                    b.ToTable("JobTitles");
                });

            modelBuilder.Entity("BusinessCard.People.Records.EducationStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearFinished")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearStarted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("EducationSteps");
                });

            modelBuilder.Entity("BusinessCard.People.Records.Hobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Hobbies", (string)null);
                });

            modelBuilder.Entity("BusinessCard.People.Records.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.Property<int>("YearsOld")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("BusinessCard.Technologies.Records.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("AssignmentTechnology", b =>
                {
                    b.HasOne("BusinessCard.Employments.Records.Assignment", null)
                        .WithMany()
                        .HasForeignKey("AssignmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessCard.Technologies.Records.Technology", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Assignment", b =>
                {
                    b.HasOne("BusinessCard.Employments.Records.Employment", "Employment")
                        .WithMany("Assignments")
                        .HasForeignKey("EmploymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BusinessCard.Employments.Records.Link", "Link", b1 =>
                        {
                            b1.Property<int>("AssignmentId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Caption")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("AssignmentId");

                            b1.ToTable("Assignments");

                            b1.WithOwner()
                                .HasForeignKey("AssignmentId");
                        });

                    b.Navigation("Employment");

                    b.Navigation("Link");
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Duty", b =>
                {
                    b.HasOne("BusinessCard.Employments.Records.Assignment", null)
                        .WithMany("Duties")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Employment", b =>
                {
                    b.HasOne("BusinessCard.Employers.Records.Employer", "Employer")
                        .WithMany("Employments")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessCard.People.Records.Person", "Person")
                        .WithMany("Employments")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BusinessCard.JobTitles.Records.JobTitle", b =>
                {
                    b.HasOne("BusinessCard.Employments.Records.Employment", "Employment")
                        .WithMany("JobTitles")
                        .HasForeignKey("EmploymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employment");
                });

            modelBuilder.Entity("BusinessCard.People.Records.EducationStep", b =>
                {
                    b.HasOne("BusinessCard.People.Records.Person", null)
                        .WithMany("EducationSteps")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("BusinessCard.People.Records.Hobby", b =>
                {
                    b.HasOne("BusinessCard.People.Records.Person", null)
                        .WithMany("Hobbies")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("BusinessCard.People.Records.Person", b =>
                {
                    b.OwnsOne("BusinessCard.People.Records.PersonImage", "Image", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .HasColumnType("INTEGER");

                            b1.Property<byte[]>("Bytes")
                                .IsRequired()
                                .HasColumnType("BLOB");

                            b1.Property<string>("ContentType")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("PersonId");

                            b1.ToTable("People");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Image")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessCard.Employers.Records.Employer", b =>
                {
                    b.Navigation("Employments");
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Assignment", b =>
                {
                    b.Navigation("Duties");
                });

            modelBuilder.Entity("BusinessCard.Employments.Records.Employment", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("JobTitles");
                });

            modelBuilder.Entity("BusinessCard.People.Records.Person", b =>
                {
                    b.Navigation("EducationSteps");

                    b.Navigation("Employments");

                    b.Navigation("Hobbies");
                });
#pragma warning restore 612, 618
        }
    }
}
