using System.Collections.Generic;
using CompilerCore.Interfaces;

namespace CompilerCore.Impl
{
    internal class ProductionRuleImpl : IProductionRule
    {
        public int Number { get; private set; }

        public INonterminal LeftHandSide { get; private set; }

        public IEnumerable<ILexicalElement> RightHandSide { get; private set; }

        internal ProductionRuleImpl(INonterminal lhs, IEnumerable<ILexicalElement> rhs, int num=-1)
        {
            Number = num;
            LeftHandSide = lhs;
            RightHandSide = rhs;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} -> {2}", Number, LeftHandSide, string.Join(" ", RightHandSide));
        }
    }
}
