using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompilerCore;

namespace CompilerDriver
{
    static class Program
    {
        public static void Main(String[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
            }
            else
            {
                var outputPath = args.Skip(1).FirstOrDefault();
                PrintTokens(args[0], outputPath);
            }

            Pause();
        }

        private static void PrintTokens(string path, string outputPath)
        {
            var sc = new Scanner(path);
            var tokens = new List<IToken>();

            while (!sc.HasNextToken())
            {
                tokens.Add(sc.GetNextToken());
            }

            var lines = tokens.Select(TokenFormat);

            if (outputPath != null)
            {
                File.WriteAllLines(outputPath, lines);
            }
            else
            {
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }
        }

        private static string TokenFormat(IToken token)
        {
            var cStyleType = "tok" + token.Type.ToString().ToLower();

            return string.Format("{0}\t\t{1}", token.Lexeme, cStyleType);
        }

        private static void PrintUsage()
        {
            var exePath = Environment.GetCommandLineArgs()[0];
            var exeName = Path.GetFileName(exePath);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Usage: {0} <file> [<output-file>]", exeName);
            Console.WriteLine("If no output file is specified, output is printed to to the console.");
            Console.ResetColor();
        }

        private static void Pause()
        {
#if DEBUG
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif
        }
    }
}
