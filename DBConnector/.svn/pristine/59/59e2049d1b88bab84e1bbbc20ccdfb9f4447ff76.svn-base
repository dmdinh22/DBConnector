using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector.Tools.Validator
{
    public sealed class RequiredFieldValidator
    {
        private static readonly RequiredFieldValidator _instance = new RequiredFieldValidator();

        static RequiredFieldValidator() { }
        private RequiredFieldValidator() { }
        public static RequiredFieldValidator Instance { get { return _instance; } }

        public void ValidateObject<T>(T obj) { }

        public bool HasError { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
