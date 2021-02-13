using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using WebAPI;

namespace Security {

  public class AccessSettings {

  #region deserialization

    private static AccessSettings _Current = null;
    public static AccessSettings Current {
      get { 
       if(_Current == null) {
          string accessSettingsFileName = Startup.Configuration.GetValue<string>("AccessSettingsFileName");
          string rawFileContent = File.ReadAllText(accessSettingsFileName, Encoding.Default);
          _Current = JsonSerializer.Deserialize<AccessSettings>(rawFileContent);
        }
        return _Current;
      }
    }

  #endregion 

    public ApiKeyConfigurationEntry[] ApiKeys { get; set; }

  }

  public class ApiKeyConfigurationEntry {

    public string ApiKey { get; set; }

    public string[] AllowedHosts { get; set; }

    public DateTime Expires { get; set; }

    public string ScopeToStudyIdentifier { get; set; }

    public string ScopeToExecutingInstituteIdentifier { get; set; }

    public String[] Permissions { get; set; }

  }

}