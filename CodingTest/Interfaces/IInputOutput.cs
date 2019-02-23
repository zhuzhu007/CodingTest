using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Interfaces
{
    interface IInputOutput
    {
        List<string> GetInput(string name);
        void OutputResult(string name, string results);
    }
}
