using AutoMapper;
using CadastroMotorista.Domain.Dtos;
using CadastroMotorista.Domain.Entities;

namespace CadastroMotorista.Portal.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Motorista, MotoristaDto>()
                .ReverseMap();
            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();
        }
    }
}
