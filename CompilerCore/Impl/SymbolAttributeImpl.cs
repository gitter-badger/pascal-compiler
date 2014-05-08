namespace CompilerCore.Impl
{
    internal class SymbolAttributeImpl : ISymbolAttribute
    {
        public TokenType TokenType { get; set; }

        public SemanticType SemanticType { get; set; }

        public DataType DataType { get; set; }

        public ISymbolAttribute ParentAttribute { get; private set; }

        internal SymbolAttributeImpl(ISymbolAttribute parent)
        {
            TokenType  = TokenType.Unknown;
            SemanticType = SemanticType.Unknown;
            DataType = DataType.Unknown;
            ParentAttribute = parent;
        }
    }
}
