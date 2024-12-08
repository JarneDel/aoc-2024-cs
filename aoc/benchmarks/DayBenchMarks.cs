using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;
using aoc.day2;
using aoc.day3;
using aoc.day4;
using aoc.day5;
using aoc.day6;
using BenchmarkDotNet.Engines;

namespace aoc.benchmarks
{
    [MaxIterationCount(10)]
    [InvocationCount(5)]
    [SimpleJob(RunStrategy.Throughput)]

    public class DayBenchmarks
    {
        private const string BasePath = "./inputs/day";
        private const string Extension = ".txt";

        [Benchmark]
        public async Task<string> Day1Benchmark()
        {
            return await Day1.RunAsync(BasePath + 1 + Extension);
        }

        [Benchmark]
        public async Task<int> Day2Part1Benchmark()
        {
            return await Day2.Part1CalculationWithStreamReader(BasePath + 2 + Extension);
        }

        [Benchmark]
        public async Task<int> Day2Part2Benchmark()
        {
            return await Day2.Part2Calculation(BasePath + 2 + Extension);
        }

        [Benchmark]
        public int Day3Part1Benchmark()
        {
            return Day3.Part1CalculationFile(BasePath + 3 + Extension);
        }

        [Benchmark]
        public int Day3Part2Benchmark()
        {
            return Day3.Part2CalculationFile(BasePath + 3 + Extension);
        }

        [Benchmark]
        public int Day4Part1Benchmark()
        {
            var day4 = new Day4(BasePath + 4 + Extension);
            day4.FindXmasCount();
            return day4.Part1Count;
        }

        [Benchmark]
        public int Day4Part2Benchmark()
        {
            var day4 = new Day4(BasePath + 4 + Extension);
            day4.FindPart2Count();
            return day4.Part2Count;
        }

        [Benchmark]
        public int Day5Part1Benchmark()
        {
            var day5 = new Day5(BasePath + 5 + Extension);
            return day5.CalculatePart2();
        }

        [Benchmark]
        public int Day5Part2Benchmark()
        {
            var day5 = new Day5(BasePath + 5 + Extension);
            return day5.CalculatePart2();
        }
        
        [Benchmark]
        public int Day6Part1Benchmark()
        {
            var day6 = new Day6(BasePath + 6 + Extension);
            day6.Part1();
            return day6.VisitedLocationCount;
        }
        
        [Benchmark]
        public int Day6Part2Benchmark()
        {
            var day6 = new Day6(BasePath + 6 + Extension);
            day6.Part2();
            return day6.AmountOfLoops;
        }
        
    }
}