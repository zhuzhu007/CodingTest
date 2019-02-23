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
            if (Filters.Any() && Filters.Count == 2 && words.Any())
            {
                var filter1 = Filters[0];
                var filter2 = Filters[1];
                if (filter1.Values.Any() && filter2.Values.Any())
                {
                    var startWithbOrB = words.Where(x => filter1.Values.Any(y => x.StartsWith(y.ToString()))).ToList();
                    return startWithbOrB.Sum(x => x.Count(y => filter2.Values.Contains(y)));
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
                    return $"{filter2.Name}_{string.Join("", lowcases2)}_in_words_{filter1.Name}_{string.Join("", lowcases1)}";
                }

            }

            return "something wrong in rule2's filter, cannot get the name";
        }
    }
}
