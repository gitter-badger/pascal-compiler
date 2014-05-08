using System;
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
                var cf = Factory.FrontendFor(args[0]);
                cf.Go(outputPath);
            }

            Pause();
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
