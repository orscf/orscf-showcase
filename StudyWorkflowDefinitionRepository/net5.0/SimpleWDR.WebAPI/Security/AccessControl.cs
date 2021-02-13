using System;
using System.Linq;
using System.Security;
using System.Threading;

namespace Security {

  public static class AccessControl {

    #region ambient provided scopes

    private static AsyncLocal<string> _ScopeToExecutingInstituteIdentifier = new AsyncLocal<string>();

    public static String ScopeToExecutingInstituteIdentifier {
      get { return _ScopeToExecutingInstituteIdentifier.Value; }
      set { _ScopeToExecutingInstituteIdentifier.Value = value; }
    }

    private static AsyncLocal<string> _ScopeToStudyIdentifier = new AsyncLocal<string>();

    public static String ScopeToStudyIdentifier {
      get { return _ScopeToStudyIdentifier.Value; }
      set { _ScopeToStudyIdentifier.Value = value; }
    }

    private static AsyncLocal<string[]> _CurrentPermissions = new AsyncLocal<string[]>();

    public static string[] CurrentPermissions {
      get { return _CurrentPermissions.Value; }
      set { _CurrentPermissions.Value = value; }
    }

    #endregion

    //public static IQueryable<Visit> AccessScopeFiltered(this IQueryable<Visit> unfiltered) {
    //  IQueryable<Visit> pipe = unfiltered;
    //  if (!string.IsNullOrWhiteSpace(ScopeToExecutingInstituteIdentifier)) {
    //    pipe = pipe.Where((Visit v) => v.ExecutingInstituteIdentifier == ScopeToExecutingInstituteIdentifier);
    //  }
    //  if (!string.IsNullOrWhiteSpace(ScopeToStudyIdentifier)) {
    //    pipe = pipe.Where((Visit v) => v.StudyIdentifier == ScopeToStudyIdentifier);
    //  }
    //  return pipe;
    //}

    //public static void AccessScopeGuard(this Visit visit) {
    //  if (!string.IsNullOrWhiteSpace(ScopeToExecutingInstituteIdentifier)) {
    //    if (visit.ExecutingInstituteIdentifier != ScopeToExecutingInstituteIdentifier) {
    //      throw new SecurityException($"Security rule requires {nameof(visit.ExecutingInstituteIdentifier)} to be '{ScopeToExecutingInstituteIdentifier}'");
    //    }
    //  }
    //  if (!string.IsNullOrWhiteSpace(ScopeToStudyIdentifier)) {
    //    if (visit.StudyIdentifier != ScopeToStudyIdentifier) {
    //      throw new SecurityException($"Security rule requires {nameof(visit.StudyIdentifier)} to be '{ScopeToStudyIdentifier}'");
    //    }
    //  }
    //}

  }
}
