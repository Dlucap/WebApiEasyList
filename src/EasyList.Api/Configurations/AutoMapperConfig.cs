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
      CreateMap<CompraCompartilhadaApiModel, CompraCompartilhadaApiModel>().ReverseMap(); 

      //CreateMap<ProdutoViewModel, Produto>()
      //    .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.NomeFornecedor));
    }
  }
}
