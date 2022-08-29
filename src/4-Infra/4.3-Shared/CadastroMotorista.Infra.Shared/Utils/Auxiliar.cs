namespace CadastroMotorista.Infra.Shared.Utils
{
    public static class Auxiliar
    {
        public static string RemovePontosTracosString(string valor)
        {
            string retorno = string.Empty;

            if (!string.IsNullOrEmpty(valor))
                retorno = valor.Replace(".", "").Replace("-", "");

            return retorno;
        }
    }
}
