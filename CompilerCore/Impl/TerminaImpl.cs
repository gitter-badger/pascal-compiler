using System;

namespace CompilerCore.Impl
{
    internal class TerminaImpl : ITerminal
    {
        public string Name { get; private set; }

        public bool IsNonterminal { get { return false; } }

        public bool IsTerminal { get { return true; } }

        public bool IsEpsilon { get { return false; } }

        public bool IsEofMarker { get; private set; }

        internal TerminaImpl(string name)
        {
            if (char.IsUpper(name[0]))
            {
                throw new ArgumentException("Element must not start with an uppercase letter.");
            }

            Name = name;
            IsEofMarker = name == "<eof>";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
