using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

// Processes ISO 3166-1 alpha-2 country list from wiki
// https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
// If Wiki changes table format this whole thing will break
// All were warned
// ---
// How to use:
// Go to wiki page
// Copy the big table into a text file
// Use said text file as argument for the application

namespace Edgar.ISO3166_1Alpha_2CountryCodesToEnum
{
    public class Program
    {
        public static string EnumModifiers = "public";
        public static string EnumName = "CountryCode";
        public static string EnumNamespace = "App.Domain";
        public static bool IncludeComments = true;
        public static bool DoubleNewLine = true;
        public static string TabSymbol = "    ";
        public static bool CountryCodeCamelCase = true;

        private static void Main(string[] args)
        {
            var filePath = args.Length > 0 ? args[0] : "codes.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Please provide a valid text file");
                return;
            }

            var countries = File
                .ReadAllLines(filePath)
                .Where(IsLineValid)
                .Select(ProcessLine)
                .ToList();

            var enumCode = BuildEnumCode(countries);
            File.WriteAllText($"{EnumName}.cs", enumCode);
        }

        private static bool IsLineValid(string line)
        {
            // a line must start with two letter country code 
            // and be followed by a tab character
            // some lines do not fit this criteria due to wiki copy/paste breaking the list
            // if a line is invalid we don't want to process it
            // due to breakage
            return line.Length >= 3 
                && char.IsLetter(line[0]) 
                && char.IsLetter(line[1]) 
                && line[2] == '\t';
        }

        private static (string, string) ProcessLine(string line)
        {
            var split = line.Split('\t');
            var code = split[0];
            var name = split[1];
            return (code, name);
        }

        private static string BuildEnumCode(List<(string code, string name)> countries)
        {
            var sb = new StringBuilder();
            var tab = TabSymbol;

            sb.AppendLine($"namespace {EnumNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"{tab}{(!string.IsNullOrEmpty(EnumModifiers) ? $"{EnumModifiers} " : "")}enum {EnumName}");
            sb.AppendLine($"{tab}{{");
            foreach (var country in countries)
            {
                if (IncludeComments)
                {
                    sb.AppendLine($"{tab}{tab}/// <summary>");
                    sb.AppendLine($"{tab}{tab}/// {country.name}");
                    sb.AppendLine($"{tab}{tab}/// </summary>");
                }
                var code = country.code;
                if (CountryCodeCamelCase)
                    code = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code.ToLower());
                sb.AppendLine($"{tab}{tab}{code},");
                if(DoubleNewLine)
                    sb.AppendLine();
            }
            sb.AppendLine($"{tab}}}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
