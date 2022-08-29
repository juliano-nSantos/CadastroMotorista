using AutoMapper;
using CadastroMotorista.Domain.Dtos;
using CadastroMotorista.Domain.Entities;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Domain.Interfaces.Services;
using CadastroMotorista.Domain.Models;
using CadastroMotorista.Infra.Shared.Extension;
using CadastroMotorista.Infra.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroMotorista.Service.Services
{
    public class MotoristaService : IService<MotoristaDto>
    {
        private readonly IRepository<Motorista> _repository;
        private readonly IMapper _mapper;

        public string MensagemErro { get; set; }

        public MotoristaService(IRepository<Motorista> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            MensagemErro = string.Empty;
        }

        public async Task<bool> AdicionarAsync(MotoristaDto entity)
        {
            bool result;
            try
            {
                entity.CPF = entity.CPF.RemovePontosTracosBarras();

                if (Validacoes.CpfValido(entity.CPF))
                {
                    var model = _mapper.Map<Motorista>(entity);                     

                    result = await _repository.AdicionarAsync(model);
                }
                else
                {
                    throw new ArgumentException("CPF inválido! ");                   
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<bool> AtualizarAsync(MotoristaDto entity)
        {
            bool result;

            try
            {
                var model = _mapper.Map<Motorista>(entity);
                result = await _repository.AtualizarAsync(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<MotoristaDto> BuscarPorCodigoAsync(int id)
        {
            MotoristaDto motorista;

            try
            {
                var result = await _repository.BuscarPorCodigoAsync(id);
                motorista = _mapper.Map<MotoristaDto>(result);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return motorista;
        }

        public async Task<List<MotoristaDto>> BuscarPorFiltroAsync(FiltroBusca filtroBusca)
        {
            string msg = string.Empty;
            var lstMotoristas = new List<MotoristaDto>();
            try
            {
                filtroBusca.CPF = filtroBusca.CPF.RemovePontosTracosBarras(); //Auxiliar.RemovePontosTracosString(filtroBusca.CPF);
                if (Validacoes.ValidaFiltroBusca(filtroBusca, out msg))
                {
                    var result = await _repository.BuscarPorFiltroAsync(filtroBusca);
                    lstMotoristas = _mapper.Map<List<MotoristaDto>>(result);
                }
                else
                {
                    MensagemErro = msg;
                }                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstMotoristas;            
        }

        public async Task<bool> DeletarAsync(int id)
        {
            bool result;

            try
            {
                result = await _repository.DeletarAsync(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
