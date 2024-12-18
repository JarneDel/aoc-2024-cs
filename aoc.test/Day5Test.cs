using aoc.day5;

namespace aoc.test;

public class Day5Test
{
    private readonly string[] _rules =
    [
        "47|53",
        "97|13",
        "97|61",
        "97|47",
        "75|29",
        "61|13",
        "75|53",
        "29|13",
        "97|29",
        "53|29",
        "61|53",
        "97|53",
        "61|29",
        "47|13",
        "75|47",
        "97|75",
        "47|61",
        "75|61",
        "47|29",
        "75|13",
        "53|13"
    ];
    
    private readonly string[] _data =
    [
        "75,47,61,53,29",
        "97,61,53,29,13",
        "75,29,13",
        "75,97,47,61,53",
        "61,13,29",
        "97,13,75,29,47"
    ];

    [Test]
    public void Day5Part1()
    {
        Day5? day5 = new(_rules, _data);
        Assert.That(day5.CalculatePart1(), Is.EqualTo(143));
    }


    [Test]
    public void Day5Part2()
    {
        Day5? day5 = new(_rules, _data);
        Assert.That(day5.CalculatePart2(), Is.EqualTo(123));
    }
    
}