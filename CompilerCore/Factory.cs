using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompilerCore.Impl;
using CompilerCore.Interfaces;

namespace CompilerCore
{
    public static class Factory
    {
        public const string ScannerTag = "Scanner";
        public const string ParserTag = "Parser";

        private static readonly Dictionary<string, ITerminal> TerminalCache = new Dictionary<string, ITerminal>();

        private static readonly Dictionary<string, INonterminal> NonterminalCache = new Dictionary<string, INonterminal>();

        internal static readonly IEpsilon Epsilon = new EpsilonImpl();

        public static ITerminal TerminalFor(string name)
        {
            if (name == ".") name = "period";
            if (name == "..") name = "range";
            if (name == ":") name = "colon";
            if (name == ";") name = "semicolon";
            if (name == "+") name = "addop";
            if (name == "-") name = "addop";
            if (name == ",") name = "comma";
            if (name == "*") name = "mulop";
            if (name == "/") name = "mulop";
            if (name == ":=") name = "assignop";

            ITerminal output;
            if (TerminalCache.TryGetValue(name, out output)) return output;

            output = new TerminaImpl(name);
            TerminalCache[name] = output;
            return output;
        }

        public static INonterminal NonterminalFor(string name)
        {
            INonterminal output;
            if (NonterminalCache.TryGetValue(name, out output)) return output;

            output = new NonterminalImpl(name);
            NonterminalCache[name] = output;
            return output;
        }

        public static ILexicalElement LexicalElementFor(string name)
        {
            if (name == "<nil>") return Epsilon;

            return Char.IsUpper(name[0]) ? (ILexicalElement) NonterminalFor(name) : TerminalFor(name);
        }

        public static IProductionRule ProductionRuleFor(INonterminal lhs, IEnumerable<ILexicalElement> rhs, int num = -1)
        {
            return new ProductionRuleImpl(lhs, rhs, num);
        }

        public static IGrammar ReadGrammarFrom(string filepath)
        {
            return new GrammarImpl(File.ReadLines(filepath));
        }

        public static IEnumerable<ILexicalElement> ReadElementsFrom(string filepath)
        {
            return File.ReadLines(filepath).Select(LexicalElementFor);
        }

        public static IEnumerable<IEnumerable<int>> ReadParseMatrixFrom(string filepath)
        {
            return File.ReadLines(filepath)
                .Select(ln => ln.Split('\t').Select(x => x.Any() ? Int32.Parse(x) : 0));
        }

        public static IScanner ScannerFor(string path)
        {
            return new ScannerImpl(path);
        }

        public static ISymbolTable SymbolTable()
        {
            return new SymbolTableLinkedImpl();
        }

        public static ISymbol SymbolFor(String lexeme)
        {
            return new SymbolImpl(lexeme);
        }

        public static IParser ParserFor(string filepath)
        {
            return new ParserImpl(filepath);
        }

        public static IParseTable ParseTableFrom(IGrammar grammar)
        {
            return new ParseTableImpl(grammar);
        }

        internal static IParseNode ParseNode(ILexicalElement element, int level = 0)
        {
            return new ParseNodeImpl(element, level);
        }
    }
}
