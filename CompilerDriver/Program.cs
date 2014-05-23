using System;
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
                var cf = Factory.ParserFor(args[0]);
                cf.Parse();
                //Logger.Dump(Factory.ParserTag);

                var filebase = Path.GetFileNameWithoutExtension(args[0]);
                var filext = Path.GetExtension(args[0]);

                var tag = Factory.ScannerTag;
                var outputFilename = filebase + "-" + tag + "Output" + filext;
                Logger.WriteTo(outputFilename, tag);

                tag = Factory.ParserTag;
                outputFilename = filebase + "-" + tag + "Output" + filext;
                Logger.WriteTo(outputFilename, tag);
            }

            Pause();
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
