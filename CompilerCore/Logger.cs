using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompilerCore
{
    public static class Logger
    {
        private const string DefaultTag = "Default";
        private static readonly Dictionary<string,IList<string>> MessageLog = new Dictionary<string, IList<string>>();

        public static void Log(string message, string tag = DefaultTag)
        {
            IList<string> output;
            if (!MessageLog.TryGetValue(tag, out output))
            {
                output = new List<string>();
                MessageLog[tag] = output;
            }

            output.Add(message);
        }

        public static void WriteTo(string outputFile, string tag = DefaultTag)
        {
            IList<string> output;
            if (MessageLog.TryGetValue(tag, out output))
            {
                File.WriteAllLines(outputFile, output);
            }
        }

        public static void AppendTo(string outputFile, string tag = DefaultTag)
        {
            IList<string> output;
            if (MessageLog.TryGetValue(tag, out output))
            {
                File.AppendAllLines(outputFile, output);
            }
        }

        public static void Dump(string tag = DefaultTag)
        {
            IList<string> output;
            if (MessageLog.TryGetValue(tag, out output))
            {
                output.ToList().ForEach(Console.WriteLine);
            }
        }

        public static void Flush(string tag = DefaultTag)
        {
            MessageLog.Remove(tag);
        }

        public static void RedErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }
    }
}
