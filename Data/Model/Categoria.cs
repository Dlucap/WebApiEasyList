using System;

namespace WebApiEasyList.Data
{
  public class Categoria
  {

    public int Id { get; set; }
    public string NomeCategoria { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
  }
}
