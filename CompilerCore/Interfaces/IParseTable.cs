namespace CompilerCore
{
    public interface IParseTable
    {
        int GetProductionRuleNumberFor(INonterminal nont, ITerminal lookahead);
    }
}
