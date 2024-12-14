using System.Text;

namespace aoc.day9;

public class Day9
{
    private readonly string _diskmap;
    public  string Blocks = string.Empty;
    public string OptimizedBlocks = string.Empty;

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