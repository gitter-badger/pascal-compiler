using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CompilerCore.Impl
{
    internal class SymbolTableLinkedImpl : ISymbolTable
    {
        private Dictionary<string, ISymbol> SymbolMap { get; set; }

        internal SymbolTableLinkedImpl()
        {
            SymbolMap = new Dictionary<string, ISymbol>();
            LoadInitialSymbols();
        }

        public bool ContainsSymbol(string lexeme)
        {
            return SymbolMap.ContainsKey(lexeme);
        }

        public ISymbol SymbolFor(string lexeme)
        {
            // Since Pascal is case-insensitive...
            lexeme = lexeme.ToLower();

            if (SymbolMap.ContainsKey(lexeme))
            {
                return SymbolMap[lexeme];
            }

            var sym = Factory.SymbolFor(lexeme);

            if (lexeme.All(char.IsDigit))
            {
                sym.CurrentAttribute.TokenType = TokenType.Num;
                sym.CurrentAttribute.IntValue = int.Parse(lexeme);
                return sym;
            }

            if (Regex.IsMatch(lexeme,"[0-9]*\\.[0-9]*"))
            {
                sym.CurrentAttribute.TokenType = TokenType.Num;
                sym.CurrentAttribute.DoubleValue = double.Parse(lexeme);
                return sym;
            }

            if (char.IsLetter(lexeme[0]))
            {
                sym.CurrentAttribute.TokenType = TokenType.Id;
                SymbolMap[lexeme] = sym;
                return sym;
            }

            sym.CurrentAttribute.SemanticType = SemanticType.Unknown;
            sym.CurrentAttribute.TokenType = TokenType.Unknown;
            return sym;
        }

        private void InstallSymbol(ISymbol symbol)
        {
            SymbolMap[symbol.Lexeme] = symbol;
        }

        private void LoadInitialSymbols()
        {
            AddKeyword("array", TokenType.Array);
            AddKeyword("begin", TokenType.Begin);
            AddKeyword("do", TokenType.Do);
            AddKeyword("else", TokenType.Else);
            AddKeyword("end", TokenType.End);
            AddKeyword("function", TokenType.Function);
            AddKeyword("if", TokenType.If);
            AddKeyword("integer", TokenType.Integer);
            AddKeyword("not", TokenType.Not);
            AddKeyword("of", TokenType.Of);
            AddKeyword("procedure", TokenType.Procedure);
            AddKeyword("program", TokenType.Program);
            AddKeyword("real", TokenType.Real);
            AddKeyword("then", TokenType.Then);
            AddKeyword("var", TokenType.Var);
            AddKeyword("while", TokenType.While);

            AddOperator("+", TokenType.AddOp);
            AddOperator("-", TokenType.AddOp);
            AddOperator(":=", TokenType.AssignOp);
            AddOperator("]", TokenType.CloseBracket);
            AddOperator(")", TokenType.CloseParen);
            AddOperator(":", TokenType.Colon);
            AddOperator(",", TokenType.Comma);
            AddOperator("*", TokenType.MulOp);
            AddOperator("/", TokenType.MulOp);
            AddOperator("[", TokenType.OpenBracket);
            AddOperator("(", TokenType.OpenParen);
            AddOperator(".", TokenType.Period);
            AddOperator("..", TokenType.Range);
            AddOperator(";", TokenType.Semicolon);
        }

        private void AddKeyword(string str, TokenType tt)
        {
            var sym = Factory.SymbolFor(str);
            sym.CurrentAttribute.SemanticType = SemanticType.Keyword;
            sym.CurrentAttribute.TokenType = tt;
            InstallSymbol(sym);
        }

        private void AddOperator(string str, TokenType tt)
        {
            var sym = Factory.SymbolFor(str);
            sym.CurrentAttribute.SemanticType = SemanticType.Operator;
            sym.CurrentAttribute.TokenType = tt;
            InstallSymbol(sym);
        }
    }
}
