using System.Collections.Generic;

namespace CompilerCore.Interfaces
{
    public interface IGrammar
    {
        IProductionRule GetProductionRuleByNumber(int num);

        int Count { get; }

        IEnumerable<ITerminal> GetFirstSetForProductionRule(int num);
    }
}
