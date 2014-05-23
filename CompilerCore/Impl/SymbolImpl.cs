using System.Collections.Generic;

namespace CompilerCore.Impl
{
    internal class SymbolImpl : ISymbol
    {
        public string Lexeme { get; private set; }

        public ISymbolAttribute CurrentAttribute { get { return _currentAttribute; } }

        private List<ISymbolAttribute> Attributes { get; set; }

        private SymbolAttributeImpl _currentAttribute;

        internal SymbolImpl(string lexeme)
        {
            Lexeme = lexeme;
            Attributes = new List<ISymbolAttribute>();
            OpenNewScope();
        }

        internal void OpenNewScope()
        {
            var newAttr = new SymbolAttributeImpl(CurrentAttribute);
            Attributes.Add(newAttr);
            _currentAttribute = newAttr;
        }

        internal void CloseCurrentScope()
        {
            _currentAttribute = (SymbolAttributeImpl) CurrentAttribute.ParentAttribute;
        }
    }
}
