using MedicalResearch.VisitData.Persistence.EF;
using MedicalResearch.VisitData.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Security;
using System;
using System.Collections.Generic;
using System.Data.AccessControl;
using System.Linq;
using System.Net;

namespace MedicalResearch.VisitData.RepositoryService {

[ApiController]
[Route("dataRecordings")]
public partial class DataRecordingsController : ControllerBase {

  public const String SchemaVersion = "1.3.0";

  private readonly ILogger<DataRecordingsController> _logger;
  private readonly MedicalResearch.VisitData.Repository.IDataRecordingRepository _Repo;

  public DataRecordingsController(ILogger<DataRecordingsController> logger) {
    _logger = logger;
    _Repo = new MedicalResearch.VisitData.Persistence.EF.DataRecordingStore();
  }

  /// <summary> Returns an info object, which specifies the possible operations (accessor specific permissions) when accessing DataRecordings.</summary>
  [RequireValidApiKey()]
  [HttpGet("-"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetDataRecordingAccessSpecs), Description = nameof(GetDataRecordingAccessSpecs))]
  public AccessSpecs GetDataRecordingAccessSpecs(){
    try {
      return AccessControlContext.Current.GetAccessSpecs("DataRecording");  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads a specific DataRecording addressed by the given primary identifier. Returns null on failure, or if no record exists with the given identity.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("read-datarecording")]
  [HttpGet("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetDataRecordingByTaskGuid), Description = nameof(GetDataRecordingByTaskGuid))]
  public DataRecording GetDataRecordingByTaskGuid(Guid taskGuid){
    try {
      return _Repo.GetDataRecordingByTaskGuid(taskGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads DataRecordings. </summary>
  /// <param name="page">Number of the page, which should be returned </param>
  /// <param name="pageSize">Max count of DataRecordings which should be returned </param>
  [RequireValidApiKey("read-datarecording")]
  [HttpGet(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetDataRecordings), Description = nameof(GetDataRecordings))]
  public DataRecording[] GetDataRecordings([FromQuery] int page = 1, [FromQuery] int pageSize = 20){
    try {
      return _Repo.GetDataRecordings(page, pageSize); 
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }


  /// <summary> Loads DataRecordings where values matching to ALL fields of the given 'filterValues' object.</summary>
  /// <param name="filterExpression">a filter expression like '((FieldName1 == "ABC" &amp;&amp; FieldName2 &gt; 12) || FieldName2 != "")'</param>
  /// <param name="sort">one or more property names which are used as sort order (before pagination)</param>
  /// <param name="page">Number of the page, which should be returned</param>
  /// <param name="pageSize">Max count of DataRecordings which should be returned</param>
  [RequireValidApiKey("read-datarecording")]
  [HttpPost("search"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(SearchDataRecordings), Description = nameof(SearchDataRecordings))]
  public DataRecording[] SearchDataRecordings([FromBody][SwaggerRequestBody(Required=true)] String filterExpression, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] String sort = null ){
    try {
      return _Repo.SearchDataRecordings(filterExpression, sort, page, pageSize);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Adds a new DataRecording and returns success. </summary>
  /// <param name="dataRecording"> DataRecording containing the new values </param>
  [RequireValidApiKey("add-datarecording")]
  [HttpPost(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(AddNewDataRecording), Description = nameof(AddNewDataRecording))]
  public bool AddNewDataRecording([FromBody][SwaggerRequestBody(Required=true)] DataRecording dataRecording){
    try {
      return _Repo.AddNewDataRecording(dataRecording);  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given DataRecording addressed by the primary identifier fields within the given DataRecording. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="dataRecording"> DataRecording containing the new values (the primary identifier fields within the given DataRecording will be used to address the target record) </param>
  [RequireValidApiKey("update-datarecording")]
  [HttpPut(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateDataRecording), Description = nameof(UpdateDataRecording))]
  public bool UpdateDataRecording([FromBody][SwaggerRequestBody(Required=true)] DataRecording dataRecording){
    try {
      return _Repo.UpdateDataRecording(dataRecording);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given DataRecording addressed by the supplementary given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  /// <param name="dataRecording"> DataRecording containing the new values (the primary identifier fields within the given DataRecording will be ignored) </param>
  [RequireValidApiKey("update-datarecording")]
  [HttpPut("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateDataRecordingByTaskGuid), Description = nameof(UpdateDataRecordingByTaskGuid))]
  public bool UpdateDataRecordingByTaskGuid(Guid taskGuid, [FromBody][SwaggerRequestBody(Required=true)] DataRecording dataRecording){
    try {
      return _Repo.UpdateDataRecordingByTaskGuid(taskGuid, dataRecording);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Deletes a specific DataRecording addressed by the given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("delete-datarecording")]
  [HttpDelete("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(DeleteDataRecordingByTaskGuid), Description = nameof(DeleteDataRecordingByTaskGuid))]
  public bool DeleteDataRecordingByTaskGuid(Guid taskGuid){
    try {
      return _Repo.DeleteDataRecordingByTaskGuid(taskGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

}

[ApiController]
[Route("visits")]
public partial class VisitsController : ControllerBase {

  public const String SchemaVersion = "1.3.0";

  private readonly ILogger<VisitsController> _logger;
  private readonly MedicalResearch.VisitData.Repository.IVisitRepository _Repo;

  public VisitsController(ILogger<VisitsController> logger) {
    _logger = logger;
    _Repo = new MedicalResearch.VisitData.Persistence.EF.VisitStore();
  }

  /// <summary> Returns an info object, which specifies the possible operations (accessor specific permissions) when accessing Visits.</summary>
  [RequireValidApiKey()]
  [HttpGet("-"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetVisitAccessSpecs), Description = nameof(GetVisitAccessSpecs))]
  public AccessSpecs GetVisitAccessSpecs(){
    try {
      return AccessControlContext.Current.GetAccessSpecs("Visit");  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads a specific Visit addressed by the given primary identifier. Returns null on failure, or if no record exists with the given identity.</summary>
  /// <param name="visitGuid"> a global unique id of a concrete study-visit execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("read-visit")]
  [HttpGet("{visitGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetVisitByVisitGuid), Description = nameof(GetVisitByVisitGuid))]
  public Visit GetVisitByVisitGuid(Guid visitGuid){
    try {
      return _Repo.GetVisitByVisitGuid(visitGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads Visits. </summary>
  /// <param name="page">Number of the page, which should be returned </param>
  /// <param name="pageSize">Max count of Visits which should be returned </param>
  [RequireValidApiKey("read-visit")]
  [HttpGet(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetVisits), Description = nameof(GetVisits))]
  public Visit[] GetVisits([FromQuery] int page = 1, [FromQuery] int pageSize = 20){
    try {
      return _Repo.GetVisits(page, pageSize); 
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }


  /// <summary> Loads Visits where values matching to ALL fields of the given 'filterValues' object.</summary>
  /// <param name="filterExpression">a filter expression like '((FieldName1 == "ABC" &amp;&amp; FieldName2 &gt; 12) || FieldName2 != "")'</param>
  /// <param name="sort">one or more property names which are used as sort order (before pagination)</param>
  /// <param name="page">Number of the page, which should be returned</param>
  /// <param name="pageSize">Max count of Visits which should be returned</param>
  [RequireValidApiKey("read-visit")]
  [HttpPost("search"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(SearchVisits), Description = nameof(SearchVisits))]
  public Visit[] SearchVisits([FromBody][SwaggerRequestBody(Required=true)] String filterExpression, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] String sort = null ){
    try {
      return _Repo.SearchVisits(filterExpression, sort, page, pageSize);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Adds a new Visit and returns success. </summary>
  /// <param name="visit"> Visit containing the new values </param>
  [RequireValidApiKey("add-visit")]
  [HttpPost(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(AddNewVisit), Description = nameof(AddNewVisit))]
  public bool AddNewVisit([FromBody][SwaggerRequestBody(Required=true)] Visit visit){
    try {
      return _Repo.AddNewVisit(visit);  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given Visit addressed by the primary identifier fields within the given Visit. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="visit"> Visit containing the new values (the primary identifier fields within the given Visit will be used to address the target record) </param>
  [RequireValidApiKey("update-visit")]
  [HttpPut(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateVisit), Description = nameof(UpdateVisit))]
  public bool UpdateVisit([FromBody][SwaggerRequestBody(Required=true)] Visit visit){
    try {
      return _Repo.UpdateVisit(visit);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given Visit addressed by the supplementary given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="visitGuid"> a global unique id of a concrete study-visit execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  /// <param name="visit"> Visit containing the new values (the primary identifier fields within the given Visit will be ignored) </param>
  [RequireValidApiKey("update-visit")]
  [HttpPut("{visitGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateVisitByVisitGuid), Description = nameof(UpdateVisitByVisitGuid))]
  public bool UpdateVisitByVisitGuid(Guid visitGuid, [FromBody][SwaggerRequestBody(Required=true)] Visit visit){
    try {
      return _Repo.UpdateVisitByVisitGuid(visitGuid, visit);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Deletes a specific Visit addressed by the given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="visitGuid"> a global unique id of a concrete study-visit execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("delete-visit")]
  [HttpDelete("{visitGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(DeleteVisitByVisitGuid), Description = nameof(DeleteVisitByVisitGuid))]
  public bool DeleteVisitByVisitGuid(Guid visitGuid){
    try {
      return _Repo.DeleteVisitByVisitGuid(visitGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

}

[ApiController]
[Route("drugApplyments")]
public partial class DrugApplymentsController : ControllerBase {

  public const String SchemaVersion = "1.3.0";

  private readonly ILogger<DrugApplymentsController> _logger;
  private readonly MedicalResearch.VisitData.Repository.IDrugApplymentRepository _Repo;

  public DrugApplymentsController(ILogger<DrugApplymentsController> logger) {
    _logger = logger;
    _Repo = new MedicalResearch.VisitData.Persistence.EF.DrugApplymentStore();
  }

  /// <summary> Returns an info object, which specifies the possible operations (accessor specific permissions) when accessing DrugApplyments.</summary>
  [RequireValidApiKey()]
  [HttpGet("-"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetDrugApplymentAccessSpecs), Description = nameof(GetDrugApplymentAccessSpecs))]
  public AccessSpecs GetDrugApplymentAccessSpecs(){
    try {
      return AccessControlContext.Current.GetAccessSpecs("DrugApplyment");  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads a specific DrugApplyment addressed by the given primary identifier. Returns null on failure, or if no record exists with the given identity.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("read-drugapplyment")]
  [HttpGet("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetDrugApplymentByTaskGuid), Description = nameof(GetDrugApplymentByTaskGuid))]
  public DrugApplyment GetDrugApplymentByTaskGuid(Guid taskGuid){
    try {
      return _Repo.GetDrugApplymentByTaskGuid(taskGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads DrugApplyments. </summary>
  /// <param name="page">Number of the page, which should be returned </param>
  /// <param name="pageSize">Max count of DrugApplyments which should be returned </param>
  [RequireValidApiKey("read-drugapplyment")]
  [HttpGet(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetDrugApplyments), Description = nameof(GetDrugApplyments))]
  public DrugApplyment[] GetDrugApplyments([FromQuery] int page = 1, [FromQuery] int pageSize = 20){
    try {
      return _Repo.GetDrugApplyments(page, pageSize); 
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }


  /// <summary> Loads DrugApplyments where values matching to ALL fields of the given 'filterValues' object.</summary>
  /// <param name="filterExpression">a filter expression like '((FieldName1 == "ABC" &amp;&amp; FieldName2 &gt; 12) || FieldName2 != "")'</param>
  /// <param name="sort">one or more property names which are used as sort order (before pagination)</param>
  /// <param name="page">Number of the page, which should be returned</param>
  /// <param name="pageSize">Max count of DrugApplyments which should be returned</param>
  [RequireValidApiKey("read-drugapplyment")]
  [HttpPost("search"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(SearchDrugApplyments), Description = nameof(SearchDrugApplyments))]
  public DrugApplyment[] SearchDrugApplyments([FromBody][SwaggerRequestBody(Required=true)] String filterExpression, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] String sort = null ){
    try {
      return _Repo.SearchDrugApplyments(filterExpression, sort, page, pageSize);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Adds a new DrugApplyment and returns success. </summary>
  /// <param name="drugApplyment"> DrugApplyment containing the new values </param>
  [RequireValidApiKey("add-drugapplyment")]
  [HttpPost(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(AddNewDrugApplyment), Description = nameof(AddNewDrugApplyment))]
  public bool AddNewDrugApplyment([FromBody][SwaggerRequestBody(Required=true)] DrugApplyment drugApplyment){
    try {
      return _Repo.AddNewDrugApplyment(drugApplyment);  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given DrugApplyment addressed by the primary identifier fields within the given DrugApplyment. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="drugApplyment"> DrugApplyment containing the new values (the primary identifier fields within the given DrugApplyment will be used to address the target record) </param>
  [RequireValidApiKey("update-drugapplyment")]
  [HttpPut(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateDrugApplyment), Description = nameof(UpdateDrugApplyment))]
  public bool UpdateDrugApplyment([FromBody][SwaggerRequestBody(Required=true)] DrugApplyment drugApplyment){
    try {
      return _Repo.UpdateDrugApplyment(drugApplyment);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given DrugApplyment addressed by the supplementary given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  /// <param name="drugApplyment"> DrugApplyment containing the new values (the primary identifier fields within the given DrugApplyment will be ignored) </param>
  [RequireValidApiKey("update-drugapplyment")]
  [HttpPut("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateDrugApplymentByTaskGuid), Description = nameof(UpdateDrugApplymentByTaskGuid))]
  public bool UpdateDrugApplymentByTaskGuid(Guid taskGuid, [FromBody][SwaggerRequestBody(Required=true)] DrugApplyment drugApplyment){
    try {
      return _Repo.UpdateDrugApplymentByTaskGuid(taskGuid, drugApplyment);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Deletes a specific DrugApplyment addressed by the given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("delete-drugapplyment")]
  [HttpDelete("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(DeleteDrugApplymentByTaskGuid), Description = nameof(DeleteDrugApplymentByTaskGuid))]
  public bool DeleteDrugApplymentByTaskGuid(Guid taskGuid){
    try {
      return _Repo.DeleteDrugApplymentByTaskGuid(taskGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

}

[ApiController]
[Route("studyEvents")]
public partial class StudyEventsController : ControllerBase {

  public const String SchemaVersion = "1.3.0";

  private readonly ILogger<StudyEventsController> _logger;
  private readonly MedicalResearch.VisitData.Repository.IStudyEventRepository _Repo;

  public StudyEventsController(ILogger<StudyEventsController> logger) {
    _logger = logger;
    _Repo = new MedicalResearch.VisitData.Persistence.EF.StudyEventStore();
  }

  /// <summary> Returns an info object, which specifies the possible operations (accessor specific permissions) when accessing StudyEvents.</summary>
  [RequireValidApiKey()]
  [HttpGet("-"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetStudyEventAccessSpecs), Description = nameof(GetStudyEventAccessSpecs))]
  public AccessSpecs GetStudyEventAccessSpecs(){
    try {
      return AccessControlContext.Current.GetAccessSpecs("StudyEvent");  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads a specific StudyEvent addressed by the given primary identifier. Returns null on failure, or if no record exists with the given identity.</summary>
  /// <param name="eventGuid"> a global unique id of a concrete study-event occurrence which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("read-studyevent")]
  [HttpGet("{eventGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetStudyEventByEventGuid), Description = nameof(GetStudyEventByEventGuid))]
  public StudyEvent GetStudyEventByEventGuid(Guid eventGuid){
    try {
      return _Repo.GetStudyEventByEventGuid(eventGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads StudyEvents. </summary>
  /// <param name="page">Number of the page, which should be returned </param>
  /// <param name="pageSize">Max count of StudyEvents which should be returned </param>
  [RequireValidApiKey("read-studyevent")]
  [HttpGet(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetStudyEvents), Description = nameof(GetStudyEvents))]
  public StudyEvent[] GetStudyEvents([FromQuery] int page = 1, [FromQuery] int pageSize = 20){
    try {
      return _Repo.GetStudyEvents(page, pageSize); 
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }


  /// <summary> Loads StudyEvents where values matching to ALL fields of the given 'filterValues' object.</summary>
  /// <param name="filterExpression">a filter expression like '((FieldName1 == "ABC" &amp;&amp; FieldName2 &gt; 12) || FieldName2 != "")'</param>
  /// <param name="sort">one or more property names which are used as sort order (before pagination)</param>
  /// <param name="page">Number of the page, which should be returned</param>
  /// <param name="pageSize">Max count of StudyEvents which should be returned</param>
  [RequireValidApiKey("read-studyevent")]
  [HttpPost("search"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(SearchStudyEvents), Description = nameof(SearchStudyEvents))]
  public StudyEvent[] SearchStudyEvents([FromBody][SwaggerRequestBody(Required=true)] String filterExpression, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] String sort = null ){
    try {
      return _Repo.SearchStudyEvents(filterExpression, sort, page, pageSize);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Adds a new StudyEvent and returns success. </summary>
  /// <param name="studyEvent"> StudyEvent containing the new values </param>
  [RequireValidApiKey("add-studyevent")]
  [HttpPost(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(AddNewStudyEvent), Description = nameof(AddNewStudyEvent))]
  public bool AddNewStudyEvent([FromBody][SwaggerRequestBody(Required=true)] StudyEvent studyEvent){
    try {
      return _Repo.AddNewStudyEvent(studyEvent);  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given StudyEvent addressed by the primary identifier fields within the given StudyEvent. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="studyEvent"> StudyEvent containing the new values (the primary identifier fields within the given StudyEvent will be used to address the target record) </param>
  [RequireValidApiKey("update-studyevent")]
  [HttpPut(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateStudyEvent), Description = nameof(UpdateStudyEvent))]
  public bool UpdateStudyEvent([FromBody][SwaggerRequestBody(Required=true)] StudyEvent studyEvent){
    try {
      return _Repo.UpdateStudyEvent(studyEvent);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given StudyEvent addressed by the supplementary given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="eventGuid"> a global unique id of a concrete study-event occurrence which is usually originated at the primary CRF or study management system ('SMS') </param>
  /// <param name="studyEvent"> StudyEvent containing the new values (the primary identifier fields within the given StudyEvent will be ignored) </param>
  [RequireValidApiKey("update-studyevent")]
  [HttpPut("{eventGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateStudyEventByEventGuid), Description = nameof(UpdateStudyEventByEventGuid))]
  public bool UpdateStudyEventByEventGuid(Guid eventGuid, [FromBody][SwaggerRequestBody(Required=true)] StudyEvent studyEvent){
    try {
      return _Repo.UpdateStudyEventByEventGuid(eventGuid, studyEvent);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Deletes a specific StudyEvent addressed by the given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="eventGuid"> a global unique id of a concrete study-event occurrence which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("delete-studyevent")]
  [HttpDelete("{eventGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(DeleteStudyEventByEventGuid), Description = nameof(DeleteStudyEventByEventGuid))]
  public bool DeleteStudyEventByEventGuid(Guid eventGuid){
    try {
      return _Repo.DeleteStudyEventByEventGuid(eventGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

}

[ApiController]
[Route("studyExecutionScopes")]
public partial class StudyExecutionScopesController : ControllerBase {

  public const String SchemaVersion = "1.3.0";

  private readonly ILogger<StudyExecutionScopesController> _logger;
  private readonly MedicalResearch.VisitData.Repository.IStudyExecutionScopeRepository _Repo;

  public StudyExecutionScopesController(ILogger<StudyExecutionScopesController> logger) {
    _logger = logger;
    _Repo = new MedicalResearch.VisitData.Persistence.EF.StudyExecutionScopeStore();
  }

  /// <summary> Returns an info object, which specifies the possible operations (accessor specific permissions) when accessing StudyExecutionScopes.</summary>
  [RequireValidApiKey()]
  [HttpGet("-"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetStudyExecutionScopeAccessSpecs), Description = nameof(GetStudyExecutionScopeAccessSpecs))]
  public AccessSpecs GetStudyExecutionScopeAccessSpecs(){
    try {
      return AccessControlContext.Current.GetAccessSpecs("StudyExecutionScope");  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads a specific StudyExecutionScope addressed by the given primary identifier. Returns null on failure, or if no record exists with the given identity.</summary>
  /// <param name="studyExecutionIdentifier"> a global unique id of a concrete study execution (dedicated to a concrete institute) which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("read-studyexecutionscope")]
  [HttpGet("{studyExecutionIdentifier}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetStudyExecutionScopeByStudyExecutionIdentifier), Description = nameof(GetStudyExecutionScopeByStudyExecutionIdentifier))]
  public StudyExecutionScope GetStudyExecutionScopeByStudyExecutionIdentifier(Guid studyExecutionIdentifier){
    try {
      return _Repo.GetStudyExecutionScopeByStudyExecutionIdentifier(studyExecutionIdentifier);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads StudyExecutionScopes. </summary>
  /// <param name="page">Number of the page, which should be returned </param>
  /// <param name="pageSize">Max count of StudyExecutionScopes which should be returned </param>
  [RequireValidApiKey("read-studyexecutionscope")]
  [HttpGet(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetStudyExecutionScopes), Description = nameof(GetStudyExecutionScopes))]
  public StudyExecutionScope[] GetStudyExecutionScopes([FromQuery] int page = 1, [FromQuery] int pageSize = 20){
    try {
      return _Repo.GetStudyExecutionScopes(page, pageSize); 
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }


  /// <summary> Loads StudyExecutionScopes where values matching to ALL fields of the given 'filterValues' object.</summary>
  /// <param name="filterExpression">a filter expression like '((FieldName1 == "ABC" &amp;&amp; FieldName2 &gt; 12) || FieldName2 != "")'</param>
  /// <param name="sort">one or more property names which are used as sort order (before pagination)</param>
  /// <param name="page">Number of the page, which should be returned</param>
  /// <param name="pageSize">Max count of StudyExecutionScopes which should be returned</param>
  [RequireValidApiKey("read-studyexecutionscope")]
  [HttpPost("search"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(SearchStudyExecutionScopes), Description = nameof(SearchStudyExecutionScopes))]
  public StudyExecutionScope[] SearchStudyExecutionScopes([FromBody][SwaggerRequestBody(Required=true)] String filterExpression, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] String sort = null ){
    try {
      return _Repo.SearchStudyExecutionScopes(filterExpression, sort, page, pageSize);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Adds a new StudyExecutionScope and returns success. </summary>
  /// <param name="studyExecutionScope"> StudyExecutionScope containing the new values </param>
  [RequireValidApiKey("add-studyexecutionscope")]
  [HttpPost(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(AddNewStudyExecutionScope), Description = nameof(AddNewStudyExecutionScope))]
  public bool AddNewStudyExecutionScope([FromBody][SwaggerRequestBody(Required=true)] StudyExecutionScope studyExecutionScope){
    try {
      return _Repo.AddNewStudyExecutionScope(studyExecutionScope);  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given StudyExecutionScope addressed by the primary identifier fields within the given StudyExecutionScope. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="studyExecutionScope"> StudyExecutionScope containing the new values (the primary identifier fields within the given StudyExecutionScope will be used to address the target record) </param>
  [RequireValidApiKey("update-studyexecutionscope")]
  [HttpPut(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateStudyExecutionScope), Description = nameof(UpdateStudyExecutionScope))]
  public bool UpdateStudyExecutionScope([FromBody][SwaggerRequestBody(Required=true)] StudyExecutionScope studyExecutionScope){
    try {
      return _Repo.UpdateStudyExecutionScope(studyExecutionScope);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given StudyExecutionScope addressed by the supplementary given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="studyExecutionIdentifier"> a global unique id of a concrete study execution (dedicated to a concrete institute) which is usually originated at the primary CRF or study management system ('SMS') </param>
  /// <param name="studyExecutionScope"> StudyExecutionScope containing the new values (the primary identifier fields within the given StudyExecutionScope will be ignored) </param>
  [RequireValidApiKey("update-studyexecutionscope")]
  [HttpPut("{studyExecutionIdentifier}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateStudyExecutionScopeByStudyExecutionIdentifier), Description = nameof(UpdateStudyExecutionScopeByStudyExecutionIdentifier))]
  public bool UpdateStudyExecutionScopeByStudyExecutionIdentifier(Guid studyExecutionIdentifier, [FromBody][SwaggerRequestBody(Required=true)] StudyExecutionScope studyExecutionScope){
    try {
      return _Repo.UpdateStudyExecutionScopeByStudyExecutionIdentifier(studyExecutionIdentifier, studyExecutionScope);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Deletes a specific StudyExecutionScope addressed by the given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="studyExecutionIdentifier"> a global unique id of a concrete study execution (dedicated to a concrete institute) which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("delete-studyexecutionscope")]
  [HttpDelete("{studyExecutionIdentifier}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(DeleteStudyExecutionScopeByStudyExecutionIdentifier), Description = nameof(DeleteStudyExecutionScopeByStudyExecutionIdentifier))]
  public bool DeleteStudyExecutionScopeByStudyExecutionIdentifier(Guid studyExecutionIdentifier){
    try {
      return _Repo.DeleteStudyExecutionScopeByStudyExecutionIdentifier(studyExecutionIdentifier);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

}

[ApiController]
[Route("treatments")]
public partial class TreatmentsController : ControllerBase {

  public const String SchemaVersion = "1.3.0";

  private readonly ILogger<TreatmentsController> _logger;
  private readonly MedicalResearch.VisitData.Repository.ITreatmentRepository _Repo;

  public TreatmentsController(ILogger<TreatmentsController> logger) {
    _logger = logger;
    _Repo = new MedicalResearch.VisitData.Persistence.EF.TreatmentStore();
  }

  /// <summary> Returns an info object, which specifies the possible operations (accessor specific permissions) when accessing Treatments.</summary>
  [RequireValidApiKey()]
  [HttpGet("-"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetTreatmentAccessSpecs), Description = nameof(GetTreatmentAccessSpecs))]
  public AccessSpecs GetTreatmentAccessSpecs(){
    try {
      return AccessControlContext.Current.GetAccessSpecs("Treatment");  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads a specific Treatment addressed by the given primary identifier. Returns null on failure, or if no record exists with the given identity.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("read-treatment")]
  [HttpGet("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetTreatmentByTaskGuid), Description = nameof(GetTreatmentByTaskGuid))]
  public Treatment GetTreatmentByTaskGuid(Guid taskGuid){
    try {
      return _Repo.GetTreatmentByTaskGuid(taskGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Loads Treatments. </summary>
  /// <param name="page">Number of the page, which should be returned </param>
  /// <param name="pageSize">Max count of Treatments which should be returned </param>
  [RequireValidApiKey("read-treatment")]
  [HttpGet(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(GetTreatments), Description = nameof(GetTreatments))]
  public Treatment[] GetTreatments([FromQuery] int page = 1, [FromQuery] int pageSize = 20){
    try {
      return _Repo.GetTreatments(page, pageSize); 
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }


  /// <summary> Loads Treatments where values matching to ALL fields of the given 'filterValues' object.</summary>
  /// <param name="filterExpression">a filter expression like '((FieldName1 == "ABC" &amp;&amp; FieldName2 &gt; 12) || FieldName2 != "")'</param>
  /// <param name="sort">one or more property names which are used as sort order (before pagination)</param>
  /// <param name="page">Number of the page, which should be returned</param>
  /// <param name="pageSize">Max count of Treatments which should be returned</param>
  [RequireValidApiKey("read-treatment")]
  [HttpPost("search"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(SearchTreatments), Description = nameof(SearchTreatments))]
  public Treatment[] SearchTreatments([FromBody][SwaggerRequestBody(Required=true)] String filterExpression, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] String sort = null ){
    try {
      return _Repo.SearchTreatments(filterExpression, sort, page, pageSize);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return null;
    }
  }

  /// <summary> Adds a new Treatment and returns success. </summary>
  /// <param name="treatment"> Treatment containing the new values </param>
  [RequireValidApiKey("add-treatment")]
  [HttpPost(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(AddNewTreatment), Description = nameof(AddNewTreatment))]
  public bool AddNewTreatment([FromBody][SwaggerRequestBody(Required=true)] Treatment treatment){
    try {
      return _Repo.AddNewTreatment(treatment);  
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given Treatment addressed by the primary identifier fields within the given Treatment. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="treatment"> Treatment containing the new values (the primary identifier fields within the given Treatment will be used to address the target record) </param>
  [RequireValidApiKey("update-treatment")]
  [HttpPut(), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateTreatment), Description = nameof(UpdateTreatment))]
  public bool UpdateTreatment([FromBody][SwaggerRequestBody(Required=true)] Treatment treatment){
    try {
      return _Repo.UpdateTreatment(treatment);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Updates all values (which are not "FixedAfterCreation") of the given Treatment addressed by the supplementary given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  /// <param name="treatment"> Treatment containing the new values (the primary identifier fields within the given Treatment will be ignored) </param>
  [RequireValidApiKey("update-treatment")]
  [HttpPut("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(UpdateTreatmentByTaskGuid), Description = nameof(UpdateTreatmentByTaskGuid))]
  public bool UpdateTreatmentByTaskGuid(Guid taskGuid, [FromBody][SwaggerRequestBody(Required=true)] Treatment treatment){
    try {
      return _Repo.UpdateTreatmentByTaskGuid(taskGuid, treatment);   
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

  /// <summary> Deletes a specific Treatment addressed by the given primary identifier. Returns false on failure or if no target record was found, otherwise true.</summary>
  /// <param name="taskGuid"> a global unique id of a concrete study-task execution which is usually originated at the primary CRF or study management system ('SMS') </param>
  [RequireValidApiKey("delete-treatment")]
  [HttpDelete("{taskGuid}"), Produces("application/json")]
  [SwaggerOperation(OperationId = nameof(DeleteTreatmentByTaskGuid), Description = nameof(DeleteTreatmentByTaskGuid))]
  public bool DeleteTreatmentByTaskGuid(Guid taskGuid){
    try {
      return _Repo.DeleteTreatmentByTaskGuid(taskGuid);
    }
    catch (Exception ex) {
      _logger.LogCritical(ex, ex.Message);
      return false;
    }
  }

}

}
