using AutoMapper;
using CadastroMotorista.Domain.Dtos;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMotorista.Service.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IMapper _mapper;

        public ServiceUsuario(IRepositoryUsuario repositoryUsuario, IMapper mapper)
        {
            _repositoryUsuario = repositoryUsuario;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> GetUserById(int id)
        {
            var user = new UsuarioDto();

            try
            {
                var usuario = await _repositoryUsuario.GetUserById(id);

                user = _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }
    }
}
