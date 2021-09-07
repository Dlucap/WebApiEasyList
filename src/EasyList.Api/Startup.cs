using AutoMapper;
using EasyList.Api.Configurations;
using EasyList.Api.Data;
using EasyList.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

      services.AddControllers();

      services.AddSwaggerConfig();
      
      //services.AddSwaggerGen(c =>
      //{
      //  c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiEasyList", Version = "v1" });
      //});
      // services.AddApplicationInsightsTelemetry();

      services.ResolveDependecies();

      _services = services;
    }

   
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiEasyList v1"));
        app.UseDeveloperExceptionPage();
      }

      #region All services
      //// add this if you want to add this for a particular path in an existing app
      //app.Map("/allservices", builder => builder.Run(async context =>
      //{
      //  var sb = new StringBuilder();
      //  sb.Append("<h1>All Services</h1>");
      //  sb.Append("<table><thead>");
      //  sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
      //  sb.Append("</thead><tbody>");
      //  foreach (var svc in _services)
      //  {
      //    sb.Append("<tr>");
      //    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
      //    sb.Append($"<td>{svc.Lifetime}</td>");
      //    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
      //    sb.Append("</tr>");
      //  }
      //  sb.Append("</tbody></table>");
      //  await context.Response.WriteAsync(sb.ToString());
      //}));

      //// otherwise just add this
      //app.Run(async context =>
      //{
      //  var sb = new StringBuilder();
      //  sb.Append("<h1>All Services</h1>");
      //  sb.Append("<table><thead>");
      //  sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
      //  sb.Append("</thead><tbody>");
      //  foreach (var svc in _services)
      //  {
      //    sb.Append("<tr>");
      //    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
      //    sb.Append($"<td>{svc.Lifetime}</td>");
      //    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
      //    sb.Append("</tr>");
      //  }
      //  sb.Append("</tbody></table>");
      //  await context.Response.WriteAsync(sb.ToString());
      //});
      #endregion All services

      app.UseMvcConfig();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

