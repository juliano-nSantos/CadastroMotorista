using System.Linq;

namespace CadastroMotorista.Infra.Shared.Extension
{
    public static class Extensions
    {
        public static string RemovePontosTracosBarras(this string value)
        {
            var newValue = string.Empty;

            if (!string.IsNullOrEmpty(value))
                newValue = value.Replace(".", "").Replace("/", "").Replace("-", "");

            return newValue;
        }

        public static bool NaoVazioNulo(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return true;

            return false;
        }

        public static bool TodosCaracteresIguais(this string valor)
        {
            char compare = valor[0];

            return valor.All(v => v.Equals(compare));
        }

        public static bool CpfValido(this string cpf)
        {
            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;

            cpf = cpf.Trim();
            cpf = cpf.RemovePontosTracosBarras();

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * mult1[i];
            }

            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();

            tempCpf = tempCpf + digit;

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * mult2[i];
            }

            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return cpf.EndsWith(digit);
        }
    }
}
