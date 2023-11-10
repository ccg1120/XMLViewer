using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FunctionTest
{
    internal class TestDataExporter
    {
        //테스트 파일 제작 코드
        // 폴더와 파일을 기준으로 노드를 만듬
        public bool CreateTextXMLData(in string path, string exportPath)
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(path);
            XmlDocument xmlDocument = new XmlDocument();
            Parser(xmlDocument, rootDirectory);
            return true;
        }

        private void Parser(XmlDocument xml, DirectoryInfo rootFolder)
        {
            DirectoryInfo[] directoryInfos = rootFolder.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                string name = directoryInfo.Name;
                string createTime = directoryInfo.CreationTime.ToString();
                string path = directoryInfo.FullName;
                string fileCount = directoryInfo.GetFiles().Count().ToString();
            }
            FileInfo[] files = rootFolder.GetFiles();
        }
    }
}
