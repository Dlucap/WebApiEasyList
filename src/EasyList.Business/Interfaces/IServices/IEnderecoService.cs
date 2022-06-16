using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
    public interface IEnderecoService : IDisposable
    {
        Task<bool> Adicionar(Endereco endereco);
        Task<bool> Atualizar(Endereco endereco);
        Task<bool> Remover(Guid id);
        Task<Endereco> ObterEnderecoPorId(Guid Id);
    }
}
