using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public abstract class ACountingRule
    {
        private string _ruleId;
        private RuleStatus _ruleStatus; 
        protected List<RuleFilter> Filters;

        public string RuleId
        {
            get { return _ruleId; }
        }
        public RuleStatus RuleStatus
        {
            get { return _ruleStatus; }
            set { _ruleStatus = value; }
        }

        protected ACountingRule(string ruleId, List<RuleFilter> filters)
        {
            _ruleStatus = RuleStatus.Active;
            _ruleId = ruleId;
            Filters = filters;
        }

        protected void ValidateFilters(int num)
        {
            if(num > 0 && (Filters == null || !Filters.Any()))
            {
                _ruleStatus = RuleStatus.Stopped;
                return;
            }
            if(num == 0 && (Filters != null || Filters.Any()))
            {
                _ruleStatus = RuleStatus.Stopped;
                return;
            }
            if(num > 0 && Filters != null && Filters.Count != num)
            {
                _ruleStatus = RuleStatus.Stopped;
                return;
            }
            if(Filters != null && Filters.Any())
            {
                foreach(var filter in Filters)
                {
                    if(string.IsNullOrEmpty(filter.Name) || filter.Values == null || !filter.Values.Any())
                    {
                        _ruleStatus = RuleStatus.Stopped;
                        return;
                    }
                }
            }
        }
        public void SetFilters(List<RuleFilter> filters)
        {
            Filters = filters;
        }

        protected List<string> FilterOutUpperCase(List<char> filter)
        {
            var lowcases = new List<string>();

            if (filter.Any())
            {
                foreach (var c in filter)
                {
                    var sc = c.ToString().ToLower();
                    if (!lowcases.Exists(x => x.Equals(sc)))
                        lowcases.Add(sc);
                }
                return lowcases;
            }
            return null;
        }

        public abstract int Count(List<string> words);

        public abstract string GetRuleName();
    }
}
