namespace CompilerCore
{
    public interface ILexicalElement
    {
        string Name { get; }

        bool IsNonterminal { get; }

        bool IsTerminal { get; }

        bool IsEpsilon { get; }

        bool IsEofMarker { get; }
    }
}
