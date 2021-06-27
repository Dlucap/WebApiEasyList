using System;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class ItmCompra : Entity
  {
    public int CompraId { get; set; }
    public int ProdutoId { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime Validade { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
    /*EF Relation*/
    [JsonIgnore]
    public Compra Compra { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; }
  }
}
