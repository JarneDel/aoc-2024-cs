
using System.Net.Mime;
using aoc;
using aoc.day2;


const string basePath = "day";
const string extension = ".txt";
Console.WriteLine(await Day1.RunAsync(basePath + 1 + extension ));


Console.WriteLine(await Day2.Part1CalculationWithStreamReader(basePath + 2 + extension));
Console.WriteLine(await Day2.Part2Calculation(basePath + 2 + extension));