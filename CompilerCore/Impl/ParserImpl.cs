using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompilerCore.Interfaces;

namespace CompilerCore.Impl
{
    internal class ParserImpl : IParser
    {
        private IGrammar Grammar { get; set; }

        private IParseTable ParseTable { get; set; }

        private IScanner Scanner { get; set; }

        private ISymbolTable SymbolTable { get; set; }

        internal ParserImpl(string filepath)
        {
            Grammar = Factory.ReadGrammarFrom("Resources/NormalizedGrammar.txt");

            ParseTable = Factory.ParseTableFrom(Grammar);

            Scanner = Factory.ScannerFor(filepath);

            SymbolTable = Factory.SymbolTable();
        }

        public void Parse()
        {
            if (!Scanner.HasNextToken())
            {
                var message = "Unexpected EOF.";
                //throw new InvalidDataException(message);
                Logger.RedErrorMessage(message);
                return;
            }

            var rootLexical = Factory.NonterminalFor("Program");
            var rootNode = Factory.ParseNode(rootLexical);
            var stack = new Stack<IParseNode>();
            stack.Push(rootNode);

            var token = Scanner.GetNextToken();
            var symbol = SymbolTable.SymbolFor(token);
            var symType = symbol.CurrentAttribute.TokenType.ToString().ToLower();

            while (stack.Any())
            {
                var ontop = stack.Peek();
                if (ontop.Element.IsTerminal)
                {
                    var cStyleType = "tok" + symType;
                    var outputLine = string.Format("{0}\t\t{1}", token, cStyleType);
                    Logger.Log(outputLine, Factory.ScannerTag);

                    if (ontop.Element.Name == symType ||
                        SymbolTable.SymbolFor(ontop.Element.Name) == symbol)
                    {
                        stack.Pop();
                        if (Scanner.HasNextToken())
                        {
                            token = Scanner.GetNextToken();
                            symbol = SymbolTable.SymbolFor(token);
                            symType = symbol.CurrentAttribute.TokenType.ToString().ToLower();
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        var format = "Expected token of type \"{0}\"; found \"{1}\" (type \"{2}\") instead.";
                        var message = string.Format(format, ontop.Element.Name, token, symType);
                        //throw new InvalidDataException(message);
                        Logger.RedErrorMessage(message);
                        break;
                    }
                }
                else if (ontop.Element.IsNonterminal)
                {
                    var nont = (INonterminal) ontop.Element;
                    var terminal = GetTerminalTypeForSymbol(symbol);
                    var prodRuleNum = ParseTable.GetProductionRuleNumberFor(nont, terminal);
                    if (prodRuleNum == 0)
                    {
                        var format =
                            "Token (\"{0}\") of terminal type \"{1}\" is not in the FIRST set of nonterminal \"{2}\".";
                        var message = string.Format(format, token, terminal.Name, nont.Name);
                        //throw new InvalidDataException(message);
                        Logger.RedErrorMessage(message);
                        break;
                    }
                    else
                    {
                        stack.Pop();
                        var prodRule = Grammar.GetProductionRuleByNumber(prodRuleNum);

                        var rhs = prodRule.RightHandSide.ToList();
                        foreach (var lexicalElement in rhs)
                        {
                            ontop.AddChildElement(lexicalElement);
                        }

                        ontop.GetChildren().Reverse().ToList().ForEach(stack.Push);
                    }
                }
                else
                {
                    stack.Pop();
                }
            }

            LogParseTree(rootNode);
        }

        private static ITerminal GetTerminalTypeForSymbol(ISymbol symbol)
        {
            if (symbol.CurrentAttribute.TokenType == TokenType.Id) return Factory.TerminalFor("id");
            if (symbol.CurrentAttribute.TokenType == TokenType.Num) return Factory.TerminalFor("num");

            return Factory.TerminalFor(symbol.Lexeme);
        }

        private void LogParseTree(IParseNode node)
        {
            var indent = string.Join("", Enumerable.Repeat("  ", node.Level));
            Logger.Log(indent + node.Element.Name, Factory.ParserTag);
            node.GetChildren().ToList().ForEach(LogParseTree);
        }
    }
}
