namespace CSV_Parser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class CsvParser
    {
        /// <summary>
        /// Returned list of lists with data
        /// </summary>
        /// <param name="contentOfCsv">Content of csv file</param>
        /// <returns></returns>
        public IEnumerable<List<string>> Read(string content)
        {
            using (StringReader reader = new StringReader(content))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    yield return ParseLine(line);
                }
            }
        }
        /// <summary>
        /// Return cells list
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns></returns>
        private List<string> ParseLine(string line)
        {
            List<string> result = new List<string>();
            int i = 0;
            while (true)
            {
                string cell = ParseNextCell(line, ref i);
                if (cell == null)
                {
                    break;
                }

                result.Add(cell);
            }

            return result;
        }

        /// <summary>
        /// Returns the finished cell
        /// </summary>
        /// <param name="line">Current string line</param>
        /// <param name="i">Current index</param>
        /// <returns></returns>
        private string ParseNextCell(string line, ref int i)
        {
            if (i >= line.Length)
            {
                return null;
            }

            if (line[i] != Symbols.QMARK )
            {
                return this.ParseNotEscapedCell(line, ref i);
            }
            else
            {
                return this.ParseEscapedCell(line, ref i);
            }
        }

        /// <summary>
        /// Parse if cell is not escaped
        /// </summary>
        /// <param name="line">Current string line</param>
        /// <param name="i">Current index</param>
        /// <returns></returns>
        private string ParseNotEscapedCell(string line, ref int i)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (i >= line.Length)
                {
                    break;
                }

                if (line[i] == Symbols.LINEDELIMITER)
                {
                    i++;
                    break;
                }
                else
                {
                    if (line[i] != Symbols.QMARK)
                    {
                        sb.Append(line[i]);
                    }

                    i++;
                }
            }

            return this.CheckingNulbleCell(sb.ToString());
        }

        /// <summary>
        /// If current cell is empty, write null
        /// </summary>
        /// <param name="str">Current cell string</param>
        /// <returns></returns>
        private string CheckingNulbleCell(string str)
        {
            if (str == string.Empty) { str = "Null"; }
            return str;
        }

        /// <summary>
        /// Parse if cell is escaped
        /// </summary>
        /// <param name="line">Current string line</param>
        /// <param name="i">Current string index</param>
        /// <returns></returns>
        private string ParseEscapedCell(string line, ref int i)
        {
            i++; 
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (i >= line.Length)
                {
                    break;
                }

                if (line[i] == Symbols.QMARK)
                {
                    i++;
                    if (i >= line.Length)
                    {
                        break;
                    }

                    if (line[i] == Symbols.LINEDELIMITER)
                    {
                        i++;
                        break;
                    }

                    if (line[i] == Symbols.QMARK){}

                }
                else
                {
                    if (line[i] != Symbols.QMARK)
                    {
                        sb.Append(line[i]);
                    }

                    i++;
                }
            }

            return sb.ToString();
        }
    }
}
