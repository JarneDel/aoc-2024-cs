
using System.Net.Mime;
using aoc;
using aoc.day2;
using aoc.day3;
using aoc.day4;
using aoc.day5;


const string basePath = "day";
const string extension = ".txt";
Console.WriteLine(await Day1.RunAsync(basePath + 1 + extension ));


Console.WriteLine(await Day2.Part1CalculationWithStreamReader(basePath + 2 + extension));
Console.WriteLine(await Day2.Part2Calculation(basePath + 2 + extension));

Console.WriteLine(Day3.Part1CalculationFile(basePath + 3 + extension));
Console.WriteLine(Day3.Part2CalculationFile(basePath + 3 + extension));
var day4 = new Day4(basePath + 4 + extension);
Console.WriteLine(day4.Part1Count);
Console.WriteLine(day4.Part2Count);
var day5 = new Day5(basePath + 5 + extension);
Console.WriteLine(day5.Part1Result);
Console.WriteLine(day5.Part2Result);