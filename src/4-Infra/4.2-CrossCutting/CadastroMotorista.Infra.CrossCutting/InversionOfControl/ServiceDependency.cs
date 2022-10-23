using Microsoft.Extensions.DependencyInjection;
using CadastroMotorista.Domain.Interfaces.Services;
using CadastroMotorista.Infra.Shared.WriteLogs;
using CadastroMotorista.Infra.Shared.WriteLogs.Interface;
using CadastroMotorista.Service.Services;
using CadastroMotorista.Domain.Dtos;

namespace CadastroMotorista.Infra.CrossCutting.InversionOfControl
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IExampleService, ExampleService>();
            services.AddTransient<IWriteLogCollections, WriteLogCollections>();
            services.AddScoped<IService<MotoristaDto>, MotoristaService>();
            services.AddScoped<IServiceLogin, ServiceLogin>();
        }
    }
}