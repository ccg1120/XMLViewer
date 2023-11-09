﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLViewer_Core.Utiles
{
    public enum FileLoadReturnValue
    {
        Success,
        ExistsFile,
        NotSupportExtention,
        NullFilePath
    }

    /// <summary>
    /// 파일을 읽어서 값을 전달 하는 역할
    /// </summary>
    public class FileHandler
    {
        private static readonly string _extension = ".xml";

        public static FileLoadReturnValue LoadXMLFile(string filePath, string readOutput)
        {
            return LoadFile(filePath, _extension, readOutput);
        }

        private static FileLoadReturnValue LoadFile(string filePath, string extension, string readOutput)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return FileLoadReturnValue.NullFilePath;
            }

            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists == false)
            {
                return FileLoadReturnValue.ExistsFile;
            }
            if (fileInfo.Extension.Normalize().Equals(extension) == false)
            {
                return FileLoadReturnValue.NotSupportExtention;
            }
            readOutput = File.ReadAllText(filePath, Encoding.UTF8);
            return FileLoadReturnValue.Success;
        }
    }
}
