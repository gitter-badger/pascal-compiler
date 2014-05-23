using System;
using System.Collections.Generic;

namespace CompilerCore.Impl
{
    internal class ParseNodeImpl : IParseNode
    {
        public int Level { get; private set; }
        public ILexicalElement Element { get; private set; }

        private IList<IParseNode> Children { get; set; }
        
        internal ParseNodeImpl(ILexicalElement element, int level)
        {
            Element = element;
            Level = level;
            Children = new List<IParseNode>();
        }

        public IEnumerable<IParseNode> GetChildren()
        {
            return Children;
        }

        public void AddChildElement(ILexicalElement element)
        {
            if (Element.IsTerminal)
            {
                var format =
                    "Child parse nodes cannot be added to a node that contains a terminal (in this case, \"{0}\")";
                var message = string.Format(format, Element.Name);
                throw new InvalidOperationException(message);
            }

            var child = Factory.ParseNode(element, Level + 1);
            Children.Add(child);
        }
    }
}
