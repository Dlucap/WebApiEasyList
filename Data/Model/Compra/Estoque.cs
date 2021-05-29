using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEasyList.Data
{
  public class Estoque
  {
    public int Id { get; set; }
    public int idproduto { get; set; }
    public int QuantidadeEstoque { get; set; }
    public DateTime Validade { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }
  }
}
