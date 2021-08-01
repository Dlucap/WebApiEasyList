using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Api.ApiModels
{
  public class CompraCompartilhadaApiModel 
  {
    [Key]
    public Guid Id { get; set; }
    public Guid CompraId { get; set; }
    public string UsuariosCompartilhados { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
    //todo: Adicionar validações nos campos
  }
}
