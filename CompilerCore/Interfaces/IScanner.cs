namespace CompilerCore
{
    interface IScanner
    {
        bool HasNextToken();

        IToken GetNextToken();
    }
}
