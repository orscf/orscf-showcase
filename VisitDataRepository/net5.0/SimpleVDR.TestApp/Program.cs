using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MedicalResearch.VisitData {

  public class Program {

    static void Main(string[] args) {

      const string repoUrl = "https://localhost:44351";
      const string xApiKey = "WTXR7QPUXPFRWWTXR7QPUXPFRW";

      //////////////////////////////////////////////////////////////////////////////////////////
      
      HttpClient http = new HttpClient();
      http.DefaultRequestHeaders.Add("X-API-Key", xApiKey);
      VdrConnector repository = new VdrConnector(repoUrl, http);

      //////////////////////////////////////////////////////////////////////////////////////////
      
      Guid studyExec = Guid.Parse("{F0E860C8-D5DD-33B7-4601-7E3FDC3A9F1D}");

      //if (!repository.SearchStudyExecutionScopes($"StudyExecutionIdentifier==\"{studyExec}\"").Any()) {
      //  repository.AddNewStudyExecutionScope(
      //    new StudyExecutionScope {
      //      StudyExecutionIdentifier = studyExec,
      //      StudyWorkflowName = "DRUG-A",
      //      StudyWorkflowVersion = "1.0.0",
      //      ExecutingInstituteIdentifier = "INSTX"
      //    }
      //  );
      //}

      //var succ = repository.AddNewVisit(
      //  new Visit {
      //      VisitGuid = Guid.NewGuid(),
      //      VisitProdecureName = "FULL",
      //      VisitExecutionTitle = "V0",
      //      StudyExecutionIdentifier = studyExec,
      //      ParticipantIdentifier = "ALBERTEINSTEIN",
      //      ScheduledDateUtc = DateTime.UtcNow.Date
      //  }
      //);

      

      Console.WriteLine("loading visits...");
      IEnumerable<Visit> results = repository.SearchVisits("V0");

      Console.WriteLine("results:");
      foreach (Visit v in results) {
        Console.WriteLine();
        Console.WriteLine("VisitGuid:                 " + v.VisitGuid);
        Console.WriteLine("VisitProdecureName:        " + v.VisitProdecureName);
        Console.WriteLine("VisitExecutionTitle:       " + v.VisitExecutionTitle);
        Console.WriteLine("StudyExecutionIdentifier:  " + v.StudyExecutionIdentifier);
        Console.WriteLine("ParticipantIdentifier:     " + v.ParticipantIdentifier);
        Console.WriteLine("ScheduledDateUtc:          " + v.ScheduledDateUtc);
      }

      //////////////////////////////////////////////////////////////////////////////////////////
      Console.WriteLine();
      Console.WriteLine("hit return to exit!");
      Console.ReadLine();
    }

  }

}
