using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Web.Script.Serialization;

namespace CSCore
{
    public class Serializer
    {
        public static string Serialize(object data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, data);

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }

        public static string Serialize(object data, Type t)
        {
            XmlSerializer serializer = new XmlSerializer(t);
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, data);

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }

        public static T Deserialize<T>(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data)));
        }

        public static string SerializeJS(object data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(data);
        }

        public static T DeserializeJS<T>(string data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return (T)serializer.Deserialize(data, typeof(T));
        }
    }
}
