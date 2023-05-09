using EasyList.Api.Extensions;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using EasyList.Business.Models.Email;
using EasyList.Business.Services;
using EasyList.Data.Context;
using EasyList.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyList.Api.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            services.AddScoped<MeuDbContext>();
            #endregion DbContext
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<FeatureManagement>(configuration.GetSection("FeatureManagement"));
            #region Repository
            //todo configurar a resolução de dependencia           
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();
            services.AddScoped<ICompraCompartilhadaRepository, CompraCompartilhadaRepository>();
            services.AddScoped<IItmCompraRepository, ItmCompraRepository>();           
            #endregion Repository
            //services.AddScoped<INotificador, Notificador>();
              
            #region Services
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<IItmCompraService, ItmCompraService>();
            services.AddScoped<IProdutoService, ProdutoService>();          
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IFormaPagamentoService, FormaPagamentoService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddFeatureManagement();

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
