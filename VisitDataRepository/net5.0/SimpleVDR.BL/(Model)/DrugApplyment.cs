using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORSCF.VisitData {

  public class DrugApplyment {

    /// <summary>
    /// a repository-specific id
    /// </summary>
    public String RecordId { get; set; } = null;

    /// <summary>
    /// Identifier as defined in the 'StudyWorkflowDefinition' (only one is possible per visit)
    /// or 'null' which indicates, that this was a unplanned task which is not defined in the 'StudyWorkflowDefinition'
    /// </summary>
    public String TaskIdentifier { get; set; } = null;
    public String ShortSubject { get; set; } = string.Empty;

    public DateTime? ScheduledDateTimeUtc { get; set; } = new DateTime?();
    public DateTime? ExecutionDateTimeUtc { get; set; } = new DateTime?();
    public TaskExecutionState ExecutionState { get; set; } = TaskExecutionState.Sheduled;

    public String DrugName { get; set; } = null;
    public decimal DrugDoseMgPerUnit { get; set; } = 0;
    public decimal AppliedUnits { get; set; } = 0;

    public String InstructionsForExecution { get; set; } = string.Empty; 
    public String NotesRegardingOutcome { get; set; } = string.Empty;

  }

}
