namespace CompilerCore
{
    public interface ISymbolTable
    {
        bool ContainsSymbol(string lexeme);

        ISymbol SymbolFor(string lexeme);
    }
}
