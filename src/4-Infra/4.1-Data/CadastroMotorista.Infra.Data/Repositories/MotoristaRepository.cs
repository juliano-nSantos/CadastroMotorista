using CadastroMotorista.Domain.Entities;
using CadastroMotorista.Domain.Interfaces.Repositories;
using CadastroMotorista.Domain.Models;
using CadastroMotorista.Infra.Shared.ConfigData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroMotorista.Infra.Data.Repositories
{
    public class MotoristaRepository : BaseData, IRepository<Motorista>
    {
        public async Task<bool> AdicionarAsync(Motorista entity)
        {
            bool retorno = false;
            var _database = GetConnection();

            try
            {
                CreateInputParameters("CPF", DbType.String, entity.CPF);
                CreateInputParameters("NOME", DbType.String, entity.Nome);
                CreateInputParameters("SEXO", DbType.String, entity.Sexo);
                CreateInputParameters("IDADE", DbType.Int16, entity.Idade);
                CreateInputParameters("ATIVO", DbType.Boolean, entity.Ativo);

                _database.BeginTran();
                var result = await _database.ExecuteNoQueryAsync(CommandType.StoredProcedure, "PRC_REG_MOTORISTA", Parameters);

                if(result > 0)
                {
                    _database.CommitTran();
                    retorno = true;
                }
                else
                {
                    _database.RollbackTran();
                }
            }
            catch(SqlException sex)
            {
                _database.RollbackTran();
                if(sex.Number == 70000)
                {
                    throw new Exception(sex.Message);
                }
            }
            catch(Exception ex)
            {
                _database.RollbackTran();
                throw new Exception("Erro ao cadastrar o motorista! " + ex.Message);
            }
            finally
            {
                if (_database != null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return retorno;
        }

        public async Task<bool> AtualizarAsync(Motorista entity)
        {
            bool retorno = false;
            var _database = GetConnection();

            try
            {
                CreateInputParameters("CODIGO", DbType.Int32, entity.Codigo);
                CreateInputParameters("NOME", DbType.String, entity.Nome);
                CreateInputParameters("IDADE", DbType.Int16, entity.Idade);
                CreateInputParameters("ATIVO", DbType.Boolean, entity.Ativo);

                _database.BeginTran();
                var result = await _database.ExecuteNoQueryAsync(CommandType.StoredProcedure, "PRC_UP_MOTORISTA", Parameters);

                if (result > 0)
                {
                    _database.CommitTran();
                    retorno = true;
                }
                else
                {
                    _database.RollbackTran();
                }
            }
            catch (Exception ex)
            {
                _database.RollbackTran();
                throw new Exception("Erro ao atualizar o motorista! " + ex.Message);
            }
            finally
            {
                if(_database != null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return retorno;
        }

        public async Task<Motorista> BuscarPorCodigoAsync(int id)
        {
            var motorista = new Motorista();
            var _database = GetConnection();

            try
            {
                CreateInputParameters("CODIGO", DbType.Int32, id);

                using(DbDataReader reader = await _database.ExecuteReaderAsync(CommandType.StoredProcedure, "PRC_GET_MOTORISTA", Parameters))
                {
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            motorista = RetornaModel(reader);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao consultar o motorista! " + ex.Message);
            }
            finally
            {
                if(_database != null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return motorista;
        }

        public async Task<List<Motorista>> BuscarPorFiltroAsync(FiltroBusca filtroBusca)
        {
            var lstMotoristas = new List<Motorista>();
            var _database = GetConnection();

            try
            {
                if (!string.IsNullOrEmpty(filtroBusca.Nome))
                    CreateInputParameters("NOME", DbType.String, filtroBusca.Nome);
                if (!string.IsNullOrEmpty(filtroBusca.CPF))
                    CreateInputParameters("CPF", DbType.String, filtroBusca.CPF);
                if (!string.IsNullOrEmpty(filtroBusca.Sexo))
                    CreateInputParameters("SEXO", DbType.String, filtroBusca.Sexo);
                if (filtroBusca.IdadeInicial > 0)
                    CreateInputParameters("IDADE_INICIAL", DbType.Int16, filtroBusca.IdadeInicial);
                if (filtroBusca.IdadeFinal > 0)
                    CreateInputParameters("IDADE_FINAL", DbType.Int16, filtroBusca.IdadeFinal);
                if (filtroBusca.Ativo != null)
                    CreateInputParameters("ATIVO", DbType.Boolean, filtroBusca.Ativo);

                using(DbDataReader reader = await _database.ExecuteReaderAsync(CommandType.StoredProcedure, "PRC_GET_FILTRO_MOTORISTAS", Parameters))
                {
                    if(reader != null)
                    {
                        while (reader.Read())
                        {
                            lstMotoristas.Add(RetornaModel(reader));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao Consultar motoritas! " + ex.Message);
            }
            finally
            {
                if(_database != null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return lstMotoristas;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            bool retorno = false;
            var _database = GetConnection();

            try
            {
                CreateInputParameters("CODIGO", DbType.Int32, id);

                _database.BeginTran();
                var result = await _database.ExecuteNoQueryAsync(CommandType.StoredProcedure, "PRC_DEL_MOTORISTA", Parameters);

                if (result > 0)
                {
                    _database.CommitTran();
                    retorno = true;
                }
                else
                {
                    _database.RollbackTran();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir o motorista! " + ex.Message);
            }
            finally
            {
                if (_database != null)
                {
                    _database.Dispose();
                    GC.SuppressFinalize(_database);
                }
            }

            return retorno;
        }

        private Motorista RetornaModel(DbDataReader reader)
        {
            var motorista = new Motorista();
            var colunas = reader.GetColumnSchema();

            if (colunas.Any(c => c.ColumnName.Equals("Codigo")) && !string.IsNullOrEmpty(reader["Codigo"].ToString()))
                motorista.Codigo = Convert.ToInt32(reader["Codigo"]);
            if (colunas.Any(c => c.ColumnName.Equals("CPF")) && !string.IsNullOrEmpty(reader["CPF"].ToString()))
                motorista.CPF = reader["CPF"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Nome")) && !string.IsNullOrEmpty(reader["Nome"].ToString()))
                motorista.Nome = reader["Nome"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Sexo")) && !string.IsNullOrEmpty(reader["Sexo"].ToString()))
                motorista.Sexo = reader["Sexo"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Idade")) && !string.IsNullOrEmpty(reader["Idade"].ToString()))
                motorista.Idade = Convert.ToInt16(reader["Idade"]);
            if (colunas.Any(c => c.ColumnName.Equals("Ativo")) && !string.IsNullOrEmpty(reader["Ativo"].ToString()))
                motorista.Ativo = Convert.ToBoolean(reader["Ativo"]);

            return motorista;
        }
    }
}
