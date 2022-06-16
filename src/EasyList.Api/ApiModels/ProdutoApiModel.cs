using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Api.ApiModels
{
    public class ProdutoApiModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }

    }
}
