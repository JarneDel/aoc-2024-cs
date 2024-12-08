using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;
using aoc.day2;
using aoc.day3;
using aoc.day4;
using aoc.day5;
using aoc.day6;
using aoc.day7;
using aoc.day8;
using BenchmarkDotNet.Engines;

namespace aoc.benchmarks
{
    [MaxIterationCount(20)]
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
            Day4 day4 = new(BasePath + 4 + Extension);
            day4.FindXmasCount();
            return day4.Part1Count;
        }

        [Benchmark]
        public int Day4Part2Benchmark()
        {
            Day4 day4 = new(BasePath + 4 + Extension);
            day4.FindPart2Count();
            return day4.Part2Count;
        }

        [Benchmark]
        public int Day5Part1Benchmark()
        {
            Day5 day5 = new(BasePath + 5 + Extension);
            return day5.CalculatePart2();
        }

        [Benchmark]
        public int Day5Part2Benchmark()
        {
            Day5 day5 = new(BasePath + 5 + Extension);
            return day5.CalculatePart2();
        }
        
        [Benchmark]
        public int Day6Part1Benchmark()
        {
            Day6 day6 = new(BasePath + 6 + Extension);
            day6.Part1();
            return day6.VisitedLocationCount;
        }
        
        [Benchmark]
        public int Day6Part2Benchmark()
        {
            Day6 day6 = new(BasePath + 6 + Extension);
            day6.Part2();
            return day6.AmountOfLoops;
        }
        
        [Benchmark]
        public long Day7Part1Benchmark()
        {
            Day7 day7 = new(BasePath + 7 + Extension);
            return day7.Part1();
        }
        
        [Benchmark]
        public long Day7Part2Benchmark()
        {
            Day7 day7 = new(BasePath + 7 + Extension);
            return day7.Part2();
        }
        
        [Benchmark]
        public long Day8Part1Benchmark()
        {
            Day8 day8 = new(BasePath + 8 + Extension);
            return day8.Part1();
        }
        
        [Benchmark]
        public int Day8Part2Benchmark()
        {
            Day8 day8 = new(BasePath + 8 + Extension);
            return day8.Part2();
        }
        
        
    }
}