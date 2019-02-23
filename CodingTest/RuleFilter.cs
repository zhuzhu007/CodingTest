using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class RuleFilter
    {
        private string _name;
        private List<char> _values;
        public string Name
        {
            get { return _name; }
        }

        public List<char> Values
        {
            get { return _values; }
        }
        public RuleFilter(string name, List<char> values)
        {
            _name = name;
            _values = values;
        }
    }
}
