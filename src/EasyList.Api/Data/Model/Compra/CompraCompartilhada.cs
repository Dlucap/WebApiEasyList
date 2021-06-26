using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEasyList.Data
{
  public class CompraCompartilhada
  {
    public int Id { get; set; }
    public int IdCompra { get; set; }
    public string UsuariosCompartilhados { get; set; }

    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }

  }
}
