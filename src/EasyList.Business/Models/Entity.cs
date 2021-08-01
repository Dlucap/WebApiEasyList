using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public abstract class Entity 
  {
    protected Entity()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string UsuarioCriacao { get; set; }
    [JsonIgnore]
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    [JsonIgnore]
    public DateTime DataModificacao { get; set; }
  }
}
