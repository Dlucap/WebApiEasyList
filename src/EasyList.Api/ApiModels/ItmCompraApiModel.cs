using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Api.ApiModels
{
  public class ItmCompraApiModel 
  {    
    public Guid Id { get; set; }
    public Guid CompraId { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime Validade { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
    //todo: Adicionar validações nos campos
  }
}
