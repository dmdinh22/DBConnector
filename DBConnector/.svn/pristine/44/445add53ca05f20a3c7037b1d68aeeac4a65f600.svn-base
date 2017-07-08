using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DBConnector.Tools.AttributeHelper
{

    [AttributeUsage(AttributeTargets.Property)]
    public class DataFieldMapAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public int MaxLength { get; set; }
        public bool IsNull { get; set; }
        public DbType DataType { get; set; }
        public bool IsIdentity { get; set; }

        public DataFieldMapAttribute(string column_name)
        {
            ColumnName = column_name;
        }

        public DataFieldMapAttribute(string column_name, int max_length, bool is_null = true)
        {
            ColumnName = column_name;
            MaxLength = max_length;
            IsNull = is_null;
        }

        public DataFieldMapAttribute(string column_name, int max_length, bool is_null = true, DbType db_type = DbType.String)
        {
            ColumnName = column_name;
            MaxLength = max_length;
            IsNull = is_null;
            DataType = db_type;
        }

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DataTableMapAttribute : Attribute
    {
        public string TableName { get; set; }
        public DataTableMapAttribute(string table_name) { TableName = table_name; }
    }
}
