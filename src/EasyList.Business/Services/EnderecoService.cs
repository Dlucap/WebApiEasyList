using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class EnderecoService : Interfaces.IServices.IEnderecoService
    {
        public Interfaces.IRepository.IEnderecoRepository _enderecoRepository { get; set; }
        public EnderecoService(Interfaces.IRepository.IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }      
                
        public async Task<bool> Adicionar(Endereco endereco)
        {
            if (!await EnderecoExists(endereco.Id))
                return false;

            await _enderecoRepository.Adicionar(endereco);
            return true;
        }             

        public async Task<bool> Atualizar(Endereco endereco)
        {
            if (!await EnderecoExists(endereco.Id))
                return false;

            await _enderecoRepository.Atualizar(endereco);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (!await EnderecoExists(id))
                return false;

            await _enderecoRepository.Remover(id);
            return true;
        }

        public async Task<Endereco> ObterEnderecoPorId(Guid Id)
        {
            return await _enderecoRepository.ObterPorId(Id);
        }

        private Task<bool> EnderecoExists(object id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _enderecoRepository?.Dispose();
        }

    }
}
