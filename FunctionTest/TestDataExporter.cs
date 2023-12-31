﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace FunctionTest
{
    internal class TestDataExporter
    {
        //테스트 파일 제작 코드
        // 폴더와 파일을 기준으로 노드를 만듬
        public static bool CreateTextXMLData(in string path, string exportPath)
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(path);
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement parentXmlElement = xmlDocument.CreateElement("root");
            xmlDocument.AppendChild(parentXmlElement);
            Parser(xmlDocument, parentXmlElement, rootDirectory);

            xmlDocument.Save(exportPath);
            return true;
        }

        private static void Parser(XmlDocument xmlDoc, XmlElement parentXmlElement, DirectoryInfo rootFolder)
        {
            DirectoryInfo[] directoryInfos = rootFolder.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                string name = directoryInfo.Name;
                string createTime = directoryInfo.CreationTime.ToString();
                string path = directoryInfo.FullName;
                string fileCount = directoryInfo.GetFiles().Count().ToString();

                XmlElement folderElement = xmlDoc.CreateElement("folder");
                folderElement.SetAttribute(nameof(name), name);
                folderElement.SetAttribute(nameof(createTime), createTime);
                folderElement.SetAttribute(nameof(path), path);
                folderElement.SetAttribute(nameof(fileCount), fileCount);
                parentXmlElement.AppendChild(folderElement);
                
                Parser(xmlDoc, folderElement, directoryInfo);
            }
            FileInfo[] files = rootFolder.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                string name = fileInfo.Name;
                string createTime = fileInfo.CreationTime.ToString();
                string path = fileInfo.FullName;
                
                XmlElement fileElement = xmlDoc.CreateElement("file");
                fileElement.SetAttribute(nameof(name), name);
                fileElement.SetAttribute(nameof(createTime), createTime);
                fileElement.SetAttribute(nameof(path), path);
                parentXmlElement.AppendChild(fileElement);
            }
        }
        private static bool isValidStringToXMLName(string str)
        {
            //https://www.w3schools.com/xml/xml_elements.asp
            // +,* 등은 element 이름으로 쓰일 수 없음 그래서 포함되는 경우 제외 시킴
            string pattern = @"^[a-zA-Z_][a-zA-Z0-9_\-.]*$";

            // 정규표현식과 매치되는지 확인
            return Regex.IsMatch(str, pattern);

        }
    }
}
