using System.Text;
using SixLabors.ImageSharp.ColorSpaces;

namespace aoc.day9;

public class Day9
{
    private readonly string _diskmap;
    public  string Blocks = string.Empty;
    public string OptimizedBlocks = string.Empty;
    public List<Block> FileSystem = [];

    public Day9(string? input, string? filename = null)
    {
        _diskmap = input ?? File.ReadAllText(filename ?? throw new ArgumentNullException(nameof(filename)));
    }

    public Day9 ConvertToBlocks()
    {
        StringBuilder stringBuilder = new();
        char index = '0';
        for (int i = 0; i < _diskmap.Length; i++)
        {
            char c = _diskmap[i];
            short count = (short)(c - '0');
            bool isEven = i % 2 == 0;
            if (isEven)
            {
                stringBuilder.Append(index, count);
                index++;
            }
            else if (i != _diskmap.Length - 1)
            {
                stringBuilder.Append('.', count);
            }
        }

        Blocks = stringBuilder.ToString();
        return this;
    }
    
    public Day9 OptimizePart2()
    {
        char[] blocks = Blocks.ToCharArray();
        StringBuilder stringBuilder = new();
        int reverseIndex = blocks.Length - 1;
        bool isFirstPass = true;
        for (int i = 0; i < blocks.Length;)
        {
            if (reverseIndex < i)
            {
                i = 0;
            }

            if (reverseIndex == 0)
            {
                break;
            }

            char c = blocks[i];
            if (c != '.')
            {
                if (isFirstPass) stringBuilder.Append(c);
                i++;
                continue;
            }

            short freeSpace = 0;
            while (blocks[i + freeSpace] == '.')
            {
                freeSpace++;
            }
            Console.WriteLine($"Free space: {freeSpace}");

            AppendBlock(stringBuilder, ref i, ref reverseIndex, freeSpace, ref blocks);
        }
        
        // add remainder of the blocks to the string starting at reverseIndex
        stringBuilder.Append(blocks, reverseIndex + 1, blocks.Length - reverseIndex - 1);
        OptimizedBlocks = stringBuilder.ToString();
        Console.WriteLine(OptimizedBlocks);
        return this;
    }

    private void AppendBlock(StringBuilder stringBuilder, ref int i, ref int reverseIndex, int freeSpace, ref char[] blocks)
    {
        while (true)
        {
            if (reverseIndex < 0)
            {
                break;
            }
            
            if (blocks[reverseIndex] == '.')
            {
                reverseIndex--;
                continue;
            }
            int blockLength = 1;
            while (reverseIndex - blockLength >= 0 && blocks[reverseIndex - blockLength] == blocks[reverseIndex])
            {
                blockLength++;
            }
            Console.WriteLine($"Block length: {blockLength}");

            if (blockLength <= freeSpace)
            {
                Console.WriteLine($"Appending {blocks[reverseIndex]} {blockLength} times with {freeSpace - blockLength} free space");
                stringBuilder.Append(blocks[reverseIndex], blockLength);
                stringBuilder.Append('.', freeSpace - blockLength);
                // replace the block with dots
                for (int j = 0; j < blockLength; j++)
                {
                    blocks[reverseIndex - j] = '.';
                }
                reverseIndex -= blockLength;
                i += freeSpace;
                Console.WriteLine($"Moved I to {i} and reverseIndex to {reverseIndex}");
                break;
            }
            
            reverseIndex -= blockLength;
        }
    }

    public Day9 Optimize()
    {
        StringBuilder stringBuilder = new();
        int reverseIndex = Blocks.Length - 1;
        for (int i = 0; i < Blocks.Length; i++)
        {
            if (reverseIndex < i)
            {
                break;
            }

            char c = Blocks[i];
            if (c != '.')
            {
                stringBuilder.Append(c);
                continue;
            }

            while (Blocks[reverseIndex] == '.' && reverseIndex > i)
            {
                reverseIndex--;
            }
            stringBuilder.Append(Blocks[reverseIndex]);
            reverseIndex--;
        }
        stringBuilder.Append('.', Blocks.Length - stringBuilder.Length);
        OptimizedBlocks = stringBuilder.ToString();
        return this;
    }

    public long CalculateChecksum()
    {
        long product = 0;
        for (int index = 0; index < OptimizedBlocks.Length; index++)
        {
            char c = OptimizedBlocks[index];
            if (c == '.')
            {
                return product;
            }
            short number = (short)(c - '0');
            product += number * index;
        }

        return product;
    }
}


public struct Block(char id, int start, int length)
{
    public char Id { get; } = id;
    public int Start { get; } = start;
    public int Length { get; } = length;
}