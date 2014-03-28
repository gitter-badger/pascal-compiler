using System;
using System.Collections.Generic;
using System.IO;

namespace CompilerCore
{
    public class CompilerFrontend
    {
        private IScanner Scanner { get; set; }

        private ISymbolTable SymbolTable { get; set; }

        public CompilerFrontend(string path)
        {
            Scanner = new ScannerImpl(path);
            SymbolTable = new SymbolTableTreeImpl();
        }

        public void Go(string outputPath = null)
        {
            var outputLines = new List<string>();

            while (Scanner.HasNextToken())
            {
                var token = Scanner.GetNextToken();
                var symbol = SymbolTable.ContainsString(token)
                    ? SymbolTable.GetSymbolFor(token)
                    : SymbolTable.InstallSymbol(token);
                var output = Utils.TokenOutputFormat(token, symbol.CurrentAttribute.TokenType);
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
