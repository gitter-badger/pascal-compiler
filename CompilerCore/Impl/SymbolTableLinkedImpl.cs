using System.Collections.Generic;
using System.Linq;

namespace CompilerCore
{
    internal class SymbolTableLinkedImpl : ISymbolTable
    {
        private Dictionary<string, Symbol> Symbols { get; set; }

        internal SymbolTableLinkedImpl()
        {
            Symbols = new Dictionary<string, Symbol>();
            LoadKeywords();
            LoadOperators();
        }

        public bool ContainsString(string lexeme)
        {
            return Symbols.ContainsKey(lexeme);
        }

        internal TokenType GetTokenTypeFor(string lexeme)
        {
            return GetOrCreateSymbolFor(lexeme).CurrentAttribute.TokenType;
        }

        private Symbol GetOrCreateSymbolFor(string lexeme)
        {
            if (ContainsString(lexeme))
            {
                return Symbols[lexeme];
            }

            var symbol = new Symbol(lexeme);
            Symbols[lexeme] = symbol;
            return symbol;
        }

        private void LoadKeywords()
        {
            foreach (var symbol in Utils.GetKeywords().Select(GetOrCreateSymbolFor))
            {
                symbol.CurrentAttribute.SemanticType = SemanticType.Keyword;
            }
        }

        private void LoadOperators()
        {
            foreach (var symbol in Utils.GetOperators().Select(GetOrCreateSymbolFor))
            {
                symbol.CurrentAttribute.SemanticType = SemanticType.Operator;
            }
        }
    }
}
