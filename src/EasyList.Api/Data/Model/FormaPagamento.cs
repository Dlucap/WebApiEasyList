using System;

namespace EasyList.Api.Data
{
  public class FormaPagamento
  {
    public int Id { get; set; }
    public string NomeFormaPagamento { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
  }
}
