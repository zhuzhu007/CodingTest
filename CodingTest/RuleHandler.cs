using CodingTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    class RuleHandler
    {
        private List<ACountingRule> _rules;
        private readonly IInputOutput _ioService;
        public RuleHandler(IInputOutput ioService)
        {
            _ioService = ioService;
            _rules = new List<ACountingRule>();
        }

        private ACountingRule GetRuleById(string ruleId)
        {
            var existing = _rules.SingleOrDefault(r => r.RuleId == ruleId);
            return existing;
        }

        public void AddRule(string ruleId, List<RuleFilter> filters)
        {
            var existing = GetRuleById(ruleId);
            if (existing != null)
            {
                Console.WriteLine("This rule exists already!");
                return;
            }

            switch (ruleId.ToLower())
            {
                case "rule1":
                    _rules.Add(new CountingRule1(ruleId, filters));
                        break;
                case "rule2":
                    _rules.Add(new CountingRule2(ruleId, filters));
                    break;
                case "rule3":
                    _rules.Add(new CountingRule3(ruleId, filters));
                    break;
                case "rule4":
                    _rules.Add(new CountingRule4(ruleId, filters));
                    break;
                default:
                    Console.WriteLine("Not support such a rule: " + ruleId);
                    return;
            }
            Console.WriteLine("This rule has been added successfully!");
        }

        public void RemoveRule(string ruleId)
        {
            var existing = GetRuleById(ruleId);
            if (existing != null)
            {
                _rules.Remove(existing);
                Console.WriteLine("This rule has been removed successfully!");
            }
                
            else
            {
                Console.WriteLine("This rule does not exist!");
            }
                
        }

        public void ApplyRule(string ruleId, List<string> words)
        {
            var existing = GetRuleById(ruleId);
            if (existing == null)
            {
                Console.WriteLine("This rule does not exist!");
                return;
            }
            if(existing.RuleStatus == RuleStatus.Stopped)
            {
                Console.WriteLine("This rule has been stopped!");
                return;
            }
            var result = existing.Count(words);
            var name = existing.GetRuleName();
            _ioService.OutputResult(name, result.ToString());
            Console.WriteLine("This rule has been applied successfully!");
        }

        public void ReconfigureRule(string ruleId, List<RuleFilter> filters)
        {
            var existing = GetRuleById(ruleId);
            if (existing == null)
            {
                Console.WriteLine("This rule does not exist!");
                return;
            }
            if (existing.RuleStatus == RuleStatus.Stopped)
            {
                Console.WriteLine("This rule has been stopped!");
                return;
            }
            existing.SetFilters(filters);
            
            Console.WriteLine("This rule has been reconfigured successfully!");
        }

        public void StopRule(string ruleId)
        {
            var existing = GetRuleById(ruleId);
            if (existing == null)
            {
                Console.WriteLine("This rule does not exist!");
                return;
            }
            if(existing.RuleStatus == RuleStatus.Stopped)
            {
                Console.WriteLine("This rule has been stopped already!");
                return;
            }
            existing.RuleStatus = RuleStatus.Stopped;
            Console.WriteLine("This rule has been stopped successfully!");
        }

        //maybe need restart rule
    }
}
