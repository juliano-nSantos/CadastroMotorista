using AutoMapper;
using CadastroMotorista.Domain.Dtos;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Domain.Interfaces.Services;
using CadastroMotorista.Infra.Shared.Cryptography;
using CadastroMotorista.Infra.Shared.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMotorista.Service.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly IRepositoryLogin _repositoryLogin;
        private readonly IMapper _mapper;
        private readonly Hash _senhaHash;

        public ServiceLogin(IRepositoryLogin repositoryLogin, IMapper mapper)
        {
            _repositoryLogin = repositoryLogin;
            _mapper = mapper;
            _senhaHash = new Hash(SHA512.Create());
        }

        public async Task<UsuarioDto> ValidarLogin(LoginDto login)
        {
            var user = new UsuarioDto();            
            try
            {
                var usuario = await _repositoryLogin.FindByEmail(login.Email);

                if (usuario.Nome.NaoVazioNulo())
                {
                    if (_senhaHash.CheckPassword(login.Senha, usuario.Senha))
                    {
                        user = _mapper.Map<UsuarioDto>(usuario);                        
                    }
                        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }
    }
}
