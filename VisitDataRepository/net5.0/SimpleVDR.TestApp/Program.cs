using ORSCF.VisitData.RepositoryClient;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ORSCF.VisitData {

  public class Program {

    static void Main(string[] args) {

      const string repoUrl = "https://localhost:44351";
      const string xApiKey = "WTXR7QPUXPFRWWTXR7QPUXPFRW";

      //////////////////////////////////////////////////////////////////////////////////////////
      ///
      HttpClient http = new HttpClient();
      http.DefaultRequestHeaders.Add("X-API-Key", xApiKey);
      Client repository = new Client(repoUrl, http);

      //////////////////////////////////////////////////////////////////////////////////////////

      string id = repository.AddNewVisit(
        new Visit {
          StudyIdentifier = "STUDY4711",
          ExecutingInstituteIdentifier = "INST111",
          StudyExecutionIdentifier = "STUDY4711@INST111",
          VisitIdentifier ="V0",
          ParticipantIdentifier = "ALBERTEINSTEIN",
          ScheduledDateUtc = DateTime.UtcNow.Date
        }
      );

      Console.WriteLine("loading visits...");
      IEnumerable<Visit> results = repository.SearchVisits();
      Console.WriteLine("results:");

      foreach (Visit v in results) {
        Console.WriteLine();
        Console.WriteLine("RecordId:                     " + v.RecordId);
        Console.WriteLine("ExecutingInstituteIdentifier: " + v.ExecutingInstituteIdentifier);
        Console.WriteLine("StudyExecutionIdentifier:     " + v.StudyExecutionIdentifier);
        Console.WriteLine("VisitIdentifier:              " + v.VisitIdentifier);
        Console.WriteLine("ParticipantIdentifier:        " + v.ParticipantIdentifier);
        Console.WriteLine("ScheduledDateUtc:             " + v.ScheduledDateUtc);
      }

      //////////////////////////////////////////////////////////////////////////////////////////
      Console.WriteLine();
      Console.WriteLine("hit return to exit!");
      Console.ReadLine();
    }

  }

}
