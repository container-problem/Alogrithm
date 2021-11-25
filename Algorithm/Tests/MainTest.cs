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
            var m = random.Next(1, 1000);
            var containersCount = random.Next(1, 1000);

            var (input, expectedResult) = GetFFTestData(m, containersCount);

            output.WriteLine("input: {0}", string.Join(", ", input));
            for(int i = 0; i < expectedResult.Count(); i++)
            {
                var line = expectedResult[i];
                output.WriteLine(string.Join(", ", line));
            }

            var testResult = Algorithms.FF(input.Length, m, input);
            Assert.Equal(expectedResult, testResult);

        }

        private (int[] input, List<List<int>> result) GetFFTestData(int m, int containersCount)
        {
            var input = new List<int>();
            var result = new List<List<int>>();

            for (int i = 0; i < containersCount; i++)
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
                var lastE = m - currentLoad;
                input.Add(lastE);
                container.Add(input.Count() - 1);


                result.Add(container);
            }
            return (input.ToArray(), result);
        }

        private (int[] input, List<List<int>> result) GetFFSTestData(int m, int containersCount)
        {
            var input = new List<int>();
            var result = new List<List<int>>();

            var sort = new List<int>();
            for(int i = 0; i < m * containersCount; i++)
            {
                sort.Add(random.Next(1, m));
            }

            sort.Sort();
            int current = 0;

            for (int i = 0; i < containersCount; i++)
            {
                List<int> container = new List<int>();

                int currentLoad = 0;
                int nextElem = sort[++current];

                while (currentLoad + nextElem < m)
                {
                    currentLoad += nextElem;
                    input.Add(nextElem);
                    container.Add(input.Count() - 1);
                    nextElem = sort[++current];
                }

                result.Add(container);
            }

            return (input.ToArray(), result);
        }
    }
}
