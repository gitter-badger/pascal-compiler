using System.Collections.Generic;

namespace CompilerCore
{
    internal class Symbol
    {
        internal string Lexeme { get; private set; }

        internal SymbolAttribute CurrentAttribute { get { return Attributes[CurrentAttributeIndex]; } }

        private List<SymbolAttribute> Attributes { get; set; }

        private int CurrentAttributeIndex { get; set; }

        internal Symbol(string lexeme)
        {
            Lexeme = lexeme;
            Attributes = new List<SymbolAttribute>();
            OpenNewScope();
        }

        internal void OpenNewScope()
        {
            Attributes.Add(new SymbolAttribute(CurrentAttributeIndex));
            CurrentAttributeIndex = Attributes.Count - 1;
            CurrentAttribute.TokenType = Utils.DetermineTokenTypeFrom(Lexeme);
        }

        internal void CloseCurrentScope()
        {
            CurrentAttributeIndex = CurrentAttribute.ParentAttributeIndex;
        }
    }
}
