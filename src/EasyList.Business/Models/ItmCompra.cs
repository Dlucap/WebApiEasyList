using System;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class ItmCompra : Entity
  {
    public Guid CompraId { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime Validade { get; set; }
    public bool Ativo { get; set; }
    //public string UsuarioCriacao { get; set; }
    //public DateTime DataCriacao { get; set; }
    //public string UsuarioModificacao { get; set; }
    //public DateTime DataModificacao { get; set; }
    /*EF Relation*/
    [JsonIgnore]
    public Compra Compra { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; }
  }
}
