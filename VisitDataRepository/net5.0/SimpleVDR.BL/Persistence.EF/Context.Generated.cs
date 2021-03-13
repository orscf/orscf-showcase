using System;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.VisitData.Persistence.EF {

  public class VisitDataDbContext : DbContext{

    public DbSet<DataRecording> DataRecordings { get; set; }

    public DbSet<Visit> Visits { get; set; }

    public DbSet<DrugApplyment> DrugApplyments { get; set; }

    public DbSet<Treatment> Treatments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

#region Mapping

      //////////////////////////////////////////////////////////////////////////////////////
      // DataRecording
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgDataRecording = modelBuilder.Entity<DataRecording>();
      cfgDataRecording.ToTable("VdrDataRecordings");
      cfgDataRecording.HasKey((e) => e.RecordId);

      // PRINCIPAL: >>> Visit
      cfgDataRecording
        .HasOne((lcl) => lcl.Visit )
        .WithMany((rem) => rem.DataRecordings )
        .HasForeignKey(nameof(DataRecording.VisitRecordId))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // Visit
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgVisit = modelBuilder.Entity<Visit>();
      cfgVisit.ToTable("VdrVisits");
      cfgVisit.HasKey((e) => e.RecordId);

      //////////////////////////////////////////////////////////////////////////////////////
      // DrugApplyment
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgDrugApplyment = modelBuilder.Entity<DrugApplyment>();
      cfgDrugApplyment.ToTable("VdrDrugApplyments");
      cfgDrugApplyment.HasKey((e) => e.RecordId);

      // PRINCIPAL: >>> Visit
      cfgDrugApplyment
        .HasOne((lcl) => lcl.Visit )
        .WithMany((rem) => rem.DrugApplyments )
        .HasForeignKey(nameof(DrugApplyment.VisitRecordId))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // Treatment
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgTreatment = modelBuilder.Entity<Treatment>();
      cfgTreatment.ToTable("VdrTreatments");
      cfgTreatment.HasKey((e) => e.RecordId);

      // PRINCIPAL: >>> Visit
      cfgTreatment
        .HasOne((lcl) => lcl.Visit )
        .WithMany((rem) => rem.Treatments )
        .HasForeignKey(nameof(Treatment.VisitRecordId))
        .OnDelete(DeleteBehavior.Cascade);

#endregion

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) {

      //reqires separate nuget-package Microsoft.EntityFrameworkCore.SqlServer
      options.UseSqlServer(_ConnectionString);

      //reqires separate nuget-package Microsoft.EntityFrameworkCore.Proxies
      options.UseLazyLoadingProxies();

    }

    public static void Migrate() {
      if (!_Migrated) {
        VisitDataDbContext c = new VisitDataDbContext();
        c.Database.Migrate();
        _Migrated = true;
        c.Dispose();
      }
    }

   private static bool _Migrated = false;

    private static String _ConnectionString = "Server=(localdb)\\MsSqlLocalDb;Database=VisitDataDbContext;Integrated Security=True;Persist Security Info=True;MultipleActiveResultSets=True;";
    public static String ConnectionString {
      set{ _ConnectionString = value;  }
    }

  }

}
