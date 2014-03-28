using System.Collections.Generic;
using System.Linq;

namespace CompilerCore
{
    internal class SymbolTableLinkedImpl : ISymbolTable
    {
        private Dictionary<string, SymbolImpl> SymbolMap { get; set; }

        internal SymbolTableLinkedImpl()
        {
            SymbolMap = new Dictionary<string, SymbolImpl>();
            LoadInitialSymbols();
        }

        public bool ContainsString(string lexeme)
        {
            return SymbolMap.ContainsKey(lexeme);
        }

        public ISymbol GetSymbolFor(string lexeme)
        {
            return SymbolMap[lexeme];
        }

        public ISymbol InstallSymbol(string lexeme)
        {
            var symbol = new SymbolImpl(lexeme);
            SymbolMap[lexeme] = symbol;
            return symbol;
        }

        private void LoadInitialSymbols()
        {
            foreach (var symbol in Utils.GetKeywords().Select(InstallSymbol))
            {
                symbol.CurrentAttribute.SemanticType = SemanticType.Keyword;
            }

            foreach (var symbol in Utils.GetOperators().Select(InstallSymbol))
            {
                symbol.CurrentAttribute.SemanticType = SemanticType.Operator;
            }
        }
    }
}
