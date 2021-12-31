using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EasyList.Api.Configurations
{
  public static class ApiConfig
  {
    public static IServiceCollection WebApiConfig(this IServiceCollection services)
    {
      services.AddControllers()
          .AddJsonOptions(options =>
          {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
          }
      );

      services.AddApiVersioning(options =>
      {
        options.AssumeDefaultVersionWhenUnspecified = true; 
        options.DefaultApiVersion = new ApiVersion(2, 0);      
        options.ReportApiVersions = true;
      });

      services.AddVersionedApiExplorer(options =>
      {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
      });

      services.Configure<ApiBehaviorOptions>(opt =>
      {
        opt.SuppressModelStateInvalidFilter = true;
      });

      return services;
    }

    public static IApplicationBuilder UseMvcConfig(this IApplicationBuilder app)
    {
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/health", new HealthCheckOptions()
        {
          Predicate = _ => true,
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
      });

      return app;
    }

  }
}
