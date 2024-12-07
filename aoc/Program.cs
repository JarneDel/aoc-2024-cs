using aoc;
using aoc.benchmarks;
using aoc.day2;
using aoc.day3;
using aoc.day4;
using aoc.day5;
using BenchmarkDotNet.Running;

const string basePath = "./inputs/day";
const string extension = ".txt";
Console.WriteLine(await Day1.RunAsync(basePath + 1 + extension));

Console.WriteLine(await Day2.Part1CalculationWithStreamReader(basePath + 2 + extension));
Console.WriteLine(await Day2.Part2Calculation(basePath + 2 + extension));

Console.WriteLine(Day3.Part1CalculationFile(basePath + 3 + extension));
Console.WriteLine(Day3.Part2CalculationFile(basePath + 3 + extension));
var day4 = new Day4(basePath + 4 + extension);
day4.FindPart2Count();
day4.FindXmasCount();
Console.WriteLine(day4.Part1Count);
Console.WriteLine(day4.Part2Count);
var day5 = new Day5(basePath + 5 + extension);
Console.WriteLine(day5.CalculatePart1());
Console.WriteLine(day5.CalculatePart2());

BenchmarkRunner.Run<DayBenchmarks>();