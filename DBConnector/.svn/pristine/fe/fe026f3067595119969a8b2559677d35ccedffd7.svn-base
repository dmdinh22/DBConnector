using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DBConnector.Tools;
using DBConnector.Tools.AttributeHelper;

namespace DBConnector.Adapter
{

    public class DbAdapter : IDbAdapter
    {

        public DbAdapter(IDbCommand db_command, IDbConnection db_connection)
        {
            DbCommand = db_command;
            DbConnection = db_connection;
        }

        int _command_timeout = 5000;
        public int CommandTimeout { get { return _command_timeout; } set { _command_timeout = value; } }
        public IDbCommand DbCommand { get; private set; }
        public IDbConnection DbConnection { get; private set; }

        #region " Load Object "
        public IEnumerable<T> LoadObject<T>(IDbCommandDef db_command_def)
        {
            try
            {
                if (db_command_def == null)
                    throw new ArgumentException("Missing db command def object");

                List<T> itms = new List<T>();
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    try
                    {
                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                        cmd.CommandType = db_command_def.DbCommandType;
                        cmd.CommandText = db_command_def.DbCommandText;
                        cmd.CommandTimeout = CommandTimeout;
                        cmd.Connection = conn;
                        if (db_command_def.DbParameters != null)
                        {
                            foreach (IDbDataParameter param in db_command_def.DbParameters) { cmd.Parameters.Add(param); }
                        }
                        IDataReader dreader = cmd.ExecuteReader();
                        while (dreader.Read())
                        {
                            itms.Add(FillItem<T>(dreader));
                        }
                    }
                    catch { throw; }
                }

                return itms;
            }
            catch { throw; }
        }

        public IEnumerable<T> LoadObject<T>(IDbCommandDef db_command_def, Func<IDataReader, T> row_to_object)
        {
            try
            {
                if (db_command_def == null)
                    throw new ArgumentException("Missing db command def object");

                List<T> itms = new List<T>();
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    try
                    {
                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                        cmd.CommandType = db_command_def.DbCommandType;
                        cmd.CommandText = db_command_def.DbCommandText;
                        cmd.CommandTimeout = CommandTimeout;
                        cmd.Connection = conn;
                        if (db_command_def.DbParameters != null)
                        {
                            foreach (IDbDataParameter param in db_command_def.DbParameters) { cmd.Parameters.Add(param); }
                        }
                        IDataReader dreader = cmd.ExecuteReader();
                        while (dreader.Read())
                        {
                            itms.Add(row_to_object(dreader));
                        }
                    }
                    catch { throw; }
                }

                return itms;
            }
            catch { throw; }
        }

        public virtual IEnumerable<T> LoadObjectAsStreamResults<T>(IDbCommandDef db_command_def)
        {
            if (db_command_def == null)
                throw new ArgumentException("Missing db command def object");

            using (IDbConnection conn = DbConnection)
            using (IDbCommand cmd = DbCommand)
            {
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.CommandType = db_command_def.DbCommandType;
                cmd.CommandText = db_command_def.DbCommandText;
                cmd.CommandTimeout = CommandTimeout;
                cmd.Connection = conn;
                if (db_command_def.DbParameters != null)
                {
                    foreach (IDbDataParameter param in db_command_def.DbParameters) { cmd.Parameters.Add(param); }
                }

                IDataReader dreader = cmd.ExecuteReader();
                while (dreader.Read())
                {
                    yield return FillItem<T>(dreader);
                }
            }
        }
        #endregion

        public int ExecuteQuery(IDbCommandDef db_command_def)
        {
            try
            {
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandType = db_command_def.DbCommandType;
                    cmd.CommandText = db_command_def.DbCommandText;
                    cmd.CommandTimeout = CommandTimeout;
                    cmd.Connection = conn;
                    if (db_command_def.DbParameters != null)
                    {
                        foreach (IDbDataParameter parm in db_command_def.DbParameters)
                        {
                            if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Output) && parm.Value == null) { parm.Value = DBNull.Value; }
                            cmd.Parameters.Add(parm);
                        }
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch { throw; }
        }

        public object ExecuteDbScalar(IDbCommandDef db_command_def)
        {
            try
            {
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandType = db_command_def.DbCommandType;
                    cmd.CommandText = db_command_def.DbCommandText;
                    cmd.CommandTimeout = CommandTimeout;
                    cmd.Connection = conn;
                    if (db_command_def.DbParameters != null)
                    {
                        foreach (IDbDataParameter parm in db_command_def.DbParameters)
                        {
                            if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Output) && parm.Value == null) { parm.Value = DBNull.Value; }
                            cmd.Parameters.Add(parm);
                        }
                    }
                    return cmd.ExecuteScalar();
                }
            }
            catch { throw; }
        }

        #region " Generic DataReader Fill Routine for a defined object "
        protected T FillItem<T>(IDataReader row)
        {
            var colname = row.GetSchemaTable().Rows.Cast<DataRow>().Select(c => c["ColumnName"].ToString().ToLower()).ToList();
            var properties = typeof(T).GetProperties();

            var obj = Activator.CreateInstance<T>();
            foreach (var prop in properties)
            {
                bool has_field_map = Attribute.IsDefined(prop, typeof(DataFieldMapAttribute));
                if (has_field_map)
                {
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (Attribute attr in attrs)
                    {
                        if (attr is DataFieldMapAttribute)
                        {
                            DataFieldMapAttribute dfm = (DataFieldMapAttribute)attr;
                            if (colname.Contains(dfm.ColumnName.ToLower()))
                            {
                                Type typ = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                if (row[dfm.ColumnName] != DBNull.Value)
                                {
                                    if (row[dfm.ColumnName].GetType() == typeof(decimal)) { prop.SetValue(obj, (row.GetDouble(dfm.ColumnName)), null); }
                                    else { prop.SetValue(obj, (row.GetValue(row.GetOrdinal(dfm.ColumnName)) ?? null), null); }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (colname.Contains(prop.Name.ToLower()))
                    {
                        Type typ = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        if (row[prop.Name] != DBNull.Value)
                        {
                            if (row[prop.Name].GetType() == typeof(decimal)) { prop.SetValue(obj, (row.GetDouble(prop.Name)), null); }
                            else { prop.SetValue(obj, (row.GetValue(row.GetOrdinal(prop.Name)) ?? null), null); }
                        }
                    }
                }
            }
            return obj;
        }
        #endregion
    }

}