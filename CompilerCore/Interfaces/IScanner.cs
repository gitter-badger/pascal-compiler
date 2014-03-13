namespace CompilerCore
{
    public interface IScanner
    {
        bool HasNextToken();

        string GetNextToken();
    }
}
