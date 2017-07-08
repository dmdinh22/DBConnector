using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DBConnector.Tools
{
    public class JsonConvert
    {
        public static T ToObject<T>(string json_text)
        {
            if (string.IsNullOrEmpty(json_text))
                throw new ArgumentException("Missing Json Text");

            return new JavaScriptSerializer().Deserialize<T>(json_text);
        }

        public static string ToJson<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentException("Object is null");

            return new JavaScriptSerializer().Serialize(obj);
        }

        public static string JsonToClass(string json_text, string name_space, string class_name)
        {
            string class_template = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace [namespace]
{
    public class [classname]
    {
        [properties]
    }
    /* JSON Sample
     [jsonsample]
     */
}";

            string rgx_obj = @"\{(.*?)\}";
            MatchCollection obj_matches = Regex.Matches(json_text, rgx_obj);
            StringBuilder sb_properties = new StringBuilder();
            if (obj_matches != null && obj_matches.Count > 0)
            {
                string first_obj = obj_matches[0].Value;
                if (!string.IsNullOrWhiteSpace(first_obj))
                {
                    first_obj = first_obj.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("'", "");
                    string[] obj_properties = first_obj.Split(",".ToCharArray());
                    foreach (string obj_prop in obj_properties)
                    {
                        string[] indiv_props = obj_prop.Split(":".ToCharArray());
                        sb_properties.Append("public string " + indiv_props[0] + " { get; set; }\r");
                    }
                }
            }
            else { return null; }
            string str_props = sb_properties.ToString();
            return class_template.Replace("[namespace]", name_space).Replace("[classname]", class_name).Replace("[properties]", str_props).Replace("[jsonsample]", json_text);
        }

    }
}