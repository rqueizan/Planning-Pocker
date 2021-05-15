using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Planning.Pocker.Api.Test
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<(Orderer, TTestCase)>>();
            foreach (var testCase in testCases)
            {
                var priority = 0;
                var orderer = Orderer.Code;
                foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(PriorityAttribute).AssemblyQualifiedName)))
                {
                    priority = attr.GetNamedArgument<int>("Priority");
                    orderer = attr.GetNamedArgument<Orderer>("Orderer");
                }
                GetOrCreate(sortedMethods, priority).Add((orderer, testCase));
            }
            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                var orderCases = list.Select(l => l.Item2).ToList();
                var order = list.Select(l => l.Item1).Max();
                switch (order)
                {
                    case Orderer.DisplayName:
                        orderCases.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.DisplayName, y.DisplayName));
                        break;
                    case Orderer.MethodName:
                        orderCases.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                        break;
                    case Orderer.Random:
                        var random = new Random();
                        var temp = new List<TTestCase>();
                        while (orderCases.Count > 0)
                        {
                            var i = random.Next(orderCases.Count);
                            var testCase = orderCases[i];
                            orderCases.RemoveAt(i);
                            temp.Add(testCase);
                        }
                        orderCases = temp;
                        break;
                    case Orderer.Code:
                    default: break;
                }
                foreach (var testCase in orderCases)
                    yield return testCase;
            }
        }

        static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result)) return result;
            return dictionary[key] = new TValue();
        }
    }
}
