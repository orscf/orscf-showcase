using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedicalResearch.VisitData.Persistence.EF;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Security;

namespace MedicalResearch.VisitData.RepositoryService {

  [ApiController]
  [Route("[controller]")]
  public class VisitsController : ControllerBase {
    
    private readonly ILogger<VisitsController> _logger;

    public VisitsController(ILogger<VisitsController> logger) {
      _logger = logger;
    }

    /// <summary>
    /// searches Visits matching the given criteria
    /// </summary>
    /// <param name="withParticipantIdentifier"> identity of the patient which can be a randomization or screening number (the exact semantic is defined per study)</param>
    /// <param name="withExecutingInstituteIdentifier"> the institute which is executing the study </param>
    /// <param name="withStudyIdentifier"> the study itself (this value is not related to any specific execution or institute) </param>
    /// <param name="withVisitIdentifier"> the VisitIdentifier as defined in the 'StudyWorkflowDefinition'</param>
    /// <param name="withExecutionState">0=Unscheduled / 1=Sheduled / 2=Executed / 3=AbortDuringExecution / 4=Skipped / 5=Removed</param>
    /// <returns>an array of found Visits matching the given criteria</returns>
    [HttpGet(), Produces("application/json")]
    [RequireValidApiKey()]
    [SwaggerOperation (OperationId = nameof(SearchVisits), Description = nameof(SearchVisits))]
    public IEnumerable<Visit> SearchVisits(
      string withParticipantIdentifier,
      string withExecutingInstituteIdentifier,
      string withStudyIdentifier,
      string withVisitIdentifier,
      VisitExecutionState? withExecutionState
    ) {

      Visit[] result;

      using (VisitDataDbContext db = new VisitDataDbContext()) {

        IQueryable<Visit> query = db.Visits.AccessScopeFiltered();
        
        if(withParticipantIdentifier != null) {
          query = query.Where(v => v.ParticipantIdentifier == withParticipantIdentifier);
        }
        if (withExecutingInstituteIdentifier != null) {
          query = query.Where(v => v.ExecutingInstituteIdentifier == withExecutingInstituteIdentifier);
        }
        if (withStudyIdentifier != null) {
          query = query.Where(v => v.StudyIdentifier == withStudyIdentifier);
        }
        if (withVisitIdentifier != null) {
          query = query.Where(v => v.VisitIdentifier == withVisitIdentifier);
        }
        if (withExecutionState.HasValue) {
          query = query.Where(v => v.ExecutionState == withExecutionState.Value);
        }

        result = query.ToArray();

      }

      return result;
    }

    /// <summary>
    /// picks the Visit which is addressed by the given RecordId
    /// </summary>
    /// <param name="recordId">target RecordId (specific for this repository)</param>
    /// <returns>the Visit which is addressed by the given RecordId or null, if is not existing</returns>
    [HttpGet("{recordId}"), Produces("application/json")]
    [RequireValidApiKey()]
    [SwaggerOperation(OperationId = nameof(GetVisitById), Description = nameof(GetVisitById))]
    public Visit GetVisitById(string recordId) {

      Visit result = null;

      using (VisitDataDbContext db = new VisitDataDbContext()) {

        result = db.Visits.AccessScopeFiltered().Where(v => v.RecordId == recordId).SingleOrDefault();

      }

      return result;
    }

    /// <summary>
    /// adds the given Visit as new Record to the Repository. A new RecordId will be assigned
    /// (any value of the 'RecordId'-Property of the given Visit will be ignored).
    /// </summary>
    /// <param name="visit">the new Visit</param>
    /// <returns>The newly generated RecordId which (specific for this repository)
    /// or null, if the visit data is invalid</returns>
    [HttpPost(), Produces("text/plain")]
    [RequireValidApiKey("add-visits")]
    [SwaggerOperation(OperationId = nameof(AddNewVisit), Description = nameof(AddNewVisit))]
    [SwaggerResponse((int)HttpStatusCode.OK, Type=typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public string AddNewVisit([FromBody][SwaggerRequestBody(Required=true)] Visit visit) {
      visit.AccessScopeGuard();

      string newRecordId = Guid.NewGuid().ToString();

      visit.RecordId = newRecordId;

      using (VisitDataDbContext db = new VisitDataDbContext()) {

        db.Visits.Add(visit);

        try {
          db.SaveChanges();
        }
        catch (Exception ex) {
          _logger.LogCritical(ex,ex.Message);
          return null;
        }

      }

      return newRecordId;
    }

    /// <summary>
    /// updates the Visit which is addressed by the value of the 'RecordId'-Property of the given Visit using the Contents of the given Visit
    /// </summary>
    /// <param name="visit">the visit</param>
    /// <returns>true, if the visit has been updated, otherwise false</returns>
    [HttpPut(), Produces("text/plain")]
    [RequireValidApiKey("update-visits")]
    [SwaggerOperation(OperationId = nameof(UpdateVisit), Description = nameof(UpdateVisit))]
    public bool UpdateVisit([FromBody][SwaggerRequestBody(Required=true)] Visit visit) {
      visit.AccessScopeGuard();

      string recordId = visit.RecordId;

      Visit existingVisit = null;

      using (VisitDataDbContext db = new VisitDataDbContext()) {

        existingVisit = db.Visits.AccessScopeFiltered().Where(v => v.RecordId == recordId).SingleOrDefault();

        if (existingVisit == null) {
          return false;
        }

        if (!this.UpdateVisit(visit, existingVisit)) {
          return false;
        }

        try {
          db.SaveChanges();
        }
        catch (Exception ex) {
          _logger.LogCritical(ex, ex.Message);
          return false;
        }

      }

      return true;
    }

    /// <summary>
    /// updates the Visit which is addressed by the given RecordId using the Contents of the given Visit
    /// (any value of the 'RecordId'-Property of the given Visit will be ignored).
    /// </summary>
    /// <param name="recordId">target RecordId (specific for this repository)</param>
    /// <param name="visit">the visit</param>
    /// <returns>true, if the visit has been updated, otherwise false</returns>
    [HttpPut("{recordId}"), Produces("text/plain")]
    [RequireValidApiKey("update-visits")]
    [SwaggerOperation(OperationId = nameof(UpdateVisitById), Description = nameof(UpdateVisitById))]
    public bool UpdateVisitById(string recordId, [FromBody][SwaggerRequestBody(Required=true)] Visit visit) {
      visit.AccessScopeGuard();

      Visit existingVisit = null;

      using (VisitDataDbContext db = new VisitDataDbContext()) {

        existingVisit = db.Visits.AccessScopeFiltered().Where(v => v.RecordId == recordId).SingleOrDefault();

        if (existingVisit == null) {
          return false;
        }

        if(!this.UpdateVisit(visit, existingVisit)) {
          return false;
        }

        try {
          db.SaveChanges();
        }
        catch (Exception ex) {
          _logger.LogCritical(ex, ex.Message);
          return false;
        }

      }

      return true;
    }

    /// <summary>
    /// deletes the Visit which is addressed by the given RecordId 
    /// </summary>
    /// <param name="recordId">target RecordId (specific for this repository)</param>
    /// <returns>true, if the visit has been deleted or wasnt exisiting, otherwise false</returns>
    [HttpDelete("{recordId}"), Produces("text/plain")]
    [RequireValidApiKey("delete-visits")]
    [SwaggerOperation(OperationId = nameof(DeleteVisit), Description = nameof(DeleteVisit))]
    public bool DeleteVisit(string recordId) {

      Visit existingVisit = null;

      using (VisitDataDbContext db = new VisitDataDbContext()) {
      
        existingVisit = db.Visits.AccessScopeFiltered().Where(v => v.RecordId == recordId).SingleOrDefault();

        if (existingVisit == null) {
          return true;
        }

        db.Visits.Remove(existingVisit);

        try {
          db.SaveChanges();
        }
        catch (Exception ex) {
          _logger.LogCritical(ex, ex.Message);
          return false;
        }

      }

      return true;
    }

    private bool UpdateVisit(Visit source, Visit target) {

      if (target.ExecutingInstituteIdentifier != source.ExecutingInstituteIdentifier)
        return false; //update of this value is not allowed

      if (target.ParticipantIdentifier != source.ParticipantIdentifier)
        return false; //update of this value is not allowed

      if (target.StudyExecutionIdentifier != source.StudyExecutionIdentifier)
        return false; //update of this value is not allowed

      if (target.StudyIdentifier != source.StudyIdentifier)
        return false; //update of this value is not allowed

      if (target.VisitIdentifier != source.VisitIdentifier)
        return false; //update of this value is not allowed

      target.ExecutionDateUtc = source.ExecutionDateUtc;
      target.ExecutionState = source.ExecutionState;
      target.ScheduledDateUtc = source.ScheduledDateUtc;

      return true;
    }

  }
}
