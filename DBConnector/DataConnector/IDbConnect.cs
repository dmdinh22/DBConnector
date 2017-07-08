using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnector.Adapter;
using DBConnector.Tools;

namespace DBConnector.DataConnector
{
    public interface IDbConnect
    {
        IEnumerable<T> LoadObjectFromDB<T>(IDbCommandDef db_command_def, Func<IDbCommandDef, IEnumerable<T>> object_loader);
        IEnumerable<T> LoadObjectFromDB<T>(IDbCommandDef db_command_def);
        int SaveDataFromObject<T>(IDbCommandDef db_command_def, Func<IDbCommandDef, int> object_saver);
        int SaveDataFromObject<T>(IDbCommandDef db_command_def);
        void DeleteDataFromDB(IDbCommandDef db_command_def);
        int NonQueryExecute(IDbCommandDef db_command_def);
    }
}