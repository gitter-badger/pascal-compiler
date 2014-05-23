using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using CompilerCore.Interfaces;

namespace CompilerCore.Impl
{
    internal class GrammarImpl : IGrammar
    {
        private IList<IProductionRule> ProductionRules { get; set; }

        private HashSet<ITerminal>[] ProductionRuleFirstSets { get; set; }

        private Dictionary<string, HashSet<ITerminal>> FirstSets { get; set; }

        private Dictionary<string, HashSet<ITerminal>> FollowSets { get; set; }

        public int Count { get { return ProductionRules.Count; } }

        internal GrammarImpl(IEnumerable<string> lines)
        {
            LoadProductions(lines);

            ProductionRuleFirstSets = new HashSet<ITerminal>[ProductionRules.Count];
            FirstSets = new Dictionary<string, HashSet<ITerminal>>();
            FollowSets = new Dictionary<string, HashSet<ITerminal>>();
        }

        public IProductionRule GetProductionRuleByNumber(int num)
        {
            return ProductionRules[num-1];
        }

        public IEnumerable<ITerminal> GetFirstSetForProductionRule(int num)
        {
            var rule = GetProductionRuleByNumber(num);

            var set = new HashSet<ITerminal>();
            var first = rule.RightHandSide.First();
            if (first.IsTerminal)
            {
                set.Add(first as ITerminal);
            }
            else if (first.IsEpsilon)
            {
                var followSet = DetermineFollowSetFor(rule.LeftHandSide);
                set.UnionWith(followSet);
            }
            else if (first.IsNonterminal)
            {
                var firstSet = DetermineFirstSetFor(first as INonterminal);
                set.UnionWith(firstSet);
            }

            ProductionRuleFirstSets[num - 1] = set;
            return set;
        }

        private void LoadProductions(IEnumerable<string> lines)
        {
            ProductionRules = new List<IProductionRule>();

            var i = 0;
            foreach (var line in lines)
            {
                var lineSplit = line.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                var nonterminal = Factory.NonterminalFor(lineSplit[0].Trim());
                var handle = lineSplit[1].Trim().Split(' ').Select(x => x.Trim()).Select(Factory.LexicalElementFor);
                ProductionRules.Add(Factory.ProductionRuleFor(nonterminal, handle, ++i));
            }
        }

        private IEnumerable<ITerminal> DetermineFirstSetFor(INonterminal nont)
        {
            if (FirstSets.ContainsKey(nont.Name)) return FirstSets[nont.Name];

            var set = new HashSet<ITerminal>();
            var matchingRules = ProductionRules.Where(r => r.LeftHandSide == nont).ToList();
            foreach (var rule in matchingRules)
            {
                var first = rule.RightHandSide.First();
                if (first.IsTerminal)
                {
                    set.Add(first as ITerminal);
                }
                else if (first.IsEpsilon)
                {
                    var followSet = DetermineFollowSetFor(nont);
                    set.UnionWith(followSet);
                }
                else if (first.IsNonterminal)
                {
                    var firstSet = DetermineFirstSetFor(first as INonterminal);
                    set.UnionWith(firstSet);
                }
            }

            FirstSets[nont.Name] = set;
            return set;
        }

        private IEnumerable<ITerminal> DetermineFollowSetFor(INonterminal nont)
        {
            if (FollowSets.ContainsKey(nont.Name)) return FollowSets[nont.Name];

            if (nont.Name == "Program")
            {
                var hardcodedSet = new HashSet<ITerminal> { Factory.TerminalFor("<eof>") };
                FollowSets[nont.Name] = hardcodedSet;
                return hardcodedSet;
            }

            var set = new HashSet<ITerminal>();
            var matchingRules = ProductionRules.Where(r => r.RightHandSide.Contains(nont)).ToList();
            foreach (var rule in matchingRules)
            {
                var next = rule.RightHandSide.SkipWhile(x => x != nont).Skip(1).FirstOrDefault();
                if (next == null || next.IsEpsilon)
                {
                    var followSet = nont == rule.LeftHandSide
                        ? Enumerable.Empty<ITerminal>()
                        : DetermineFollowSetFor(rule.LeftHandSide);
                    set.UnionWith(followSet);
                }
                else if (next.IsTerminal)
                {
                    set.Add(next as ITerminal);
                }
                else if (next.IsNonterminal)
                {
                    var firstSet = DetermineFirstSetFor(next as INonterminal);
                    set.UnionWith(firstSet);
                }
            }

            FollowSets[nont.Name] = set;
            return set;
        }
    }
}
