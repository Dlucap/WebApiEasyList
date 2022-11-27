using System.Collections.Generic;

namespace EasyList.Business.Models
{
    public class Paginado
    {
        public string PaginaAtual { get; set; }
        public int QuantidadePorPagina { get; set; }
        public int Total { get; set; }
        public IEnumerable<Entity> Entidades { get; set; }
    }
}
