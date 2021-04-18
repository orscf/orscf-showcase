using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalResearch.VisitData {

  public class AccessSpecs {

    public bool CanRead { get; set; } = false;
    public bool CanAddNew { get; set; } = false;
    public bool CanUpdate { get; set; } = false;
    public bool CanDelete { get; set; } = false;

  }

}
