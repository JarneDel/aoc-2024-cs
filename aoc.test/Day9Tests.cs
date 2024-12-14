using aoc.day9;

namespace aoc.test;

public class Day9Tests
{
    private readonly string _diskmap = "2333133121414131402;";

    [Test]
    public void ConvertToBlocks()
    {
        var result = "00...111...2...333.44.5555.6666.777.888899";
        var testmap = "12345";
        var result2 = "0..111....22222";
        var day9 = new Day9(_diskmap);
        var blocks = day9.ConvertToBlocks().Blocks;
        Assert.That(blocks, Is.EqualTo(result));
        Assert.That(new Day9(testmap).ConvertToBlocks().Blocks, Is.EqualTo(result2));
    }

    [Test]
    public void Optimize()
    {
        var result = "0099811188827773336446555566..............";
        var day9 = new Day9(_diskmap);
        var optimized = day9.ConvertToBlocks().Optimize().OptimizedBlocks;
        Assert.That(optimized, Is.EqualTo(result));
    }

    [Test]
    public void Part1()
    {
        var day9 = new Day9(_diskmap);
        long checksum = day9.ConvertToBlocks().Optimize().CalculateChecksum();
        Assert.That(checksum, Is.EqualTo(1928));
    }

    [Test]
    public void Part2()
    {
        var day9 = new Day9(_diskmap);
        long checksum = day9.ConvertToBlocks().OptimizePart2().CalculateChecksumPart2();
        Assert.That(checksum, Is.EqualTo(2858));
    }
}