using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace MedicalResearch.VisitData {

public class DataRecording {

  /// <summary> the internal record id which is managed by the VisitData repository *this field has a max length of 50 </summary>
  [FixedAfterCreation, MaxLength(50), Required]
  public String RecordId { get; set; }

  /// <summary> the internal record id of the parent visit *this field has a max length of 50 </summary>
  [MaxLength(50), Required]
  public String VisitRecordId { get; set; }

  [Principal]
  public virtual Visit Visit { get; set; }

}

public class Visit {

  /// <summary> the internal record id which is managed by the VisitData repository *this field has a max length of 50 </summary>
  [FixedAfterCreation, MaxLength(50), Required]
  public String RecordId { get; set; }

  /// <summary> identity of the patient which can be a randomization or screening number (the exact semantic is defined per study) </summary>
  [FixedAfterCreation, Required]
  public String ParticipantIdentifier { get; set; }

  /// <summary> the institute which is executing the study (this should be an invariant technical representation of the company name or a guid) </summary>
  [Required]
  public String ExecutingInstituteIdentifier { get; set; }

  /// <summary> unique invariant name of the study itself as defined in the 'StudyWorkflowDefinition' (originated from the sponsor) </summary>
  [Required]
  public String StudyIdentifier { get; set; }

  /// <summary> unique invariant name of the visit procedure as defined in the 'StudyWorkflowDefinition' (originated from the sponsor) </summary>
  [Required]
  public String VisitIdentifier { get; set; }

  /// <summary> id of the execution (related to the institute specific execution of the study) </summary>
  [Required]
  public String StudyExecutionIdentifier { get; set; }

  /// <summary> the estimated date when the visit is scheduled for execution </summary>
  [Required]
  public String ScheduledDateUtc { get; set; }

  /// <summary> the real date, when the visits was executed </summary>
  [Required]
  public String ExecutionDateUtc { get; set; }

  /// <summary> 0=Unscheduled / 1=Sheduled / 2=Executed / 3=AbortDuringExecution / 4=Skipped / 5=Removed </summary>
  [Required]
  public Int32 ExecutionState { get; set; }

  [Dependent]
  public virtual ObservableCollection<DataRecording> DataRecordings { get; set; } = new ObservableCollection<DataRecording>();

  [Dependent]
  public virtual ObservableCollection<DrugApplyment> DrugApplyments { get; set; } = new ObservableCollection<DrugApplyment>();

  [Dependent]
  public virtual ObservableCollection<Treatment> Treatments { get; set; } = new ObservableCollection<Treatment>();

}

public class DrugApplyment {

  /// <summary> the internal record id which is managed by the VisitData repository *this field has a max length of 50 </summary>
  [FixedAfterCreation, MaxLength(50), Required]
  public String RecordId { get; set; }

  /// <summary> the internal record id of the parent visit *this field has a max length of 50 </summary>
  [MaxLength(50), Required]
  public String VisitRecordId { get; set; }

  [Principal]
  public virtual Visit Visit { get; set; }

}

public class Treatment {

  /// <summary> the internal record id which is managed by the VisitData repository *this field has a max length of 50 </summary>
  [FixedAfterCreation, MaxLength(50), Required]
  public String RecordId { get; set; }

  /// <summary> the internal record id of the parent visit *this field has a max length of 50 </summary>
  [MaxLength(50), Required]
  public String VisitRecordId { get; set; }

  [Principal]
  public virtual Visit Visit { get; set; }

}

}
