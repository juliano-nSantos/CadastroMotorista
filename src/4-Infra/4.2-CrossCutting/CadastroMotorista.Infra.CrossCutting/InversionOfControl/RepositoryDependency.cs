using Microsoft.Extensions.DependencyInjection;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Infra.Data.Repositories;
using CadastroMotorista.Domain.Entities;

namespace CadastroMotorista.Infra.CrossCutting.InversionOfControl
{
    public static class RepositoryDependency
    {
        public static void AddRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped<IExampleRepository, ExampleRepository>();
            services.AddScoped<IRepository<Motorista>, MotoristaRepository>();
            services.AddScoped<IRepositoryLogin, RepositoryLogin>();
            services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();
        }
    }
}