using System.Linq;

namespace CompilerCore.Impl
{
    internal class SymbolTableTreeImpl : ISymbolTable
    {
        private Scope RootScope { get; set; }

        private Scope CurrentScope { get; set; }

        internal SymbolTableTreeImpl()
        {
            RootScope = new Scope();
            CurrentScope = RootScope;
            LoadInitialSymbols();
        }

        internal void OpenNewScope()
        {
            CurrentScope = CurrentScope.OpenNewScope();
        }

        internal void CloseScope()
        {
            CurrentScope = CurrentScope.ParentScope;
        }

        public bool ContainsString(string lexeme)
        {
            return CurrentScope.Ancestry().Any(sc => sc.ContainsString(lexeme));
        }

        public ISymbol GetSymbolFor(string lexeme)
        {
            return CurrentScope.Ancestry().First(sc => sc.ContainsString(lexeme)).GetSymbolFor(lexeme);
        }

        public ISymbol InstallSymbol(string lexeme)
        {
            return CurrentScope.InstallSymbol(lexeme);
        }

        private void LoadInitialSymbols()
        {
            foreach (var kw in Utils.GetKeywords())
            {
                var symbol = CurrentScope.InstallSymbol(kw);
                symbol.CurrentAttribute.SemanticType = SemanticType.Keyword;
            }

            foreach (var kw in Utils.GetOperators())
            {
                var symbol = CurrentScope.InstallSymbol(kw);
                symbol.CurrentAttribute.SemanticType = SemanticType.Operator;
            }
        }
    }
}
