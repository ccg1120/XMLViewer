using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace XMLViewer_Core.Utiles
{
    public class XMLParser
    {
        public static FileLoadReturnValue LoadXML(string filePath, in XmlDocument xmlDocument)
        {
            FileLoadReturnValue fileLoadReturnValue = FileHandler.isValidFilePathAndXMLExtension(filePath);
            if (fileLoadReturnValue != FileLoadReturnValue.Success)
            {
                Logger.Log(LogLevel.Error, $"isValidFilePathAndXMLExtension ={fileLoadReturnValue}");
                return fileLoadReturnValue;
            }

            xmlDocument.Load(filePath);
            return fileLoadReturnValue;
        }
    }
}
