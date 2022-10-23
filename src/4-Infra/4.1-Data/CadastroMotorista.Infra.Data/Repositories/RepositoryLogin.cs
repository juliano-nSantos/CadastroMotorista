using CadastroMotorista.Domain.Entities;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Infra.Shared.ConfigData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMotorista.Infra.Data.Repositories
{
    public class RepositoryLogin : BaseData, IRepositoryLogin
    {
        public async Task<Usuario> FindByEmail(string email)
        {
            var user = new Usuario();
            var _database = GetConnection();

            try
            {
                CreateInputParameters("EMAIL", DbType.String, email);

                using(DbDataReader reader = await _database.ExecuteReaderAsync(CommandType.StoredProcedure, "PRC_GET_USUARIO", Parameters))
                {
                    if(reader != null)
                    {
                        if (reader.Read())
                        {
                            user = RetornaModel(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar o usuario! " + ex.Message);
            }
            finally
            {
                if (_database != null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return user;
        }

        private Usuario RetornaModel(DbDataReader reader)
        {
            var usuario = new Usuario();

            var colunas = reader.GetColumnSchema();

            if (colunas.Any(c => c.ColumnName.Equals("IdUsuario")) && !string.IsNullOrEmpty(reader["IdUsuario"].ToString()))
                usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
            if (colunas.Any(c => c.ColumnName.Equals("Nome")) && !string.IsNullOrEmpty(reader["Nome"].ToString()))
                usuario.Nome = reader["Nome"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("CPF")) && !string.IsNullOrEmpty(reader["CPF"].ToString()))
                usuario.CPF = reader["CPF"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Senha")) && !string.IsNullOrEmpty(reader["Senha"].ToString()))
                usuario.Senha = reader["Senha"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Permissao")) && !string.IsNullOrEmpty(reader["Permissao"].ToString()))
                usuario.Permissao = reader["Permissao"].ToString();
            usuario.Exists = true;

            return usuario;
        }
    }
}
