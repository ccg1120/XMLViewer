using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLViewer_Core.Utiles
{
    enum XMLFileLoadReturnValue
    {
        Success,
        ExistsFile,
        NoXMLExtention,
        NullFilePath
    }

    /// <summary>
    /// 파일을 읽어서 값을 전달 하는 역할
    /// </summary>
    internal class FileHandler
    {
        static XMLFileLoadReturnValue LoadXMLFile(ref string filePath, ref string readOutput)
        {
            if ( string.IsNullOrEmpty(filePath))
            {
                return XMLFileLoadReturnValue.NullFilePath;
            }

            FileInfo fileInfo = new FileInfo(filePath);
            if(fileInfo.Exists == false)
            {
                return XMLFileLoadReturnValue.ExistsFile;
            }
            if (fileInfo.Extension.Normalize().Equals(".xml") == false)
            {
                return XMLFileLoadReturnValue.NoXMLExtention;
            }

            readOutput = File.ReadAllText(filePath, Encoding.UTF8);
            return XMLFileLoadReturnValue.Success;
        }
    }
}
