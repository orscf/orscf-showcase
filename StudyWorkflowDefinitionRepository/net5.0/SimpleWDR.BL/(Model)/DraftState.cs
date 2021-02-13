using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalResearch.Workflow {

  // FOO
  public enum DraftState {

    /// <summary> Released </summary>
    Released = 0,

    /// <summary> DraftNewFix </summary>
    DraftNewFix = 1,

    /// <summary> DraftNewMinor </summary>
    DraftNewMinor = 2,

    /// <summary> DraftNewMajor </summary>
    DraftNewMajor = 3
  }

}
