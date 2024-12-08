using aoc.day6;

namespace aoc.test;

public class Day6Tests
{
    private readonly List<string> _map =
    [
        "....#.....",
        ".........#",
        "..........",
        "..#.......",
        ".......#..",
        "..........",
        ".#..^.....",
        "........#.",
        "#.........",
        "......#..."
    ];
    
    
    
    [Test]
    public void Day6Part1()
    {
        Day6? day6 = new(_map);
        day6.Part1();
        Assert.That(day6.VisitedLocationCount, Is.EqualTo(41));
    }
    
    
    [Test]
    public void Day6Part2()
    {
        Day6? day6 = new(_map);
        day6.Part2();
        Assert.That(day6.AmountOfLoops, Is.EqualTo(6));
    }
    
    [Test]
    public void Day6Part2WithFullMap()
    {
        Day6? day6 = new("./inputs/day6.txt");
        
        Assert.That(day6.Part2(), Is.EqualTo(1911));
    }
}