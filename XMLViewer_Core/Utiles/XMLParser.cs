using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLViewer_Core.Utiles
{
    public class XMLParser
    {
        public static void LoadXML(string filePath)
        {
            FileLoadReturnValue fileLoadReturnValue = FileHandler.isValidFilePathAndXMLExtension(filePath);
            if (fileLoadReturnValue != FileLoadReturnValue.Success)
            {
                //로거 하나 만들어야 할듯?
                return;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(filePath);


        }
        async Task TestReader(System.IO.Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine("Start Element {0}", reader.Name);
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("End Element {0}", reader.Name);
                            break;
                        default:
                            Console.WriteLine("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
        }
    }
}
