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
            if (Filters.Any() && words.Any())
            {
                var filter1 = Filters.First().Values;
                if (filter1.Any())
                {
                    var startWithaOrA = words.Where(x => filter1.Any(y => x.StartsWith(y.ToString()))).ToList();
                    return Convert.ToInt32(startWithaOrA.Average(x => x.Length));
                }
                
            }
            //if no filters or words
            return -1;
        }

        public override string GetRuleName()
        {
            if (Filters.Any())
            {
                var filter1 = Filters.First();
                if (!string.IsNullOrEmpty(filter1.Name) && filter1.Values.Any())
                {
                    var lowcases = FilterOutUpperCase(filter1.Values);
                    return $"average_length_of_words_{filter1.Name}_{string.Join("", lowcases)}";
                }
            }

            return "something wrong in rule1's filter, cannot get the name";
        }
    }
}
