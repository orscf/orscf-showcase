using System;
using Microsoft.EntityFrameworkCore;

namespace ORSCF.VisitData.Persistence.EF {

  public class VisitDataDbContext : DbContext{


    public DbSet<DataRecording> DataRecordings { get; set; }
    public DbSet<DrugApplyment> DrugApplyments { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<Visit> Visits { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<DataRecording>().HasKey(e => e.RecordId);
      modelBuilder.Entity<DrugApplyment>().HasKey(e => e.RecordId);
      modelBuilder.Entity<Treatment>().HasKey(e => e.RecordId);
      modelBuilder.Entity<Visit>().HasKey(e => e.RecordId);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder options) {
      options.UseSqlServer(_ConnectionString);
    }

    private static String _ConnectionString = null;
    public static String ConnectionString {
      set{ _ConnectionString = value;  }
    }

  }

}
