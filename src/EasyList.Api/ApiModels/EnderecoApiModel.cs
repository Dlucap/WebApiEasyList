using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Api.ApiModels
{
  public class EnderecoApiModel
  {
    [Key]
    public Guid Id { get; set; } 
  
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 8)]
    public string Cep { get; set; }
   
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(2, ErrorMessage = "O campo {0} precisa ter {1} caracteres")]
    public string Estado { get; set; }
   
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Cidade { get; set; }
   
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Logradouro { get; set; }

    [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Complemento { get; set; }

    //[Required(ErrorMessage = "O campo {0} é obrigatório")]
    //[StringLength(5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
    public int Numero { get; set; }
    [JsonIgnore]
    public Guid FornecedorId { get; set; }

  }
}
