using MedicalResearch.Workflow.Persistence.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace WebAPI {

  public class Startup {

    public Startup(IConfiguration configuration) {
      _Configuration = configuration;
      WorkflowDefinitionDbContext.ConnectionString = configuration.GetValue<String>("SqlConnectionString");
    }

    private static IConfiguration _Configuration = null;
    public static IConfiguration Configuration { get { return _Configuration; } }

    public void ConfigureServices(IServiceCollection services) {

      WorkflowDefinitionDbContext.Migrate();

      string outDir = AppDomain.CurrentDomain.BaseDirectory;

      services.AddControllers();
      
      services.AddSwaggerGen(c => {

        c.EnableAnnotations(true, true);

        c.IncludeXmlComments(outDir + "ORSCF.WorkflowDefinitionRepository.WebAPI.xml", true);
        c.IncludeXmlComments(outDir + "ORSCF.WorkflowDefinitionRepository.BL.xml", true);

        c.UseInlineDefinitionsForEnums();

        c.SwaggerDoc(
          "v1",
          new OpenApiInfo {
            Title = "Workflow Definition Repository API",
            Version = "v1.0.0",
            Description = "stores workflow definitions for research studies (digital representations of study-protocols)",
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
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkflowDefinitionRepositoryApi v1");
          //c.RoutePrefix = string.Empty;
          c.ConfigObject.DefaultModelExpandDepth = 2;
          
          c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
          c.DocumentTitle = "Workflow Definition Repository - OpenAPI Schema";
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
