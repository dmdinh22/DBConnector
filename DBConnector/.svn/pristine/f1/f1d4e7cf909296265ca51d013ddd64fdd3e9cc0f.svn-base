using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace DBConnector.Tools
{
    public class XMLParseHelper
    {
        public static T XMLToObject<T>(string xml_string)
        {
            XmlSerializer xser = new XmlSerializer(typeof(T));
            using (System.IO.TextReader tr2 = new System.IO.StringReader(xml_string))
            {
                return (T)xser.Deserialize(tr2);
            }
        }

        public static List<T> XMLToObjectList<T>(string xml_string, string node_path)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml_string);
            var returnItemsList = new List<T>();
            foreach (XmlNode xmlNode in xmlDocument.SelectNodes(node_path))
            {
                returnItemsList.Add(XMLToObject<T>(xmlNode.OuterXml));
            }

            return returnItemsList;
        }

        public static string ObjectToXML<T>(T obj_to_serialize)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, obj_to_serialize);
                return sw.ToString();
            }
        }
    }
}