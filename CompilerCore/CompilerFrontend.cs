using System;
using System.Collections.Generic;
using System.IO;

namespace CompilerCore
{
    public class CompilerFrontend
    {
        private Scanner Scanner { get; set; }

        private SymbolTable SymbolTable { get; set; }

        public CompilerFrontend(string path)
        {
            Scanner = new Scanner(path);
            SymbolTable = new SymbolTable();
        }

        public void Go(string outputPath = null)
        {
            var outputLines = new List<string>();

            while (!Scanner.HasNextToken())
            {
                var token = Scanner.GetNextToken();
                var tt = SymbolTable.GetTokenTypeFor(token);
                var output = Utils.TokenOutputFormat(token, tt);
                outputLines.Add(output);
            }

            if (outputPath != null)
            {
                File.WriteAllLines(outputPath, outputLines);
            }
            else
            {
                foreach (var line in outputLines)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
