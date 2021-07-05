using EasyList.Api.Extensions;
using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using EasyList.Business.Services;
using EasyList.Data.Context;
using EasyList.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyList.Api.Configurations
{
  public static class DependecyInjectionConfig
  {
    public static IServiceCollection ResolveDependecies(this IServiceCollection services)
    {
      //todo configurar a resolução de dependencia
      services.AddScoped<MeuDbContext>();
      services.AddScoped<IProdutoRepository, ProdutoRepository>();
      services.AddScoped<IFornecedorRepository, FornecedorRepository>();
      services.AddScoped<IEnderecoRepository, EnderecoRepository>();
      services.AddScoped<IFormaPagamentoRepository,FormaPagamentoRepository>();

      //services.AddScoped<INotificador, Notificador>();
      //services.AddScoped<IProdutoService, ProdutoService>();
      services.AddScoped<IFornecedorService, FornecedorService>();

      //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      //services.AddScoped<IUser, AspNetUser>();

      //services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
      //services.AddTransient<ISchemaGenerator, SchemaGenerator>();

      //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

      return services;
    }


  }
}
