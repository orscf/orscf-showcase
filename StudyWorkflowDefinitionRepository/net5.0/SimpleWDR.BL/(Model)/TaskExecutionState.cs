using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalResearch.Workflow {

  public enum TaskExecutionState {

    /// <summary> Sheduled </summary>
    Sheduled = 1,

    /// <summary> Executed </summary>
    Executed = 2,

    /// <summary> AbortDuringExecution </summary>
    AbortDuringExecution = 3,

    /// <summary> Skipped </summary>
    Skipped = 4,

  }

}
