namespace CompilerCore
{
    public interface ISymbolTable
    {
        bool ContainsString(string lexeme);

        ISymbol GetSymbolFor(string lexeme);

        ISymbol InstallSymbol(string lexeme);
    }
}
