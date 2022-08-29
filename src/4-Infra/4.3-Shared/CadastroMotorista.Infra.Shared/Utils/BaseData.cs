using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CadastroMotorista.Infra.Shared.Utils
{
    public abstract class BaseData : IDisposable
    {
        private bool _disposed = false;
        ~BaseData() => Dispose(false);        
        /// <summary>
        /// Método para montar o command, recebendo por parametro o nome da procedure e a lista de parametros
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlCommand MakeCommand(string name, List<SqlParameter> parameters)
        {
            SqlCommand cmd = new SqlCommand(name);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters.ToArray());

            return cmd;
        }

        /// <summary>
        /// Método para executar uma lista de commands usando a mesma transaction, Recebe por parametro uma lista de commands,
        /// e a connectionString
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool ExecuteQueryTransaction(List<SqlCommand> commands, SqlConnection conn)
        {
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                foreach(var cmd in commands)
                {
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

                transaction.Commit();

                return true;
            }
            catch (SqlException ex)
            {
                //Log.Error(Assistant.FormatError("ExecuteQueryTransaction", ex.Message, ex.StackTrace, ex.InnerException == null ? null : ex.InnerException.ToString()));

                if(transaction != null)
                {
                    transaction.Rollback();
                }

                throw ex;
            }
            catch (Exception ex)
            {
                //Log.Error(Assistant.FormatError("ExecuteQueryTransaction", ex.Message, ex.StackTrace, ex.InnerException == null ? null : ex.InnerException.ToString()));

                if(transaction != null)
                {
                    transaction.Rollback();
                }

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Método para adcionar uma variavel a lista de parametros, recebe o nome e o valor da variavel.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlParameter AddParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        /// <summary>
        /// Método para adcionar uma variavel a lista de parametros, recebe o nome, tipo e valor da variavel.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlParameter AddParameter(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type) { Value = value };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(_disposed)
                return;
            
            _disposed = true;
        }

    }
}