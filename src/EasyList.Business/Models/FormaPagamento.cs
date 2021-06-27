using EasyList.Business.Models;
using System;

namespace EasyList.Business.Models
{
  public class FormaPagamento : Entity
  {
    public string NomeFormaPagamento { get; set; }
    public string UsuarioCriacao { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
  }
}
