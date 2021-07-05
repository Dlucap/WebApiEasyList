using System;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class Endereco : Entity
  {   
    public string Cep { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public int Numero { get; set; }
    [JsonIgnore]
    public Fornecedor Fornecedor { get; set; }
  }
}
