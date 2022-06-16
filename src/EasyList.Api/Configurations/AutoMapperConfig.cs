using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Models;

namespace EasyList.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //TODO: CONFIGURAR  O AUTOMAPPER      
            CreateMap<Fornecedor, FornecedorApiModel>().ReverseMap();
            CreateMap<Endereco, EnderecoApiModel>().ReverseMap();
            CreateMap<Produto, ProdutoApiModel>().ReverseMap();
            CreateMap<FormaPagamento, FormaPagamentoApiModel>().ReverseMap();
            CreateMap<Compra, CompraApiModel>().ReverseMap();
            CreateMap<Categoria, CategoriaApiModel>().ReverseMap();
            CreateMap<ItmCompra, ItmCompraApiModel>().ReverseMap();
            CreateMap<CompraCompartilhada, CompraCompartilhadaApiModel>().ReverseMap();

            // Mapeando relações entre entidades
            CreateMap<CompraApiModel, Compra>()
               .ForMember(dest => dest.ItemsCompra, opt => opt.MapFrom(src => src.ItensCompra)).ReverseMap();
            
            CreateMap<FornecedorApiModel, Fornecedor>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco)).ReverseMap();
        }
    }
}
