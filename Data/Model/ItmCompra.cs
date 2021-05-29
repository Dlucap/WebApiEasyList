using System;

namespace WebApiEasyList.Data
{
  public class ItmCompra
  {
    public int Id { get; set; }
    public int IdCompra { get; set; }
    public int IdProduto { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }
  }
}
