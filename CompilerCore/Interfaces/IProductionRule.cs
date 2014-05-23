using System.Collections.Generic;

namespace CompilerCore.Interfaces
{
    public interface IProductionRule
    {
        int Number { get; }

        INonterminal LeftHandSide { get; }

        IEnumerable<ILexicalElement> RightHandSide { get; }
    }
}
