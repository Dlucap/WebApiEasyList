﻿using EasyList.Business.Interfaces;
using EasyList.Data.Context;
using EasyList.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Api.Configurations
{
  public static class DependecyInjectionConfig
  {
    public static IServiceCollection ResolveDependecies(this IServiceCollection services)
    {
      //todo configurar a resolução de dependencia
      services.AddScoped<MeuDbContext>();
      //services.AddScoped<IProdutoRepository, ProdutoRepository>();
      services.AddScoped<IFornecedorRepository, FornecedorRepository>();
      services.AddScoped<IEnderecoRepository, EnderecoRepository>();

      //services.AddScoped<INotificador, Notificador>();
      //services.AddScoped<IProdutoService, ProdutoService>();
      //services.AddScoped<IFornecedorService, FornecedorService>();

      //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      //services.AddScoped<IUser, AspNetUser>();

      //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

      return services;
    }
  }
}