using CadastroMotorista.Domain.Dtos;
using CadastroMotorista.Domain.Entities;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Domain.Interfaces.Services;

namespace CadastroMotorista.Service.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _exampleRepository;

        public ExampleService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public void InsertExample(ExampleDto example)
        {
            var model = new  Example();
            _exampleRepository.InsertExample(model);
        }
    }
}