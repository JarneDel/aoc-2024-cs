using aoc.day4;

namespace aoc.test;


[TestFixture]
public class Day4Tests
{
    private readonly List<string> _grid =
    [
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX"
    ];
    
    
    [Test]
    public void FindXmasCount()
    {
        Day4? day4 = new(_grid);
        day4.FindXmasCount();
        Assert.That(day4.Part1Count, Is.EqualTo(18));
    }


    [Test]
    public void FindXmasCountSimple()
    {
        List<string> simpList = new()
        {
            "XMAS",
            "MXMA",
            "AMXM",
            "SAMX"
        };
        Day4? day4 = new(simpList);
        day4.FindXmasCount();
        Assert.That(day4.Part1Count, Is.EqualTo(4));
    }
    
    [Test]
    public void FindXmasCountDiagonal()
    {
        List<string> simpList = new()
        {
            "X..S",
            ".MA.",
            ".MA.",
            "X..S"
        };
        Day4? day4 = new(simpList);
        day4.FindXmasCount();
        Assert.That(day4.Part1Count, Is.EqualTo(2));
    }
    
    [Test]
    public void FindXmasCountDiagonal2()
    {
        List<string> simpList = new()
        {
            "X..X",
            ".MM.",
            ".AA.",
            "S..S"
        };
        Day4? day4 = new(simpList);
        day4.FindXmasCount();
        Assert.That(day4.Part1Count, Is.EqualTo(2));
    }
    [Test]
    public void FindXmasCountDiagonalInverse()
    {
        List<string> simpList = new()
        {
            "S..S",
            ".AA.",
            ".MM.",
            "X..X"
        };
        Day4? day4 = new(simpList);
        day4.FindXmasCount();
        Assert.That(day4.Part1Count, Is.EqualTo(2));
    }
    
    
    [Test]
    public void FindPart2Count()
    {
        Day4? day4 = new(_grid);
        day4.FindPart2Count();
        Assert.That(day4.Part2Count, Is.EqualTo(9));
    }
}