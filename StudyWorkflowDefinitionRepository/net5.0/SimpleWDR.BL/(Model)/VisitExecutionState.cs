using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalResearch.Workflow {

  /// <summary>
  /// 0=Unscheduled / 1=Sheduled / 2=Executed / 3=AbortDuringExecution / 4=Skipped / 5=Removed
  /// </summary>
  public enum VisitExecutionState {

    /// <summary> Unscheduled </summary>
    Unscheduled = 0,

    /// <summary> Sheduled </summary>
    Sheduled = 1,

    /// <summary> Executed </summary>
    Executed = 2,

    /// <summary> AbortDuringExecution </summary>
    AbortDuringExecution = 3,

    /// <summary> Skipped </summary>
    Skipped = 4,

    /// <summary> Removed </summary>
    Removed = 5,

  }

}
