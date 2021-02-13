using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Security {

  [ApiController]
  [Route("[controller]")]
  public class PermissionsController : ControllerBase {
    
    private readonly ILogger<PermissionsController> _logger;

    public PermissionsController(ILogger<PermissionsController> logger) {
      _logger = logger;
    }

    /// <summary>
    /// returns the set of permissions which are assigned to the api-key of the request
    /// </summary>
    /// <returns>the set of permissions which are assigned to the api-key of the request</returns>
    [HttpGet(), Produces("application/json")]
    [RequireValidApiKey()]
    [SwaggerOperation(OperationId = nameof(GetPermissions), Description = nameof(GetPermissions))]
    public string[] GetPermissions() {
      return AccessControl.CurrentPermissions;
    }

  }
}
