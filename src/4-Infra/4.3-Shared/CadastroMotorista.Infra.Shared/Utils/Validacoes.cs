using CadastroMotorista.Domain.Models;
using CadastroMotorista.Infra.Shared.Extension;

namespace CadastroMotorista.Infra.Shared.Utils
{
    public static class Validacoes
    {
        public static bool CpfValido(string cpf)
        {
            if (!cpf.NaoVazioNulo() || cpf.Length < 11 || cpf.TodosCaracteresIguais())
                return false;            

            return cpf.CpfValido();

        }

        public static bool ValidaIdadeFinalMaiorIdadeInicial(int idadeInicial, int idadeFinal)
        {
            if (idadeInicial > 0 && idadeFinal > 0 && idadeInicial > idadeFinal)
                return false;

            return true;
        }

        public static bool ValidaFiltroBusca(FiltroBusca filtroBusca, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            if(!IdadeValida(filtroBusca.IdadeInicial) && !IdadeValida(filtroBusca.IdadeFinal) && !CampoPreenchido(filtroBusca.CPF)
                && !CampoPreenchido(filtroBusca.Nome) && filtroBusca.Ativo == null)
            {
                mensagemErro = "Preencha ao menos 1 filtro para realizar a busca!";
                return false;
            }

            if(CampoPreenchido(filtroBusca.CPF) && !CpfValido(filtroBusca.CPF))
            {
                mensagemErro = "CPF inválido!";
                return false;
            }

            if(!ValidaIdadeFinalMaiorIdadeInicial(filtroBusca.IdadeInicial, filtroBusca.IdadeFinal))
            {
                mensagemErro = "Idade Final não pode ser maior que a idade inicial";
                return false;
            }

            return true;                    
        }

        public static bool IdadeValida(int idade)
        {
            if (!string.IsNullOrEmpty(idade.ToString()) && idade > 0)
                return true;

            return false;
        }
        
        public static bool CampoPreenchido(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
                return true;

            return false;
        }

       
    }
}
