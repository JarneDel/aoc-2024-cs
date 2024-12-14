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
using aoc.day9;

// --no-benchmark argument (not specifically on args[0] but anywhere in the arguments)
bool noBenchmark = args.Length > 0 && args.Any(arg => arg == "--no-benchmark");
// --day argument
int dayArg = args.Length > 0 && args.Any(arg => arg == "--day") ? int.Parse(args[Array.IndexOf(args, "--day") + 1]) : 0;
// --part argument
int partArg = args.Length > 0 && args.Any(arg => arg == "--part") ? int.Parse(args[Array.IndexOf(args, "--part") + 1]) : 0;
Console.WriteLine($"noBenchmark: {noBenchmark}, dayArg: {dayArg}, partArg: {partArg}");

const string basePath = "./inputs/day";
const string extension = ".txt";

bool ShouldExecute(int day, int part) => dayArg == 0 || (dayArg == day && (partArg == 0 || partArg == part));
async Task ExecuteAsync<T>(int day, int part, Func<Task<T>> func)
{
    if (!ShouldExecute(day, part)) return;
    Stopwatch stopwatch = Stopwatch.StartNew();
    T result = await func();
    stopwatch.Stop();
    Console.WriteLine($"day{day}, part{part} ({stopwatch.ElapsedMilliseconds}ms): {result}");
}

void Execute<T>(int day, int part, Func<T> func)
{
    if (!ShouldExecute(day, part)) return;
    Stopwatch stopwatch = Stopwatch.StartNew();
    T result = func();
    stopwatch.Stop();
    Console.WriteLine($"day{day}, part{part} ({stopwatch.ElapsedMilliseconds}ms): {result}");
}
await ExecuteAsync(1, 1, async () => await Day1.RunAsync(basePath + 1 + extension));

await ExecuteAsync(2, 1, async () => await Day2.Part1CalculationWithStreamReader(basePath + 2 + extension));
await ExecuteAsync(2, 2, async () => await Day2.Part2Calculation(basePath + 2 + extension));

Execute(3, 1, () => Day3.Part1CalculationFile(basePath + 3 + extension));
Execute(3, 2, () => Day3.Part2CalculationFile(basePath + 3 + extension));

Day4 day4 = new(basePath + 4 + extension);
Execute(4, 1, () => {
    day4.FindXmasCount();
    return day4.Part1Count;
});
Execute(4, 2, () => {
    day4.FindPart2Count();
    return day4.Part2Count;
});

Day5 day5 = new(basePath + 5 + extension);
Execute(5, 1, () => day5.CalculatePart1());
Execute(5, 2, () => day5.CalculatePart2());

Day6 day6 = new(basePath + 6 + extension);
Execute(6, 1, () => {
    day6.Part1();
    return day6.VisitedLocationCount;
});
#if DEBUG
new MapVisualizer(day6.Map).Save("./day6.jpg");
#endif
Execute(6, 2, () => {
    day6.Part2();
    return day6.AmountOfLoops;
});

Day7 day7 = new(basePath + 7 + extension);
Execute(7,1, () => day7.Part1());
Execute(7,2, () => day7.Part2());

Day8 day8 = new(basePath + 8 + extension);
Execute(8,1, () => day8.Part1());
Execute(8,1, () => day8.Part2());

Day9 day9 = new(null,basePath + 9 + extension);
Execute(9,1, () => day9.ConvertToBlocks().Optimize().CalculateChecksum());

if (noBenchmark) Process.GetCurrentProcess().Kill();
// only when release build
#if !DEBUG
BenchmarkRunner.Run<DayBenchmarks>();
#endif