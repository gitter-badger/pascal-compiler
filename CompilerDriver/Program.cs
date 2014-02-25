using System;
using System.Collections.Generic;
using System.IO;
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
                PrintTokens(args[0]);
            }

            Pause();
        }

        private static void PrintTokens(string path)
        {
            var sc = new Scanner(path);
            var tokens = new List<Token>();

            while (!sc.IsAtEof)
            {
                tokens.Add(sc.GetNextToken());
            }

            foreach (var token in tokens)
            {
#if DEBUG
                Console.WriteLine("{0}\t\t{1}\t\t{2}", token.Lexeme, token.CStyleType, token.Line);
#else
                Console.WriteLine("{0}\t\t{1}", token.Lexeme, token.CStyleType);
#endif
            }
        }

        private static void PrintUsage()
        {
            var exePath = Environment.GetCommandLineArgs()[0];
            var exeName = Path.GetFileName(exePath);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Usage: {0} <file>", exeName);
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
