using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ORSCF.VisitData.RepositoryService {

  [AttributeUsage(validOn: AttributeTargets.Method)]
  public class RequireValidApiKeyAttribute : Attribute, IAsyncActionFilter {

    private string[] _RequiredPermissions;

    public RequireValidApiKeyAttribute(params string[] requiredExplicitPermissions) {
      _RequiredPermissions = requiredExplicitPermissions;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {

      if (!context.HttpContext.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey)) {
        context.Result = new ContentResult() {
          StatusCode = 401,
          Content = "'X-API-Key'-Header was not provided"
        };
        return;
      }

      ApiKeyConfigurationEntry keyConfig = AccessSettings.Current.ApiKeys.Where(e=>e.ApiKey.Equals(extractedApiKey)).SingleOrDefault();
      if(keyConfig == null) {
        context.Result = new ContentResult() {
          StatusCode = 401,
          Content = "'X-API-Key'-Header doesn't contain a valid api key"
        };
        return;
      }

      if (keyConfig.Expires> DateTime.MinValue && keyConfig.Expires < DateTime.Now) {
        context.Result = new ContentResult() {
          StatusCode = 401,
          Content = "the provided api key has expired"
        };
        return;
      }

      if (keyConfig.AllowedHosts != null && keyConfig.AllowedHosts.Length > 0) {
        if (!keyConfig.AllowedHosts.Contains(context.HttpContext.Request.Host.Host.ToLower())){
          context.Result = new ContentResult() {
            StatusCode = 401,
            Content = "access denied by firewall rules"
          };
          return;
        }
      }

      if (_RequiredPermissions.Length > 0) {
        foreach (string requiredPermission in _RequiredPermissions) {
          if (keyConfig.Permissions == null || !keyConfig.Permissions.Contains(requiredPermission)) {
            context.Result = new ContentResult() {
              StatusCode = 401,
              Content = "missing permissions for this operation"
            };
            return;
          }
        }
      }

      AccessControl.ScopeToExecutingInstituteIdentifier = keyConfig.ScopeToExecutingInstituteIdentifier;
      AccessControl.ScopeToStudyIdentifier = keyConfig.ScopeToStudyIdentifier;

      await next();
    }
  }

}