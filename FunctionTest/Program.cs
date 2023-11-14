using FunctionTest;
using XMLViewer_Core.Utiles;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("TestDataExport Start");

        TestDataExporter. CreateTextXMLData("D:\\Project\\My", "D:\\Project\\Test.xml");
        Console.WriteLine("TestDataExport End");
    }
}