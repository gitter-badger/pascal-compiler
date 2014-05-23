using System;
using System.Collections.Generic;
using System.Linq;
using CompilerCore.Interfaces;

namespace CompilerCore.Impl
{
    internal class ParseTableImpl : IParseTable
    {
        private ILookup<string,int> NontToRuleIndexMap { get; set; }

        private HashSet<ITerminal>[] ParseTable { get; set; }

        internal ParseTableImpl(IGrammar grammar)
        {
            NontToRuleIndexMap = Enumerable.Range(1, grammar.Count)
                .Select(grammar.GetProductionRuleByNumber)
                .ToLookup(x => x.LeftHandSide.Name, x => x.Number - 1);

            ParseTable =
                Enumerable.Range(1, grammar.Count)
                    .Select(grammar.GetFirstSetForProductionRule)
                    .Select(fis => new HashSet<ITerminal>(fis))
                    .ToArray();
        }

        public int GetProductionRuleNumberFor(INonterminal nont, ITerminal lookahead)
        {
            var prodRuleIndices = NontToRuleIndexMap[nont.Name].ToList();
            for (var i = 0; i < prodRuleIndices.Count; i++)
            {
                var ruleIdx = prodRuleIndices[i];
                if (ParseTable[ruleIdx].Contains(lookahead))
                {
                    return ruleIdx + 1;
                }
            }

            return 0;
        }
    }
}
