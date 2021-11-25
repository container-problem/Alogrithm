using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Algorithm.Tests
{
    public class MainTest
    {
        private readonly ITestOutputHelper output;
        private readonly Random random;

        public MainTest(ITestOutputHelper output)
        {
            random = new Random();
            this.output = output;
        }

        [Fact]
        public void TestFF()
        { 
            int containersResult = random.Next(1, 1000);
            var m = random.Next(1, 1000);

            var input = new List<int>();
            var result = new List<List<int>>();

            for(int i = 0; i < containersResult; i++)
            {
                List<int> container = new List<int>();

                int currentLoad = 0;
                int nextElem = random.Next(1, m + 1);

                while (currentLoad + nextElem < m)
                {
                    currentLoad += nextElem;
                    input.Add(nextElem);
                    container.Add(input.Count() - 1);
                    nextElem = random.Next(1, m);
                }
                var lastE = m - container.Select(c => input[c]).Sum();
                input.Add(lastE);
                container.Add(input.Count() - 1);
                

                result.Add(container);
            }

            output.WriteLine("input: {0}", string.Join(", ", input));
            for(int i = 0; i < result.Count(); i++)
            {
                var line = result[i];
                output.WriteLine(string.Join(", ", line));
            }

        }
    }
}
