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
    public class RepositoryUsuario : BaseData, IRepositoryUsuario
    {
        public async Task<Usuario> GetUserById(int id)
        {
            var user = new Usuario();
            var _database = GetConnection();

            try
            {
                CreateInputParameters("IdUsuario", DbType.Int32, id);

                using(DbDataReader reader = await _database.ExecuteReaderAsync(CommandType.StoredProcedure, "PRC_GET_USUARIO_POR_CODIGO", Parameters))
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
                throw new Exception("Erro ao consultar o usuário por código! " + ex.Message);
            }
            finally
            {
                if(_database!= null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return user;
        }

        private Usuario RetornaModel(DbDataReader reader)
        {
            var user = new Usuario();

            var colunas = reader.GetColumnSchema();

            if (colunas.Any(c => c.ColumnName.Equals("IdUsuario")) && !string.IsNullOrEmpty(reader["IdUsuario"].ToString()))
                user.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
            if (colunas.Any(c => c.ColumnName.Equals("Nome")) && !string.IsNullOrEmpty(reader["Nome"].ToString()))
                user.Nome = reader["Nome"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("CPF")) && !string.IsNullOrEmpty(reader["CPF"].ToString()))
                user.CPF = reader["CPF"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Email")) && !string.IsNullOrEmpty(reader["Email"].ToString()))
                user.Email = reader["Email"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataCadastro")) && !string.IsNullOrEmpty(reader["DataCadastro"].ToString()))
                user.DataCadastro = Convert.ToDateTime(reader["DataCadastro"]);
            if (colunas.Any(c => c.ColumnName.Equals("PermissaoId")) && !string.IsNullOrEmpty(reader["PermissaoId"].ToString()))
                user.PermissaoId = Convert.ToInt32(reader["PermissaoId"]);
            if (colunas.Any(c => c.ColumnName.Equals("Permissao")) && !string.IsNullOrEmpty(reader["Permissao"].ToString()))
                user.Permissao = reader["Permissao"].ToString();
            user.Exists = true;

            return user;
        }
    }
}
