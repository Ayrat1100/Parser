namespace CSV_Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = args[0];
            string fileContent = null;

            ProcG.OpenAndPreparation(path, ref fileContent);
            var reader = new CsvParser();
            var data = reader.Read(fileContent);

            object finalData;
            if (args.Length > 1)
            {
                try
                {
                    finalData = ProcG.NormalizeData(data).OrderBy(o => o[Convert.ToInt32(args[1]) - 1]);
                    ProcG.PrintData(finalData);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                finalData = ProcG.NormalizeData(data);
                ProcG.PrintData(finalData);
            }
        }
    }
}
