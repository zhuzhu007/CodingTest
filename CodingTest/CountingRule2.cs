using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class CountingRule2 : ACountingRule
    {
        public CountingRule2(string ruleId, List<RuleFilter> filters) : base(ruleId, filters)
        {
            ValidateFilters(2);
        }
        public override int Count(List<string> words)
        {
            if (words == null || !words.Any())
                return -1;
            var filter1 = Filters[0];
            var filter2 = Filters[1];
            var startWithbOrB = words.Where(x => filter1.Values.Any(y => x.StartsWith(y.ToString()))).ToList();
            return startWithbOrB.Sum(x => x.Count(y => filter2.Values.Contains(y)));

        }

        public override string GetRuleName()
        {
            var filter1 = Filters[0];
            var filter2 = Filters[1];
            var lowcases1 = FilterOutUpperCase(filter1.Values);
            var lowcases2 = FilterOutUpperCase(filter2.Values);
            return $"{filter2.Name}_{string.Join("", lowcases2)}_in_words_{filter1.Name}_{string.Join("", lowcases1)}";
        }
    }
}
