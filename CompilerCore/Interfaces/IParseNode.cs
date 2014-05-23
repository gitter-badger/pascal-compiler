using System.Collections.Generic;

namespace CompilerCore
{
    interface IParseNode
    {
        int Level { get; }

        ILexicalElement Element { get; }

        IEnumerable<IParseNode> GetChildren();

        void AddChildElement(ILexicalElement element);
    }
}
