namespace CompilerCore
{
    interface IToken
    {
        string Lexeme { get; }

        int Line { get; }

        TokenType Type { get; }
    }
}
