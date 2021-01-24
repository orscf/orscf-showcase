using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORSCF.VisitData {

  public class Visit {

    /// <summary>
    /// a repository-specific id
    /// </summary>
    public String RecordId { get; set; } = null;

    /// <summary>
    /// identity of the patient which can be a randomization or screening number
    /// (the exact semantic is defined per study)
    /// </summary>
    public String ParticipantIdentifier { get; set; } = null;

    /// <summary>
    /// the institute which is executing the study
    /// </summary>
    public String ExecutingInstituteIdentifier { get; set; } = null;


    /// <summary>
    /// the study itself (this value is not related to any specific execution or institute)
    /// </summary>
    public String StudyIdentifier { get; set; } = null;

    /// <summary>
    /// as defined in the 'StudyWorkflowDefinition'
    /// only one is possible per visit
    /// </summary>
    public String VisitIdentifier { get; set; } = null;


    /// <summary>
    /// the study itself (this value is related to the institute specific execution of the study)
    /// </summary>
    public String StudyExecutionIdentifier { get; set; } = String.Empty;

    public DateTime? ScheduledDateUtc { get; set; } = new DateTime?();

    public DateTime? ExecutionDateUtc { get; set; } = new DateTime?();

    public VisitExecutionState ExecutionState { get; set; } = VisitExecutionState.Unscheduled;



    public ObservableCollection<DataRecording> DataRecordings { get; set; } = new ObservableCollection<DataRecording>();
    public ObservableCollection<DrugApplyment> DrugApplyments { get; set; } = new ObservableCollection<DrugApplyment>();
    public ObservableCollection<Treatment> Treatments { get; set; } = new ObservableCollection<Treatment>();

  }

}
