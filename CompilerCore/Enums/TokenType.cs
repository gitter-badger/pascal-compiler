namespace CompilerCore
{
    public enum TokenType
    {
        Word, // tokword
        Number, // toknumber
        Operator, // tokop

        Begin, // tokbegin
        Call, // tokcall
        CloseParen, // tokcloseparen
        Comma, // tokcomma
        Constant, // tokconstant
        Declare, // tokdeclare
        Do, // tokdo
        Else, // tokelse
        End, // tokend
        EndIf, // tokendif
        EndUntil, // tokenduntil
        EndWhile, // tokendwhile
        Eof, // tokeof
        Equals, // tokequals
        Error, // tokerror
        Float, // tokfloat
        Greater, // tokgreater
        Identifier, // tokidentifier
        If, // tokif
        Integer, // tokinteger
        Less, // tokless
        Minus, // tokminus
        NotEqual, // toknotequal
        OpenParen, // tokopenparen
        Parameters, // tokparameters
        Period, // tokperiod
        Plus, // tokplus
        Procedure, // tokprocedure
        Program, // tokprogram
        Read, // tokread
        Real, // tokreal
        Semicolon, // toksemicolon
        Set, // tokset
        Slash, // tokslash
        Star, // tokstar
        Then, // tokthen
        Unknown, // tokunknown
        Until, // tokuntil
        While, // tokwhile
        Write, // tokwrite
    }
}
