using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace AdomdUtilities
{
    public class QueryImporter
    {
        private string fileContents;

        public QueryImporter(string filename)
        {
            this.fileContents = File.ReadAllText(filename);
        }

        public string FromFile(string queryKey)
        {
            Regex regex = new Regex(@"[/|\-]{2}<" + queryKey + @">([^@]*)[/|\-]{2}</" + queryKey + @">");
            Match match = regex.Match(fileContents);

            return match.Success ? match.Groups[1].ToString() : "";
        }
    }
}
