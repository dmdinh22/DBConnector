using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnector.Adapter;
using DBConnector.Tools;

namespace DBConnector.DataConnector
{
    public class DbConnect : IDbConnect
    {
        public IDbAdapter DbAdapter { get; private set; }

        public DbConnect(IDbAdapter db_adapter)
        {
            DbAdapter = db_adapter;
        }

        public IEnumerable<T> LoadObjectFromDB<T>(IDbCommandDef db_command_def, Func<IDbCommandDef, IEnumerable<T>> object_loader)
        {
            return object_loader(db_command_def);
        }

        public IEnumerable<T> LoadObjectFromDB<T>(IDbCommandDef db_command_def)
        {
            return DbAdapter.LoadObject<T>(db_command_def);
        }

        public int SaveDataFromObject<T>(IDbCommandDef db_command_def, Func<IDbCommandDef, int> object_saver)
        {
            return object_saver(db_command_def);
        }

        public int SaveDataFromObject<T>(IDbCommandDef db_command_def)
        {
            return DbAdapter.ExecuteDbScalar(db_command_def).ToInt32();
        }

        public void DeleteDataFromDB(IDbCommandDef db_command_def)
        {
            DbAdapter.ExecuteQuery(db_command_def);
        }

        public int NonQueryExecute(IDbCommandDef db_command_def)
        {
            return DbAdapter.ExecuteQuery(db_command_def);
        }
    }
}