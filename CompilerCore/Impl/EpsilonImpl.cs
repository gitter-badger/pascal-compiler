using CompilerCore.Interfaces;

namespace CompilerCore.Impl
{
    internal class EpsilonImpl : IEpsilon
    {
        public string Name { get { return "<nil>"; } }

        public bool IsNonterminal { get { return false; } }

        public bool IsTerminal { get { return false; } }

        public bool IsEpsilon { get { return true; } }

        public bool IsEofMarker { get { return false; } }

        public override string ToString()
        {
            return Name;
        }
    }
}
