using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector.Tools.AttributeHelper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredFieldAttribute : Attribute
    {
        private bool isrequired;
        public bool IsRequired { get { return isrequired; } }
        private string errmsg;
        public string ErrorMessage { get { return errmsg; } set { errmsg = value; } }

        public RequiredFieldAttribute(bool is_required)
        {
            this.isrequired = is_required;
        }
    }
}