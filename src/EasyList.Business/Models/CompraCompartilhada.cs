using EasyList.Business.Models;
using System;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class CompraCompartilhada : Entity
  {   
    public int CompraId { get; set; }
    public string UsuariosCompartilhados { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }

    [JsonIgnore]
    public Compra Compra { get; set; }

  }
}
