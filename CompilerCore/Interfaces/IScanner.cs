namespace CompilerCore
{
    public interface IScanner
    {
        bool HasNextToken();

        IToken GetNextToken();
    }
}
