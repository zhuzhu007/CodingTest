using CodingTest.Interfaces;
using CodingTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Usage");
            Console.WriteLine("add/update ruleid filter1[-filter2]");
            Console.WriteLine("apply/stop/remove ruleid");
            Console.WriteLine("filters can include more than one chars, use format like this filtername:char1,char2,...) for example -startwith(a,b,c)");
            Console.WriteLine("for example, to add rule 1, type in add rule1 starting_with:a,A");
            //add rule1 starting_with:a,A
            //add rule2 starting_with:b,B-count_of:e,E
            //add rule3 starting_with:a,b,c
            //add rule4 starting_with:c,C-starting_with:a,A

            Console.WriteLine("for example, to apply rule 2, type in apply rule2 starting_with:b,B-count_of:e,E");

            IInputOutput io = new InputOutputService();
            List<string> words = io.GetInput("TextFile1");
            if (words != null && !words.Any())
            {
                Console.WriteLine("Please check your input file");
            }
            var handler = new RuleHandler(io);
            handler.AddRule("rule1", new List<RuleFilter> { new RuleFilter("starting_with", new List<char> { 'a', 'A'}) });
            handler.AddRule("rule2", new List<RuleFilter> { new RuleFilter("starting_with", new List<char> { 'b', 'B' }), new RuleFilter("count_of", new List<char> { 'e', 'E' }) });
            handler.AddRule("rule3", new List<RuleFilter> { new RuleFilter("starting_with", new List<char> { 'a', 'b','c' }) });
            handler.AddRule("rule4", new List<RuleFilter> { new RuleFilter("starting_with", new List<char> { 'c', 'C' }), new RuleFilter("count_of", new List<char> { 'a', 'A' }) });
            Console.WriteLine("Please type in your command when you are ready");
            string input = Console.ReadLine();
            while (!input.ToLower().Equals("exit"))
            {
                var spaceSplit = input.Split(' ');
                if(!spaceSplit.Any() || spaceSplit.Length < 2)
                {
                    Console.WriteLine("Your input format is wrong");
                }
                else
                {
                    var action = spaceSplit[0];
                    var ruleId = spaceSplit[1];
                    switch (action.ToLower())
                    {
                        case "add":
                        case "update":
                            if(spaceSplit.Length != 3)
                            {
                                Console.WriteLine("Your input format for add/update is wrong");
                            }
                            else
                            {
                                var filterStr = spaceSplit[2];
                                var filters = spaceSplit[2].Split('-');
                                if (!filters.Any() || filters.Length < 1)
                                {
                                    Console.WriteLine("Your action rule filter input format is wrong");
                                }
                                else
                                {
                                    var filterList = new List<RuleFilter>();
                                    foreach (var f in filters)
                                    {
                                        var keyValue = f.Split(':');
                                        if (!keyValue.Any() || keyValue.Length != 2)
                                        {
                                            Console.WriteLine("Your filter input format is wrong");
                                            break;
                                        }
                                        else
                                        {
                                            var key = keyValue[0];
                                            var values = keyValue[1].Split(',');
                                            //need to do a duplicate checking
                                            filterList.Add(new RuleFilter(key, string.Join(string.Empty, values).ToCharArray().ToList()));
                                            
                                        }
                                    }
                                    if (action.ToLower().Equals("add"))
                                    {
                                        handler.AddRule(ruleId, filterList);
                                    }
                                    else
                                    {
                                        handler.ReconfigureRule(ruleId, filterList);
                                    }
                                }
                            }

                            break;
                        case "apply":
                            handler.ApplyRule(ruleId, words);
                            break;

                        case "stop":
                            handler.StopRule(ruleId);
                            break;
                        case "remove":
                            handler.RemoveRule(ruleId);
                            break;
                        default:
                            Console.WriteLine("no such an action exists!");
                            break;
                    }

                }
                
                Console.WriteLine("Please type in your command when you are ready");
                input = Console.ReadLine();
            }
        }
    }
}
