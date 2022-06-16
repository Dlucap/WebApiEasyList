using EasyList.Api.Extensions;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using EasyList.Business.Services;
using EasyList.Data.Context;
using EasyList.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyList.Api.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            #region Repository
            //todo configurar a resolução de dependencia
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<Business.Interfaces.IRepository.IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();
            services.AddScoped<ICompraCompartilhadaRepository, CompraCompartilhadaRepository>();
            services.AddScoped<IItmCompraRepository, ItmCompraRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            #endregion Repository
            //services.AddScoped<INotificador, Notificador>();
            //services.AddScoped<IProdutoService, ProdutoService>();

            #region Services
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IItmCompraService, ItmCompraService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<Business.Interfaces.IServices.IEnderecoService, EnderecoService>();
            #endregion Services

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            #region Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            #endregion Swagger

            return services;
        }
    }
}
