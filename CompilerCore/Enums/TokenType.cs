namespace CompilerCore
{
    public enum TokenType
    {
        // Operators
        AddOp, // + -
        AssignOp, // :=
        CloseBracket, // ]
        CloseParen, // )
        Colon, // :
        Comma, // ,
        OpenBracket, // [
        OpenParen, // (
        Period, // .
        Range, // ..
        Semicolon, // ;
        Unknown,

        // Words
        Array,
        Begin,
        Do,
        Else,
        End,
        Function,
        Id,
        If,
        Integer,
        MulOp,
        Not,
        Num,
        Of,
        Procedure,
        Program,
        Real,
        RelOp,
        Then,
        Var,
        While
    }
}
