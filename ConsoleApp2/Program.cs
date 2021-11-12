using System;
using System.Text;
using System.Data;
using System.IO;
using Newtonsoft.Json;
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу JSON");            
            JSONtoCSV.JSONdeserializing(Console.ReadLine());
            Console.WriteLine("Введите путь к файлу CSV и разделитель");
            JSONtoCSV.SavetoCSV(Console.ReadLine(),Console.ReadLine());
            Console.WriteLine("Программа успешно завершила работу, нажмите любую клавишу для выхода");
            Console.ReadKey();
        }
        public static void InputHandler (string InputString)
        {
            switch (InputString)
            {
                case  "-q":
                    Environment.Exit(0);
                    break;
            }
         }
    }
   abstract class JSONtoCSV
    {
        public static DataTable JSONData = new DataTable();
        public static void JSONdeserializing(string JSONpath)
        {            
            {
                Program.InputHandler(JSONpath);
                string jsonString = File.ReadAllText(JSONpath);
                JSONData = (DataTable)JsonConvert.DeserializeObject(jsonString, (typeof(DataTable)));                
            }            
        }
        public static void SavetoCSV(string CSVPath,string separator = ",")
        {
            Program.InputHandler(CSVPath);
            Program.InputHandler(separator);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataColumn col in JSONData.Columns)
            {
                stringBuilder.Append(col.ColumnName + separator);
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(Environment.NewLine);
            foreach (DataRow row in JSONData.Rows)
            {
                for (int i = 0; i < JSONData.Columns.Count; i++)
                {
                    stringBuilder.Append(row[i].ToString() + separator);
                }

                stringBuilder.Append(Environment.NewLine);
            }
            File.WriteAllText(CSVPath, stringBuilder.ToString());
        }
    }
}
