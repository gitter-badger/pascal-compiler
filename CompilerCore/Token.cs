namespace CompilerCore
{
    public class Token : IToken
    {
        public string Lexeme { get; private set; }
        public int Line { get; private set; } // the line number (not a zero-based index) where the token was encountered

        public TokenType Type { get; private set; }

        internal Token(string lexeme, int line)
        {
            Lexeme = lexeme;
            Line = line;
            Type = DetermineTokenTypeFromString(lexeme);
        }

        // A quick and dirty way to determine the token type of a string by checking the first character
        private static TokenType DetermineTokenTypeFromString(string str)
        {
            if (char.IsLetter(str[0])) return TokenType.Word;

            if (char.IsDigit(str[0])) return TokenType.Number;

            return TokenType.Operator;
        }
    }
}
