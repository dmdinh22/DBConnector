using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBConnector.Tools
{
    public static class ObjectClone
    {
        public static T SerializeClone<T>(this T object_to_clone)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("Object to clone must be serializable", "object_to_clone");

            if (object_to_clone == null)
                return default(T);

            IFormatter formatter = new BinaryFormatter();
            using (Stream strm = new MemoryStream())
            {
                formatter.Serialize(strm, object_to_clone);
                strm.Seek(0, SeekOrigin.Begin);
                return (T)Convert.ChangeType(formatter.Deserialize(strm), typeof(T));
            }
        }

        public static T JSSerializeClone<T>(this T object_to_clone)
        {
            if (object_to_clone == null)
                return default(T);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(jss.Serialize(object_to_clone));
        }
    }
}
