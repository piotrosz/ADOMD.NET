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
        public QueryImporter(string filename)
        {
            Filename = filename;
        }

        public string Filename { get; set; }

        public string FromFile(string queryKey)
        {
            string allText = File.ReadAllText(Filename);

            Regex r = new Regex(@"//<" + queryKey + @">([^@]*)[/|\-]{2}</" + queryKey + ">");
            Match match;
            match = r.Match(allText);

            return match.Success ? match.Groups[1].ToString() : "";
        }
    }
}
