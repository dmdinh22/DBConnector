using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector.Tools
{
    public class GenericCompare<T> : IEqualityComparer<T>
    {
        private Func<T, object> _fndistinct;
        public GenericCompare(Func<T, object> fn_distinct) { _fndistinct = fn_distinct; }
        public bool Equals(T x, T y) { return _fndistinct(x).Equals(_fndistinct(y)); }
        public int GetHashCode(T obj) { return _fndistinct(obj).GetHashCode(); }

    }
}