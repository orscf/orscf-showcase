﻿// <auto-generated />
using MedicalResearch.VisitData.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedicalResearch.VisitData.Migrations
{
    [DbContext(typeof(VisitDataDbContext))]
    [Migration("20210312141521_V001")]
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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VisitRecordId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RecordId");

                    b.HasIndex("VisitRecordId");

                    b.ToTable("VdrDataRecordings");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.DrugApplyment", b =>
                {
                    b.Property<string>("RecordId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VisitRecordId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RecordId");

                    b.HasIndex("VisitRecordId");

                    b.ToTable("VdrDrugApplyments");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Treatment", b =>
                {
                    b.Property<string>("RecordId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VisitRecordId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RecordId");

                    b.HasIndex("VisitRecordId");

                    b.ToTable("VdrTreatments");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Visit", b =>
                {
                    b.Property<string>("RecordId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ExecutingInstituteIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExecutionDateUtc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExecutionState")
                        .HasColumnType("int");

                    b.Property<string>("ParticipantIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScheduledDateUtc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudyExecutionIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudyIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisitIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordId");

                    b.ToTable("VdrVisits");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.DataRecording", b =>
                {
                    b.HasOne("MedicalResearch.VisitData.Visit", "Visit")
                        .WithMany("DataRecordings")
                        .HasForeignKey("VisitRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.DrugApplyment", b =>
                {
                    b.HasOne("MedicalResearch.VisitData.Visit", "Visit")
                        .WithMany("DrugApplyments")
                        .HasForeignKey("VisitRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("MedicalResearch.VisitData.Treatment", b =>
                {
                    b.HasOne("MedicalResearch.VisitData.Visit", "Visit")
                        .WithMany("Treatments")
                        .HasForeignKey("VisitRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Visit");
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
