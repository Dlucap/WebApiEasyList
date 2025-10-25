using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EasyList.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyList.Api.Extensions;

namespace EasyList.Api.Configurations
{
  public static class IdentityConfig
  {
    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
       var stringSqlconnection = configuration.GetConnectionString("WebApiEasyList");

      services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(stringSqlconnection, ServerVersion.AutoDetect(stringSqlconnection)));

      services.AddIdentity<IdentityUser, IdentityRole>()/*options => options.SignIn.RequireConfirmedAccount = false)*/
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddErrorDescriber<IdentityMensagensPortugues>()
        .AddDefaultTokenProviders();

      #region  JWT
      var appSettingsSection = configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);

      var authBuilder = services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
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

      var googleClientId = configuration["Authentication:Google:ClientId"];
      var googleClientSecret = configuration["Authentication:Google:ClientSecret"];
      if (!string.IsNullOrEmpty(googleClientId) && !string.IsNullOrEmpty(googleClientSecret))
      {
        authBuilder.AddGoogle(googleOptions =>
        {
          googleOptions.ClientId = googleClientId;
          googleOptions.ClientSecret = googleClientSecret;
        });
      }

      var instagramClientId = configuration["Authentication:Instagram:ClientId"];
      var instagramClientSecret = configuration["Authentication:Instagram:ClientSecret"];
      if (!string.IsNullOrEmpty(instagramClientId) && !string.IsNullOrEmpty(instagramClientSecret))
      {
        authBuilder.AddInstagram(instagramOptions =>
        {
          instagramOptions.ClientId = instagramClientId;
          instagramOptions.ClientSecret = instagramClientSecret;
        });
      }
      #endregion  JWT

      return services;
    }

  }
}
