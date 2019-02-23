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
            if (Filters.Any() && Filters.Count == 2 && words.Any())
            {
                var filter1 = Filters[0];
                var filter2 = Filters[1];
                if (filter1.Values.Any() && filter2.Values.Any())
                {
                    var count = 0;
                    for (int i = 0; i < words.Count - 1; i++)
                    {
                        if (filter1.Values.Any(x => words[i].StartsWith(x.ToString())) &&
                            filter2.Values.Any(x => words[i + 1].StartsWith(x.ToString())))
                            count++;
                    }
                    return count;
                }
            }
            //if no filters or words
            return -1;
        }

        public override string GetRuleName()
        {
            if (Filters.Any() && Filters.Count == 2)
            {
                var filter1 = Filters[0];
                var filter2 = Filters[1];
                if (!string.IsNullOrEmpty(filter1.Name) && filter1.Values.Any() && !string.IsNullOrEmpty(filter2.Name) && filter2.Values.Any())
                {
                    var lowcases1 = FilterOutUpperCase(filter1.Values);
                    var lowcases2 = FilterOutUpperCase(filter2.Values);
                    return $"count_of_sequence_of_words_{filter1.Name}_{string.Join("", lowcases1)}_and_{string.Join("", lowcases2)}";
                }

            }

            return "something wrong in rule4's filter, cannot get the name";
        }
    }
}
