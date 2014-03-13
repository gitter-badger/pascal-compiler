using System;
using System.Collections.Generic;
using System.Linq;

namespace CompilerCore
{
    internal static class Utils
    {
        internal static TokenType DetermineTokenTypeFrom(string str)
        {
            // Since Pascal is case-insensitive...
            str = str.ToLower();

            if (str.Length == 1) return DetermineTokenTypeFrom(str[0]);
            if (str.All(Char.IsDigit)) return TokenType.Number;

            if (str == "begin") return TokenType.Begin;
            if (str == "call") return TokenType.Call;
            if (str == "const") return TokenType.Constant;
            if (str == "declare") return TokenType.Declare;
            if (str == "do") return TokenType.Do;
            if (str == "else") return TokenType.Else;
            if (str == "end") return TokenType.End;
            if (str == "endif") return TokenType.EndIf;
            if (str == "enduntil") return TokenType.EndUntil;
            if (str == "endwhile") return TokenType.EndWhile;
            if (str == "if") return TokenType.If;
            if (str == "integer") return TokenType.Integer;
            if (str == "procedure") return TokenType.Procedure;
            if (str == "program") return TokenType.Program;
            if (str == "read") return TokenType.Read;
            if (str == "real") return TokenType.Real;
            if (str == "set") return TokenType.Set;
            if (str == "then") return TokenType.Then;
            if (str == "until") return TokenType.Until;
            if (str == "while") return TokenType.While;
            if (str == "write") return TokenType.Write;

            return TokenType.Identifier;
        }

        private static TokenType DetermineTokenTypeFrom(char ch)
        {
            switch (ch)
            {
                case ')': return TokenType.CloseParen;
                case ',': return TokenType.Comma;
                case '=': return TokenType.Equals;
                case '>': return TokenType.Greater;
                case '<': return TokenType.Less;
                case '-': return TokenType.Minus;
                case '.': return TokenType.Period;
                case '+': return TokenType.Plus;
                case ';': return TokenType.Semicolon;
                case '/': return TokenType.Slash;
                case '*': return TokenType.Star;
                case '(': return TokenType.OpenParen;
            }

            if (Char.IsDigit(ch)) return TokenType.Number;
            if (Char.IsLetter(ch)) return TokenType.Identifier;

            return TokenType.Unknown;
        }

        internal static string CStyleStringFor(DataType dt)
        {
            return "dt" + dt.ToString().ToLower();
        }

        internal static string CStyleStringFor(TokenType tok)
        {
            return "tok" + tok.ToString().ToLower();
        }

        internal static string CStyleStringFor(SemanticType st)
        {
            return "st" + st.ToString().ToLower();
        }

        internal static string TokenOutputFormat(string lexeme, TokenType tt)
        {
            var cStyleType = CStyleStringFor(tt);

            return string.Format("{0}\t\t{1}", lexeme, cStyleType);
        }

        internal static IEnumerable<string> GetKeywords()
        {
            var keywords = new List<string>
            {
                "begin",
                "call",
                "const",
                "declare",
                "do",
                "else",
                "end",
                "endif",
                "enduntil",
                "endwhile",
                "if",
                "procedure",
                "program",
                "read",
                "real",
                "set",
                "then",
                "until",
                "while",
                "write",
            };
            return keywords;
        }

        internal static IEnumerable<string> GetOperators()
        {
            var operators = new List<string>
            {
                ")", ",", "=", ">", "<", "-", ".", "+", ";", "/", "*", "(",
            };
            return operators;
        }
    }
}
