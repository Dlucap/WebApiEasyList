using EasyList.Business.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class Compra : Entity
  {    
    public Guid FornecedorId { get; set; }
    public Guid FormaPagamentoId { get; set; }
    public bool Compartilhado { get; set; } = false;
    public StatusCompraEnum StatusCompra { get; set; }
    public IEnumerable<ItmCompra> ItemsCompra { get; set; }
    public DateTime DataCompra { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }

    /*EF Relation*/
    [JsonIgnore]
    public Fornecedor Fornecedor { get; set; }
    [JsonIgnore]
    public FormaPagamento FormaPagamento { get; set; }
   
    [JsonIgnore]
    public CompraCompartilhada CompraCompartilhada { get; set; }
  }  
}
