using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class CountingRule1 : ACountingRule
    {
        public CountingRule1(string ruleId, List<RuleFilter> filters) : base(ruleId, filters)
        {
            ValidateFilters(1);
        }
        public override int Count(List<string> words)
        {
            if (words == null || !words.Any())
                return -1;
            var filter1 = Filters.First().Values;
            var startWithaOrA = words.Where(x => filter1.Any(y => x.StartsWith(y.ToString()))).ToList();
            return Convert.ToInt32(startWithaOrA.Average(x => x.Length));
        }

        public override string GetRuleName()
        {
            var filter1 = Filters.First();
            var lowcases = FilterOutUpperCase(filter1.Values);
            return $"average_length_of_words_{filter1.Name}_{string.Join("", lowcases)}";
        }
    }
}
