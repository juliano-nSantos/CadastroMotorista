using System.Data.Common;

namespace CadastroMotorista.Infra.Shared.ConfigData
{
    public class ExecutionResultReader
    {
        public DbParameter[] Parameters { get; protected set; }
        public DbDataReader Reader { get; protected set; }

        internal ExecutionResultReader(DbDataReader reader)
            : this(reader, null)
        {}

        internal ExecutionResultReader(DbDataReader reader, params DbParameter[] parameters)
        {
            Reader = reader;
            Parameters = parameters;
        }
    }
}