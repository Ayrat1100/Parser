namespace CSV_Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class ProcG
    {
        public static void PrintData(object dataObj)
        {
            var data = dataObj as IEnumerable<List<string>>;
            foreach (var item in data)
            {
                foreach (var cell in item)
                {
                    Console.Write("{0,-27} |", cell);
                }
                Console.WriteLine();
            }
        }

        public static IEnumerable<List<string>> NormalizeData(object dataArgs)
        {
            IEnumerable<List<string>> data = dataArgs as IEnumerable<List<string>>;
            int maxCount = data.Max(u => u.Count);
            foreach (var item in data)
            {
                if (item.Count < maxCount)
                {
                    var iter = maxCount - item.Count();
                    for (int i = 0; i < iter; i++)
                    {
                        item.Add("NULL");
                    }
                }

                yield return item;
            }
        }

        /// <summary>
        /// Open, read and preparation file for processing
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="fileContent">File contents</param>
        public static void OpenAndPreparation(string path, ref string fileContent)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                Console.WriteLine("File is not exist!");
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    while (!streamReader.EndOfStream)
                    {
                        Regex regex = new Regex(@"(?<=([,]))(\s?)+");
                        fileContent = regex.Replace(streamReader.ReadToEnd(), String.Empty);
                    }
                }
            }
        }
    }
}