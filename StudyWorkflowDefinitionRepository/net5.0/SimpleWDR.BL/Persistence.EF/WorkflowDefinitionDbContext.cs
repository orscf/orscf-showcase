using System;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Workflow.Persistence.EF {

  public class WorkflowDefinitionDbContext : DbContext{


    public DbSet<DataRecording> DataRecordings { get; set; }
    public DbSet<DrugApplyment> DrugApplyments { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<Visit> Visits { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      // TABLE NAMES

      const string tablePrefix = "Wdr";

      modelBuilder.Entity<DataRecording>().ToTable(tablePrefix + nameof(this.DataRecordings));

      // PRIMARY KEYS

      modelBuilder.Entity<DataRecording>().HasKey(e => e.RecordId);
      modelBuilder.Entity<DrugApplyment>().HasKey(e => e.RecordId);
      modelBuilder.Entity<Treatment>().HasKey(e => e.RecordId);
      modelBuilder.Entity<Visit>().HasKey(e => e.RecordId);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder options) {
      options.UseSqlServer(_ConnectionString);
    }

    public static void Migrate() {
      if (!_Migrated) {
        WorkflowDefinitionDbContext c = new WorkflowDefinitionDbContext();
        c.Database.Migrate();
        _Migrated = true;
        c.Dispose();
      }
    }

    private static bool _Migrated = false;
    private static String _ConnectionString = null;
    public static String ConnectionString {
      set{ _ConnectionString = value;  }
    }

  }

}
