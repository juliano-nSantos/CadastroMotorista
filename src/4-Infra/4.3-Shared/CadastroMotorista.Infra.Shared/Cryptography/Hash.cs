using CadastroMotorista.Infra.Shared.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMotorista.Infra.Shared.Cryptography
{
    public class Hash
    {
        private readonly HashAlgorithm _Algorithm;
        private StringBuilder _sb;

        public Hash(HashAlgorithm Algorithm)
        {
            _Algorithm = Algorithm;
        }

        public string EncryptPassword(string pPassword)
        {
            _sb = new StringBuilder();

            var password = _Algorithm.ComputeHash(Encoding.UTF8.GetBytes(pPassword));

            foreach (var caracter in password)
            {
                _sb.Append(caracter.ToString("X2"));
            }

            return _sb.ToString();
        }

        public bool CheckPassword(string pPassword, string pRegisteredPassword)
        {
            _sb = new StringBuilder();

            if (!pRegisteredPassword.NaoVazioNulo())
            {
                throw new NullReferenceException("Cadastre uma senha!");
            }

            var password = _Algorithm.ComputeHash(Encoding.UTF8.GetBytes(pPassword));

            foreach (var caracter in password)
            {
                _sb.Append(caracter.ToString("X2"));
            }

            return _sb.ToString() == pRegisteredPassword;
        }
    }
}
