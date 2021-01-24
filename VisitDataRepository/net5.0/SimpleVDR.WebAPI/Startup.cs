using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ORSCF.VisitData.RepositoryService {

  public class Startup {

    public Startup(IConfiguration configuration) {
      _Configuration = configuration;
      Persistence.EF.VisitDataDbContext.ConnectionString = configuration.GetValue<String>("SqlConnectionString");
    }

    private static IConfiguration _Configuration = null;
    public static IConfiguration Configuration { get { return _Configuration; } }

    public void ConfigureServices(IServiceCollection services) {

      string outDir = AppDomain.CurrentDomain.BaseDirectory;

      services.AddControllers();
      
      services.AddSwaggerGen(c => {

        c.EnableAnnotations(true, true);

        c.IncludeXmlComments(outDir + "ORSCF.SimpleVisitDataRepository.WebAPI.xml", true);
        c.IncludeXmlComments(outDir + "ORSCF.SimpleVisitDataRepository.BL.xml", true);

        c.UseInlineDefinitionsForEnums();

        c.SwaggerDoc(
          "v1",
          new OpenApiInfo {
            Title = "Visit Data Repository",
            Version = "v1.0.0",
            Description = "Stores data for visits of research studies.",
            Contact = new OpenApiContact { 
              Name = "Open Research Study Communication Format",
              Email = "info@orscf.org",
              Url = new Uri("https://orscf.org")
            }
          }
        );

      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

      //if (env.IsDevelopment()) {

        app.UseDeveloperExceptionPage();

        app.UseSwagger(o => { 
        });

        app.UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyDefinitionRepository v1");
          //c.RoutePrefix = string.Empty;
          c.ConfigObject.DefaultModelExpandDepth = 2;
          
          c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
          c.DocumentTitle = "Study Definition Repository - OpenAPI Schema";
        });

      //}
      
      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });

    }
  }
}
