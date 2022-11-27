using System.Collections.Generic;

namespace EasyList.Api.ApiModels.Paginados
{
    public class FornecedorPaginado : PaginadoModel
    {
        public IEnumerable<FornecedorApiModel> Entidades { get; set; }
    }
}
