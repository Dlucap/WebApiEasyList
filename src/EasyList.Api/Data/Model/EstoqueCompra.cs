using System;

namespace EasyList.Api.Data
{//todo: https://www.learnentityframeworkcore.com/dbset/querying-data
  public class EstoqueCompra
  {

    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
  }
}
