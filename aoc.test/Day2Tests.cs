using aoc.day2;

namespace aoc.test;

[TestFixture]
public class Day2Tests
{
    private readonly string[] _input =
    {
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9"
    };

    [Test]
    public void Part1Shouldpass()
    {
        Assert.That(Day2.Part1Calculation(_input), Is.EqualTo(2));
    }

    [Test]
    public void IncreaseToHight_1()
    {
        Assert.That(Day2.Part1Calculation(new[] { "1 2 7 8 9" }), Is.EqualTo(0));
    }

    [Test]
    public void IncreaseToHight_2()
    {
        Assert.That(Day2.Part1Calculation(new[] { "7 6 4 2 1" }), Is.EqualTo(1));
    }

    [Test]
    public void IncreaseToHight_3()
    {
        Assert.That(Day2.Part1Calculation(new[] { "9 7 6 2 1" }), Is.EqualTo(0));
    }

    [Test]
    public void IncreaseToHight_4()
    {
        Assert.That(Day2.Part1Calculation(new[] { "1 3 2 4 5" }), Is.EqualTo(0));
    }

    [Test]
    public void IncreaseToHight_5()
    {
        Assert.That(Day2.Part1Calculation(new[] { "8 6 4 4 1" }), Is.EqualTo(0));
    }

    [Test]
    public void IncreaseToHight_6()
    {
        Assert.That(Day2.Part1Calculation(new[] { "1 3 6 7 9" }), Is.EqualTo(1));
    }
    
    [Test]
    public void Part2ShouldPass()
    {
        Assert.Multiple( () =>
        {
            Assert.That(Day2.Part2Calculation(new[] { "7 6 4 2 1" }), Is.EqualTo(1));
            Assert.That( Day2.Part2Calculation(new[] { "1 2 7 8 9" }), Is.EqualTo(0));
            Assert.That( Day2.Part2Calculation(new[] { "9 7 6 2 1" }), Is.EqualTo(0));
            Assert.That( Day2.Part2Calculation(new[] { "1 3 2 4 5" }), Is.EqualTo(1));
            Assert.That( Day2.Part2Calculation(new[] { "8 6 4 4 1" }), Is.EqualTo(1));
            Assert.That( Day2.Part2Calculation(new[] { "1 3 6 7 9" }), Is.EqualTo(1));
        });
    }
}