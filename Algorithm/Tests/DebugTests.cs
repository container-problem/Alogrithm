using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Algorithm.Tests
{
    public class DebugTests
    {
        private readonly ITestOutputHelper output;
        private readonly Random random;

        public DebugTests(ITestOutputHelper output)
        {
            random = new Random();
            this.output = output;
        }

        [Fact]
        public void TestExact()
        {
            var m = 3;
            var containersCount = 3;

            var (input, expectedResult) = GetSolution(m, containersCount);

            output.WriteLine("input: {0}", string.Join(", ", input));
            for(int i = 0; i < expectedResult.Count(); i++)
            {
                var line = expectedResult[i];
                output.WriteLine(string.Join(", ", line));
            }

            var testResult = Algorithms.ExactAlg(input.Length, m, input);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void TestFFAlg()
        {
            var m = 5;
            var n = 10;

            var input = new int[n];
            
            for(int i = 0; i < n; i++)
            {
                input[i] = random.Next(1, m);
            }

            output.WriteLine("input: " + string.Join(", ", input));

            var result = Algorithms.FFAlg(n, m, input);

            output.WriteLine("output: ");
            for (int i = 0; i < result.Count(); i++)
            {
                output.WriteLine(string.Join(", ", result[i]));
            }


                var resultSum = result.SelectMany(r => r).Select(r => input[r]).Sum();
            var inputSum = input.Sum();

            Assert.Equal(inputSum, resultSum);
            output.WriteLine("Сумма масс элементов во всех контейнерах равна сумме масс всех элементов входных данных");

            for (int i = 0; i < result.Count(); i++)
            {
                var sum = result[i].Select(r => input[r]).Sum();
                Assert.NotEqual(0, sum);
                Assert.True(sum <= m);
            }
            output.WriteLine("Сумма масс эл. каждого контейнера не превышает M и не равна 0");
        }

        [Fact]
        public void TestFFSAlg()
        {
            var m = 5;
            var n = 10;

            var input = new int[n];

            for (int i = 0; i < n; i++)
            {
                input[i] = random.Next(1, m);
            }

            output.WriteLine("input: " + string.Join(", ", input));

            var result = Algorithms.FFSAlg(n, m, input);

            output.WriteLine("output: ");
            for (int i = 0; i < result.Count(); i++)
            {
                output.WriteLine(string.Join(", ", result[i]));
            }


            var resultSum = result.SelectMany(r => r).Select(r => input[r]).Sum();
            var inputSum = input.Sum();

            Assert.Equal(inputSum, resultSum);
            output.WriteLine("Сумма масс элементов во всех контейнерах равна сумме масс всех элементов входных данных");

            for (int i = 0; i < result.Count(); i++)
            {
                var sum = result[i].Select(r => input[r]).Sum();
                Assert.NotEqual(0, sum);
                Assert.True(sum <= m);
            }
            output.WriteLine("Сумма масс эл. каждого контейнера не превышает M и не равна 0");
        }

        private (int[] input, List<List<int>> result) GetSolution(int m, int containersCount)
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
    }
}
