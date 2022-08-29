using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CadastroMotorista.Infra.Shared.ConfigData
{
    public abstract class BaseData
    {
        private readonly List<DbParameter> ListParameters;

        public BaseData()
        {
            ListParameters = new List<DbParameter>();
        }

        public static DataBase GetDataBase()
        {
            return DataBase.GetDataBase();
        }

        public DataBase GetConnection()
        {
            DataBase database;

            try
            {
                database = GetDataBase();

                return database;
            }
            catch(Exception ex)
            {
                _ = ex.Message;
            }

            return null;
        }

        public void CreateInputParameters(string name, DbType type, object value)
        {
            var sqlParameter = new SqlParameter
            {
                ParameterName = name,
                DbType = type,
                Value = value ?? DBNull.Value,
                Direction = ParameterDirection.Input
            };

            ListParameters.Add(sqlParameter);
        }

        public void Parameters(ExecutionParameter executionParameter)
        {
            try
            {
                foreach (var item in ListParameters)
                {
                    executionParameter.Add(item.ParameterName, item.Direction, item.DbType, item.Value);                  
                }

                ListParameters.Clear();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}