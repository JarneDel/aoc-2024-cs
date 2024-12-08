using aoc.day8;

namespace aoc.test;

public class Day8Tests
{
    private readonly string[] _input =
    [
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............"
    ];

    [Test]
    public void Day8Part1()
    {
        Day8 day8 = new(_input);
        int result = day8.Part1();
        Assert.That(result, Is.EqualTo(14));
    }        
    
    [Test]
    public void Day8Part2()
    {
        Day8 day8 = new(_input);
        int result = day8.Part2();
        Assert.That(result, Is.EqualTo(34));
    }    
    
    private readonly string[] _input3 =
    {
        "T.........",
        "...T......",
        ".T........",
        "..........",
        "..........",
        "..........",
        "..........",
        "..........",
        "..........",
        ".........."
    };
    
    [Test]
    public void Day8Part2_2()
    {
        Day8 day8 = new(_input3);
        int result = day8.Part2();
        Assert.That(result, Is.EqualTo(9));
        
    }
    
    
    
    private readonly string[] _input2 =
    [
        "..x.x.......",
        "............",
        "............",
        "............",
        "............",
        "............",
        "............",
        "............",
        "............",
        "............",
        "............",
        "............"
    ];

    [Test]
    public void Day8Part1_2()
    {
        Day8 day8 = new(_input2);
        int result = day8.Part1();
        Assert.That(result, Is.EqualTo(2));
    }
}