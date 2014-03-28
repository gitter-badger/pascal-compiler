namespace CompilerCore
{
    public interface ISymbol
    {
        string Lexeme { get; }

        ISymbolAttribute CurrentAttribute { get; }
    }
}
