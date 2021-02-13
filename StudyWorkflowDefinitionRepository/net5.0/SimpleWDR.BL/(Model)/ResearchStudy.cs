using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalResearch.Workflow {

  public class ResearchStudy {

    public String InvariantIdentifier { get; set; } = String.Empty;
    public String OfficialLabel { get; set; } = String.Empty;

    /*
           VersionNumber="1.0.2"  Semver!!!!                           Bleibt bis zum verladden des editogmode
           VesionKey=YYYYMMdd-HHmmss-EDITAUZTHORNAME     nur bei ENTR DRAFTMODE!!!
           LastChange=YYYYMMdd-HHmmss                               bei jeder änderung
           DraftState: 0=RELEASED|1=EDITING_FIX|2=EDITING_MINOR|3=EDITING_MAJOR

           An ui wird die berechnerte neue version angezeigt!
           Beim RELEASE wird dann version oben hochgezählt und EditState Verlassen ->0 
      
     */

    /// <summary>
    /// This value follows the rules of 'Semantic Versioning' (https://semver.org) and
    /// needs to be updated exactly and only on transition to DraftState.Released!
    ///  - if the previously DraftState was 'DraftNewFix', then the 3. number must be increased at this time!
    ///  - if the previously DraftState was 'DraftNewMinor', then the 2. number must be increased, and the 3. number must be set to 0 at this time!
    ///  - if the previously DraftState was 'DraftNewMajor', then the 1. number must be increased, and the 2.+3. number must be set to 0 at this time!
    /// </summary>
    public String Version { get; set; } = "0.0.0";

    /// <summary>
    /// This value needs to be updated exactly on:
    ///   1. initial creation  OR
    ///   2. transition away from DraftState.Released to any other DraftState!
    ///   
    /// Is MUST NOT be updated on every change during Draft!
    /// 
    /// Format: the Author, which is starting the new Draft 
    /// (Alphanumeric, in PascalCase without blanks or other Symbols) + 
    /// the current UTC-Time when setting the value (in ISO8601 format) separated by a Pipe "|"
    /// 
    /// Sample: "MaxMustermann|2020-06-15T13:45:30.0000000Z".
    /// </summary>
    public String VersionIdentity { get; set; } = null;

    /// <summary>
    /// 
    /// </summary>
    public DateTime LastChangeUtc { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Enumeration:
    ///   0=Foo |
    ///   1=Bar
    /// </summary>
    public DraftState DraftState { get; set; } = DraftState.DraftNewMinor;







    public ObservableCollection<StudyEvent> Events { get; set; } = new ObservableCollection<StudyEvent>();

  }

}
