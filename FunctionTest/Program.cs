using FunctionTest;
using XMLViewer_Core.Utiles;

public class TestDataParameter
{
    public static readonly string TestXMLDataFilePath = "D:\\Project\\Test.xml";
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TestDataExport Start");
        FileInfo testDataFile = new FileInfo(TestDataParameter.TestXMLDataFilePath);
        if(testDataFile.Exists == false)
            TestDataExporter.CreateTextXMLData("D:\\Project\\My", TestDataParameter.TestXMLDataFilePath);
        Logger.Log_Dev("TESTDEV");
        Logger.Log(LogLevel.Info, "test");
        Console.WriteLine("TestDataExport End");
        XMLParser.LoadXML(TestDataParameter.TestXMLDataFilePath);
    }
}