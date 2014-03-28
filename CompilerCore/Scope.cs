using System.Collections.Generic;

namespace CompilerCore
{
    internal class Scope
    {
        internal Scope ParentScope { get; private set; }

        private List<Scope> ChildScopes { get; set; }

        private Dictionary<string, ISymbol> SymbolMap { get; set; }

        internal Scope(Scope parentScope = null)
        {
            ParentScope = parentScope;
            ChildScopes = new List<Scope>();
            SymbolMap = new Dictionary<string, ISymbol>();
        }

        internal Scope OpenNewScope()
        {
            var scope = new Scope(this);
            ChildScopes.Add(scope);
            return scope;
        }

        internal IEnumerable<Scope> Ancestry()
        {
            var curr = this;

            do
            {
                // The "yield" keyword marks this method as an iterator block
                yield return curr;
                curr = curr.ParentScope;
            } while (curr != null);
        }

        internal ISymbol GetSymbolFor(string lexeme)
        {
            return SymbolMap[lexeme];
        }

        internal bool ContainsString(string lexeme)
        {
            return SymbolMap.ContainsKey(lexeme);
        }

        internal ISymbol InstallSymbol(string lexeme)
        {
            // TODO Have different logic if the symbol exists?
            var symbol = new SymbolImpl(lexeme);
            SymbolMap[lexeme] = symbol;
            return symbol;
        }
    }
}
