using System;
using System.Text.Json.Serialization;

namespace EasyList.Business.Models
{
  public class Endereco : Entity
  {
    //[JsonIgnore]
    public Guid FornecedorId { get; set; }
    public string Cep { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public int Numero { get; set; }
    /*EF Relation*/
    public Fornecedor Fornecedor { get; set; }
  }
}
