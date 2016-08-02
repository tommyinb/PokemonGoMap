using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PokemonGoMap.Utility
{
    public static class XmlUtil
    {
        public static string Serialize<T>(T @object)
        {
            var serializer = new XmlSerializer(typeof(T));

            var stringWriter = new StringWriter();
            var xmlSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                CloseOutput = true
            };
            using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
            {
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add(string.Empty, string.Empty);

                serializer.Serialize(xmlWriter, @object, xmlNamespaces);

                return stringWriter.ToString();
            }
        }
        public static T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(xml))
            {
                var @object = serializer.Deserialize(reader);

                if (@object == null) return default(T);

                return (T)@object;
            }
        }

        public static void WriteFile<T>(T @object, string filePath)
        {
            var xml = Serialize(@object);

            File.WriteAllText(filePath, xml);
        }
        public static T ReadFile<T>(string filePath)
        {
            var xml = File.ReadAllText(filePath);

            return Deserialize<T>(xml);
        }
    }
}
