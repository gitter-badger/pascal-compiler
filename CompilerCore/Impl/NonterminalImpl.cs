using System;

namespace CompilerCore.Impl
{
    internal class NonterminalImpl : INonterminal
    {
        public string Name { get; private set; }

        public bool IsNonterminal { get { return true; } }

        public bool IsTerminal { get { return false; } }

        public bool IsEpsilon { get { return false; } }

        public bool IsEofMarker { get { return false; } }

        internal NonterminalImpl(string name)
        {
            if (!char.IsUpper(name[0]))
            {
                throw new ArgumentException("Element must start with an uppercase letter.");
            }

            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
