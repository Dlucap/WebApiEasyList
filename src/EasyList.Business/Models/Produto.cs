using System;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class Produto : Entity
  {   
    public Guid CategoriaId { get; set; }
    public string Marca { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }   
    //public string UsuarioCriacao { get; set; }
    //public DateTime DataCriacao { get; set; }
    //public string UsuarioModificacao { get; set; }
    //public DateTime DataModificacao{ get; set; }
    [JsonIgnore]
    public Categoria Categoria { get; set; }
  }
}
