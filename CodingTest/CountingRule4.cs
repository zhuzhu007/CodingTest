using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class CountingRule4 : ACountingRule
    {
        public CountingRule4(string ruleId, List<RuleFilter> filters) : base(ruleId, filters)
        {
            ValidateFilters(2);
        }
        public override int Count(List<string> words)
        {
            if (words == null || !words.Any() || words.Count < 2)
                return -1;
            var filter1 = Filters[0];
            var filter2 = Filters[1];
            var count = 0;
            for (int i = 0; i < words.Count - 1; i++)
            {
                if (filter1.Values.Any(x => words[i].StartsWith(x.ToString())) &&
                    filter2.Values.Any(x => words[i + 1].StartsWith(x.ToString())))
                    count++;
            }
            return count;

        }

        public override string GetRuleName()
        {
            var filter1 = Filters[0];
            var filter2 = Filters[1];
            var lowcases1 = FilterOutUpperCase(filter1.Values);
            var lowcases2 = FilterOutUpperCase(filter2.Values);
            return $"count_of_sequence_of_words_{filter1.Name}_{string.Join("", lowcases1)}_and_{string.Join("", lowcases2)}";
        }
    }
}
