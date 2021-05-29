using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEasyList.Data
{
  public class Compra
  {
    public int Id { get; set; }
    public int IdFornecedor { get; set; }
    public List<ItmCompra> ItemCompra { get; set; }
    public DateTime DataCompra { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }

    
  }  
}
