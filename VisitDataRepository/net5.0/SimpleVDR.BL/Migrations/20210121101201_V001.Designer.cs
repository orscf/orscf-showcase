﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MedicalResearch.VisitData.Persistence.EF;

namespace MedicalResearch.VisitData.Migrations
{
    [DbContext(typeof(VisitDataDbContext))]
    [Migration("20210121101201_V001")]
    partial class V001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("MedicalResearch.VisitData.DataRecording", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DataSchemaInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExecutionDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExecutionState")
                        .HasColumnType("int");

                    b.Property<string>("InstructionsForExecution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotesRegardingOutcome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordedData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ScheduledDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisitRecordId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecordId");

                    b.HasIndex("VisitRecordId");

                    b.ToTable("DataRecordings");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.DrugApplyment", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AppliedUnits")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DrugDoseMgPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DrugName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExecutionDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExecutionState")
                        .HasColumnType("int");

                    b.Property<string>("InstructionsForExecution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotesRegardingOutcome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ScheduledDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisitRecordId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecordId");

                    b.HasIndex("VisitRecordId");

                    b.ToTable("DrugApplyments");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Treatment", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ExecutionDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExecutionState")
                        .HasColumnType("int");

                    b.Property<string>("InstructionsForExecution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotesRegardingOutcome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ScheduledDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisitRecordId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecordId");

                    b.HasIndex("VisitRecordId");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Visit", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExecutingInstituteIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExecutionDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExecutionState")
                        .HasColumnType("int");

                    b.Property<string>("ParticipantIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ScheduledDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudyExecutionIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudyIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisitIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.DataRecording", b =>
                {
                    b.HasOne("MedicalResearch.VisitData.Visit", null)
                        .WithMany("DataRecordings")
                        .HasForeignKey("VisitRecordId");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.DrugApplyment", b =>
                {
                    b.HasOne("MedicalResearch.VisitData.Visit", null)
                        .WithMany("DrugApplyments")
                        .HasForeignKey("VisitRecordId");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Treatment", b =>
                {
                    b.HasOne("MedicalResearch.VisitData.Visit", null)
                        .WithMany("Treatments")
                        .HasForeignKey("VisitRecordId");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Visit", b =>
                {
                    b.Navigation("DataRecordings");

                    b.Navigation("DrugApplyments");

                    b.Navigation("Treatments");
                });
#pragma warning restore 612, 618
        }
    }
}
