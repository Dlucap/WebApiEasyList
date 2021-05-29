using System;

namespace WebApiEasyList.Data
{
  public class Compra
  {
    public int Id { get; set; }
    public int IdFornecedor { get; set; }
    public int IdFormaPagamento { get; set; }
    public bool Compartilhado { get; set; } = false;

    public DateTime DataCompra { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }

    
  }  
}
