using System;

namespace EasyList.Business.Models
{
  public class Categoria : Entity
  {
    public string NomeCategoria { get; set; }   
    public bool Ativo { get; set; }
    //public string UsuarioCriacao { get; set; }
    //public DateTime DataCriacao { get; set; }
    //public string UsuarioModificacao { get; set; }
    //public DateTime DataModificacao { get; set; }
  }
}
