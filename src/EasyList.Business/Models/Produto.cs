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
        public bool Ativo { get; set; }
        public bool ControlaEstoque { get; set; } = false;

        [JsonIgnore]
        public Categoria Categoria { get; set; }
    }
}
