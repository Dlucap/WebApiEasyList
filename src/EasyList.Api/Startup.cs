using AutoMapper;
using EasyList.Api.Configurations;
using EasyList.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyList.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public IServiceCollection _services { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      var stringSqlconnection = Configuration.GetConnectionString("WebApiEasyList");

      services.AddDbContext<MeuDbContext>(options =>
       options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddIdentityConfig(Configuration);

      services.AddAutoMapper(typeof(Startup));

      services.WebApiConfig();

      services.AddControllers()
          .AddNewtonsoftJson();

      services.AddSwaggerConfig();

      services.ResolveDependecies();

      services.AddHealthChecks();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDeveloperExceptionPage();
      }

      app.UseMvcConfig();
      app.UseSwaggerConfig(provider);
    }
  }
}

