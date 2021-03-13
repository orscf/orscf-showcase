using System;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Workflow.Persistence.EF {

  public class WorkflowDefinitionDbContext : DbContext{

    public DbSet<Arm> Arms { get; set; }

    public DbSet<ResearchStudy> ResearchStudies { get; set; }

    public DbSet<ProcedureSchedule> ProcedureSchedules { get; set; }

    public DbSet<DataRecordingTask> DataRecordingTasks { get; set; }

    public DbSet<InducedDataRecordingTask> InducedDataRecordingTasks { get; set; }

    public DbSet<DrugApplymentTask> DrugAppliymentTasks { get; set; }

    public DbSet<InducedDrugApplymentTask> InducedDrugApplymentTasks { get; set; }

    public DbSet<TaskSchedule> TaskSchedules { get; set; }

    public DbSet<InducedSubProcedureSchedule> InducedSubProcedureSchedules { get; set; }

    public DbSet<InducedSubTaskSchedule> InducedSubTaskSchedules { get; set; }

    public DbSet<InducedTreatmentTask> InducedTreatmentTasks { get; set; }

    public DbSet<TreatmentTask> TreatmentTasks { get; set; }

    public DbSet<InducedVisitProcedure> InducedVisitProcedures { get; set; }

    public DbSet<VisitProdecureDefinition> VisitProdecureDefinitions { get; set; }

    public DbSet<ProcedureCycleDefinition> ProcedureCycleDefinitions { get; set; }

    public DbSet<StudyEvent> StudyEvents { get; set; }

    public DbSet<TaskCycleDefinition> TaskCycleDefinitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

#region Mapping

      //////////////////////////////////////////////////////////////////////////////////////
      // Arm
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgArm = modelBuilder.Entity<Arm>();
      cfgArm.ToTable("WdrArms");
      cfgArm.HasKey((e) => new {e.StudyArmName, e.StudyWorkflowName, e.StudyWorkflowVersion});

      // PRINCIPAL: >>> ResearchStudy
      cfgArm
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.Arms )
        .HasForeignKey(nameof(Arm.StudyWorkflowName), nameof(Arm.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      // LOOKUP: >>> ProcedureSchedule
      cfgArm
        .HasOne((lcl) => lcl.RootProcedureSchedule )
        .WithMany((rem) => rem.EntryArms )
        .HasForeignKey(nameof(Arm.RootProcedureScheduleId))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // ResearchStudy
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgResearchStudy = modelBuilder.Entity<ResearchStudy>();
      cfgResearchStudy.ToTable("WdrResearchStudies");
      cfgResearchStudy.HasKey((e) => new {e.StudyWorkflowName, e.StudyWorkflowVersion});

      //////////////////////////////////////////////////////////////////////////////////////
      // ProcedureSchedule
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgProcedureSchedule = modelBuilder.Entity<ProcedureSchedule>();
      cfgProcedureSchedule.ToTable("WdrProcedureSchedules");
      cfgProcedureSchedule.HasKey((e) => e.ProcedureScheduleId);

      // PRINCIPAL: >>> ResearchStudy
      cfgProcedureSchedule
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.ProcedureSchedules )
        .HasForeignKey(nameof(ProcedureSchedule.StudyWorkflowName), nameof(ProcedureSchedule.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // DataRecordingTask
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgDataRecordingTask = modelBuilder.Entity<DataRecordingTask>();
      cfgDataRecordingTask.ToTable("WdrDataRecordingTasks");
      cfgDataRecordingTask.HasKey((e) => e.DataRecordingName);

      // PRINCIPAL: >>> ResearchStudy
      cfgDataRecordingTask
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.DataRecordingTasks )
        .HasForeignKey(nameof(DataRecordingTask.StudyWorkflowName), nameof(DataRecordingTask.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // InducedDataRecordingTask
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgInducedDataRecordingTask = modelBuilder.Entity<InducedDataRecordingTask>();
      cfgInducedDataRecordingTask.ToTable("WdrInducedDataRecordingTasks");
      cfgInducedDataRecordingTask.HasKey((e) => e.Id);

      // LOOKUP: >>> DataRecordingTask
      cfgInducedDataRecordingTask
        .HasOne((lcl) => lcl.InducedTask )
        .WithMany((rem) => rem.Inducements )
        .HasForeignKey(nameof(InducedDataRecordingTask.InducedDataRecordingName))
        .OnDelete(DeleteBehavior.Restrict);

      // PRINCIPAL: >>> TaskSchedule
      cfgInducedDataRecordingTask
        .HasOne((lcl) => lcl.TaskSchedule )
        .WithMany((rem) => rem.InducedDataRecordingTasks )
        .HasForeignKey(nameof(InducedDataRecordingTask.TaskScheduleId))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // DrugApplymentTask
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgDrugApplymentTask = modelBuilder.Entity<DrugApplymentTask>();
      cfgDrugApplymentTask.ToTable("WdrDrugAppliymentTasks");
      cfgDrugApplymentTask.HasKey((e) => e.DrugApplymentName);

      // PRINCIPAL: >>> ResearchStudy
      cfgDrugApplymentTask
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.DrugApplymentTasks )
        .HasForeignKey(nameof(DrugApplymentTask.StudyWorkflowName), nameof(DrugApplymentTask.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // InducedDrugApplymentTask
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgInducedDrugApplymentTask = modelBuilder.Entity<InducedDrugApplymentTask>();
      cfgInducedDrugApplymentTask.ToTable("WdrInducedDrugApplymentTasks");
      cfgInducedDrugApplymentTask.HasKey((e) => e.Id);

      // LOOKUP: >>> DrugApplymentTask
      cfgInducedDrugApplymentTask
        .HasOne((lcl) => lcl.InducedTask )
        .WithMany((rem) => rem.Inducements )
        .HasForeignKey(nameof(InducedDrugApplymentTask.InducedDrugApplymentName))
        .OnDelete(DeleteBehavior.Restrict);

      // PRINCIPAL: >>> TaskSchedule
      cfgInducedDrugApplymentTask
        .HasOne((lcl) => lcl.TaskSchedule )
        .WithMany((rem) => rem.InducedDrugApplymentTasks )
        .HasForeignKey(nameof(InducedDrugApplymentTask.TaskScheduleId))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // TaskSchedule
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgTaskSchedule = modelBuilder.Entity<TaskSchedule>();
      cfgTaskSchedule.ToTable("WdrTaskSchedules");
      cfgTaskSchedule.HasKey((e) => e.TaskScheduleId);

      // PRINCIPAL: >>> ResearchStudy
      cfgTaskSchedule
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.TaskSchedules )
        .HasForeignKey(nameof(TaskSchedule.StudyWorkflowName), nameof(TaskSchedule.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // InducedSubProcedureSchedule
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgInducedSubProcedureSchedule = modelBuilder.Entity<InducedSubProcedureSchedule>();
      cfgInducedSubProcedureSchedule.ToTable("WdrInducedSubProcedureSchedules");
      cfgInducedSubProcedureSchedule.HasKey((e) => e.Id);

      // PRINCIPAL: >>> ProcedureSchedule
      cfgInducedSubProcedureSchedule
        .HasOne((lcl) => lcl.ParentProcedureSchedule )
        .WithMany((rem) => rem.InducedSubProcedureSchedules )
        .HasForeignKey(nameof(InducedSubProcedureSchedule.ParentProcedureScheduleId))
        .OnDelete(DeleteBehavior.Cascade);

      // LOOKUP: >>> ProcedureSchedule
      cfgInducedSubProcedureSchedule
        .HasOne((lcl) => lcl.InducedProcedureSchedule )
        .WithMany((rem) => rem.InducingSubProcedureSchedules )
        .HasForeignKey(nameof(InducedSubProcedureSchedule.InducedProcedureScheduleId))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // InducedSubTaskSchedule
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgInducedSubTaskSchedule = modelBuilder.Entity<InducedSubTaskSchedule>();
      cfgInducedSubTaskSchedule.ToTable("WdrInducedSubTaskSchedules");
      cfgInducedSubTaskSchedule.HasKey((e) => e.Id);

      // PRINCIPAL: >>> TaskSchedule
      cfgInducedSubTaskSchedule
        .HasOne((lcl) => lcl.ParentTaskSchedule )
        .WithMany((rem) => rem.InducedSubTaskSchedules )
        .HasForeignKey(nameof(InducedSubTaskSchedule.ParentTaskScheduleId))
        .OnDelete(DeleteBehavior.Cascade);

      // LOOKUP: >>> TaskSchedule
      cfgInducedSubTaskSchedule
        .HasOne((lcl) => lcl.InducedTaskSchedule )
        .WithMany((rem) => rem.InducingTaskSchedules )
        .HasForeignKey(nameof(InducedSubTaskSchedule.InducedTaskScheduleId))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // InducedTreatmentTask
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgInducedTreatmentTask = modelBuilder.Entity<InducedTreatmentTask>();
      cfgInducedTreatmentTask.ToTable("WdrInducedTreatmentTasks");
      cfgInducedTreatmentTask.HasKey((e) => e.Id);

      // PRINCIPAL: >>> TaskSchedule
      cfgInducedTreatmentTask
        .HasOne((lcl) => lcl.TaskSchedule )
        .WithMany((rem) => rem.InducedTreatmentTasks )
        .HasForeignKey(nameof(InducedTreatmentTask.TaskScheduleId))
        .OnDelete(DeleteBehavior.Cascade);

      // LOOKUP: >>> TreatmentTask
      cfgInducedTreatmentTask
        .HasOne((lcl) => lcl.InducedTask )
        .WithMany((rem) => rem.Inducements )
        .HasForeignKey(nameof(InducedTreatmentTask.InducedTreatmentName))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // TreatmentTask
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgTreatmentTask = modelBuilder.Entity<TreatmentTask>();
      cfgTreatmentTask.ToTable("WdrTreatmentTasks");
      cfgTreatmentTask.HasKey((e) => e.TreatmentName);

      // PRINCIPAL: >>> ResearchStudy
      cfgTreatmentTask
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.TreatmentTasks )
        .HasForeignKey(nameof(TreatmentTask.StudyWorkflowName), nameof(TreatmentTask.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // InducedVisitProcedure
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgInducedVisitProcedure = modelBuilder.Entity<InducedVisitProcedure>();
      cfgInducedVisitProcedure.ToTable("WdrInducedVisitProcedures");
      cfgInducedVisitProcedure.HasKey((e) => e.Id);

      // PRINCIPAL: >>> ProcedureSchedule
      cfgInducedVisitProcedure
        .HasOne((lcl) => lcl.ProcedureSchedule )
        .WithMany((rem) => rem.InducedProcedures )
        .HasForeignKey(nameof(InducedVisitProcedure.ProcedureScheduleId))
        .OnDelete(DeleteBehavior.Cascade);

      // LOOKUP: >>> VisitProdecureDefinition
      cfgInducedVisitProcedure
        .HasOne((lcl) => lcl.InducedVisitProdecure )
        .WithMany((rem) => rem.Inducements )
        .HasForeignKey(nameof(InducedVisitProcedure.InducedVisitProdecureName))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // VisitProdecureDefinition
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgVisitProdecureDefinition = modelBuilder.Entity<VisitProdecureDefinition>();
      cfgVisitProdecureDefinition.ToTable("WdrVisitProdecureDefinitions");
      cfgVisitProdecureDefinition.HasKey((e) => e.VisitProdecureName);

      // PRINCIPAL: >>> ResearchStudy
      cfgVisitProdecureDefinition
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.VisitProdecureDefinitions )
        .HasForeignKey(nameof(VisitProdecureDefinition.StudyWorkflowName), nameof(VisitProdecureDefinition.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      // LOOKUP: >>> TaskSchedule
      cfgVisitProdecureDefinition
        .HasOne((lcl) => lcl.RootTaskSchedule )
        .WithMany((rem) => rem.EntryVisitProdecureDefinitions )
        .HasForeignKey(nameof(VisitProdecureDefinition.RootTaskScheduleId))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // ProcedureCycleDefinition
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgProcedureCycleDefinition = modelBuilder.Entity<ProcedureCycleDefinition>();
      cfgProcedureCycleDefinition.ToTable("WdrProcedureCycleDefinitions");
      cfgProcedureCycleDefinition.HasKey((e) => e.ProcedureScheduleId);

      // PRINCIPAL: >>> ProcedureSchedule
      cfgProcedureCycleDefinition
        .HasOne((lcl) => lcl.ProcedureSchedule )
        .WithOne((rem) => rem.CycleDefinition )
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // StudyEvent
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgStudyEvent = modelBuilder.Entity<StudyEvent>();
      cfgStudyEvent.ToTable("WdrStudyEvents");
      cfgStudyEvent.HasKey((e) => e.StudyEventName);

      // PRINCIPAL: >>> ResearchStudy
      cfgStudyEvent
        .HasOne((lcl) => lcl.ResearchStudy )
        .WithMany((rem) => rem.Events )
        .HasForeignKey(nameof(StudyEvent.StudyWorkflowName), nameof(StudyEvent.StudyWorkflowVersion))
        .OnDelete(DeleteBehavior.Cascade);

      //////////////////////////////////////////////////////////////////////////////////////
      // TaskCycleDefinition
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgTaskCycleDefinition = modelBuilder.Entity<TaskCycleDefinition>();
      cfgTaskCycleDefinition.ToTable("WdrTaskCycleDefinitions");
      cfgTaskCycleDefinition.HasKey((e) => e.TaskScheduleId);

      // PRINCIPAL: >>> TaskSchedule
      cfgTaskCycleDefinition
        .HasOne((lcl) => lcl.TaskSchedule )
        .WithOne((rem) => rem.CycleDefinition )
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
        WorkflowDefinitionDbContext c = new WorkflowDefinitionDbContext();
        c.Database.Migrate();
        _Migrated = true;
        c.Dispose();
      }
    }

   private static bool _Migrated = false;

    private static String _ConnectionString = "Server=(localdb)\\MsSqlLocalDb;Database=WorkflowDefinitionDbContext;Integrated Security=True;Persist Security Info=True;MultipleActiveResultSets=True;";
    public static String ConnectionString {
      set{ _ConnectionString = value;  }
    }

  }

}
