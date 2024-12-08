using aoc;
using aoc.benchmarks;
using aoc.day2;
using aoc.day3;
using aoc.day4;
using aoc.day5;
using aoc.day6;
using BenchmarkDotNet.Running;
using System.Diagnostics;
using aoc.day7;
using aoc.day8;

const string basePath = "./inputs/day";
const string extension = ".txt";

async Task LogExecutionTimeAsync(int day, int part, Func<Task<int>> func)
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    int result = await func();
    stopwatch.Stop();
    Console.WriteLine($"day{day}, part{part} ({stopwatch.ElapsedMilliseconds}ms): {result}");
}

async Task LogExecutionTimeAsyncString(int day, int part, Func<Task<string>> func)
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    string result = await func();
    stopwatch.Stop();
    Console.WriteLine($"day{day}, part{part} ({stopwatch.ElapsedMilliseconds}ms): {result}");
}

void LogExecutionTime(int day, int part, Func<int> func)
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    int result = func();
    stopwatch.Stop();
    Console.WriteLine($"day{day}, part{part} ({stopwatch.ElapsedMilliseconds}ms): {result}");
}

void LogExecutionTimeLong(int day, int part, Func<long> func)
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    long result = func();
    stopwatch.Stop();
    Console.WriteLine($"day{day}, part{part} ({stopwatch.ElapsedMilliseconds}ms): {result}");
}

await LogExecutionTimeAsyncString(1, 1, async () => await Day1.RunAsync(basePath + 1 + extension));

await LogExecutionTimeAsync(2, 1, async () => await Day2.Part1CalculationWithStreamReader(basePath + 2 + extension));
await LogExecutionTimeAsync(2, 2, async () => await Day2.Part2Calculation(basePath + 2 + extension));

LogExecutionTime(3, 1, () => Day3.Part1CalculationFile(basePath + 3 + extension));
LogExecutionTime(3, 2, () => Day3.Part2CalculationFile(basePath + 3 + extension));

Day4 day4 = new(basePath + 4 + extension);
LogExecutionTime(4, 1, () => {
    day4.FindXmasCount();
    return day4.Part1Count;
});
LogExecutionTime(4, 2, () => {
    day4.FindPart2Count();
    return day4.Part2Count;
});

Day5 day5 = new(basePath + 5 + extension);
LogExecutionTime(5, 1, () => day5.CalculatePart1());
LogExecutionTime(5, 2, () => day5.CalculatePart2());

Day6 day6 = new(basePath + 6 + extension);
LogExecutionTime(6, 1, () => {
    day6.Part1();
    return day6.VisitedLocationCount;
});
#if DEBUG
new MapVisualizer(day6.Map).Save("./day6.jpg");
#endif
LogExecutionTime(6, 2, () => {
    day6.Part2();
    return day6.AmountOfLoops;
});

Day7 day7 = new(basePath + 7 + extension);
LogExecutionTimeLong(7,1, () => day7.Part1());
LogExecutionTimeLong(7,2, () => day7.Part2());

Day8 day8 = new(basePath + 8 + extension);
LogExecutionTime(8,1, () => day8.Part1());
LogExecutionTime(8,1, () => day8.Part2());


// only when release build
#if !DEBUG
BenchmarkRunner.Run<DayBenchmarks>();
#endif