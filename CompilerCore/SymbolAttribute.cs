namespace CompilerCore
{
    internal class SymbolAttribute
    {
        internal TokenType TokenType { get; set; } // TODO Consider moving this up to Symbol ...

        internal SemanticType SemanticType { get; set; }

        internal DataType DataType { get; set; }

        internal int ParentAttributeIndex { get; private set; }

        internal SymbolAttribute(int parentIdx)
        {
            TokenType  = TokenType.Unknown;
            SemanticType = SemanticType.Unknown;
            DataType = DataType.Unknown;
            ParentAttributeIndex = parentIdx;
        }
    }
}
