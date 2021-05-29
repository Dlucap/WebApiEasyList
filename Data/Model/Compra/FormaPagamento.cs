using System;

namespace WebApiEasyList.Data
{
  public class FormaPagamento
  {
    public int Id { get; set; }
    public string NomeFormaPagamento { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }
  }
}
