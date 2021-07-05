using EasyList.Business.Enum;
using System;

namespace EasyList.Business.Models
{
  public class CompraApiModels : Entity
  {    
    public Guid FornecedorId { get; set; }
    public Guid FormaPagamentoId { get; set; }
    public bool Compartilhado { get; set; } = false;
    public StatusCompraEnum StatusCompra { get; set; }
    public DateTime DataCompra { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }

    
  }  
}
