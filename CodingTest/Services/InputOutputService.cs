using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTest.Interfaces;

namespace CodingTest.Services
{
    class InputOutputService : IInputOutput
    {
        public List<string> GetInput(string name)
        {
            try
            {
                var fullName = Directory.GetCurrentDirectory() + "\\SampleFiles\\" + name + ".txt";
                
                string text = File.ReadAllText(fullName);
                var words = text.Split(' ').ToList();
                return words;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void OutputResult(string name, string results)
        {
            try
            {
                var fullName = Directory.GetCurrentDirectory() + "\\SampleFiles\\" + name + ".txt";
                if (!File.Exists(fullName))
                {

                    using (StreamWriter sw = File.CreateText(fullName))
                    {
                        sw.WriteLine(results);
                    }
                }
                else
                {
                    File.WriteAllText(fullName, results);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
