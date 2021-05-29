using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiEasyList.Data;

namespace WebApiEasyList
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

   
    public IConfiguration Configuration { get; }

    public IServiceCollection _services { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var stringSqlconnection = Configuration.GetConnectionString("WebApiEasyList");
     
      services.AddDbContextPool<AuthJwtContext>(options =>
     options.UseMySql(stringSqlconnection,
       ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<CompraDbContext>(options =>
      options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<ItmCompraDbContext>(options =>
      options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<CompraCompartilhadaDbContext>(options =>
      options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<FornecedorDbContext>(options =>
      options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<ProdutoDbContext>(options =>
      options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<CategoriaDbContext>(options =>
    options.UseMySql(stringSqlconnection,
      ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddDbContextPool<FormaPagamentoDbContext>(options =>
    options.UseMySql(stringSqlconnection,
      ServerVersion.AutoDetect(stringSqlconnection)));

      /*
      services.AddDbContextPool<CompraDbContext>(options =>
      options.UseMySql(stringSqlconnection,
        ServerVersion.AutoDetect(stringSqlconnection)));

    //  services.AddDbContextPool<EstoqueCompraContext>(options =>
    //options.UseMySql(stringSqlconnection,
    //  ServerVersion.AutoDetect(stringSqlconnection)));
      */


      services.AddIdentity<IdentityUser,IdentityRole>()
          .AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<AuthJwtContext>()
          .AddDefaultTokenProviders();

      // JWT
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);


      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = appSettings.ValidoEm,
          ValidIssuer = appSettings.Emissor
        };
      });

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiEasyList", Version = "v1" });
      });
      // services.AddApplicationInsightsTelemetry();

      _services = services;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiEasyList v1"));
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

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication(); 

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

/**
 * User App
*{
*  "email": "dlpsilva@yahoo.com.br",
*  "password": "1A2b@6"
*}
*{
*  "email": "mestre@admin",
*  "password": "1A2b@6"
*}
*
*/