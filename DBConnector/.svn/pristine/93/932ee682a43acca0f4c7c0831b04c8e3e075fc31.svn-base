using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DBConnector.Adapter
{

    public interface IDbAdapter
    {
        IEnumerable<T> LoadObject<T>(IDbCommandDef db_command_def);
        IEnumerable<T> LoadObjectAsStreamResults<T>(IDbCommandDef db_command_def);
        IEnumerable<T> LoadObject<T>(IDbCommandDef db_command_def, Func<IDataReader, T> row_to_object);
        int ExecuteQuery(IDbCommandDef db_command_def);
        object ExecuteDbScalar(IDbCommandDef db_command_def);
        //IDbDataParameter BuildParameter(IDbParameter db_parameter);
    }

    public interface IDbCommandDef
    {
        string DbCommandText { get; set; }
        CommandType DbCommandType { get; set; }
        IDbDataParameter[] DbParameters { get; set; }
    }

}