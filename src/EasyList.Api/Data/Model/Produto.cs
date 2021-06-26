using System;
using System.Text.Json.Serialization;

namespace EasyList.Api.Data
{
  public class Produto
  {
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public string Marca { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }   
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao{ get; set; }
    [JsonIgnore]
    public Categoria Categoria { get; set; }
  }
}
