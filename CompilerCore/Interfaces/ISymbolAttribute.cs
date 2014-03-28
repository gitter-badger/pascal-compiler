namespace CompilerCore
{
    public interface ISymbolAttribute
    {
        TokenType TokenType { get; set; }

        SemanticType SemanticType { get; set; }

        DataType DataType { get; set; }

        ISymbolAttribute ParentAttribute { get; }
    }
}
