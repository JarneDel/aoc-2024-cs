using System.Text;
using SixLabors.ImageSharp.ColorSpaces;

namespace aoc.day9;

public class Day9
{
    private readonly string _diskmap;
    public  string Blocks = string.Empty;
    public string OptimizedBlocks = string.Empty;
    private readonly List<FileBlock> _fileData = new();
    public List<FileBlock> OptimizedFileData { get; private set; } = new();

    public Day9(string? input, string? filename = null)
    {
        _diskmap = input ?? File.ReadAllText(filename ?? throw new ArgumentNullException(nameof(filename)));
    }

    public Day9 ConvertToBlocks()
    {
        StringBuilder stringBuilder = new();
        char index = '0';
        short indexNumber = 0;
        for (int i = 0; i < _diskmap.Length; i++)
        {
            char c = _diskmap[i];
            short count = (short)(c - '0');
            bool isEven = i % 2 == 0;
            if (isEven)
            {
                stringBuilder.Append(index, count);
                _fileData.Add(new FileBlock(indexNumber, stringBuilder.Length, count));
                index++;
                indexNumber++;
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
        List<FreeSpace> freeSpaces = GetFreeSpaceSpans();

        // Sort files by descending ID
        _fileData.Sort((a, b) => b.Id.CompareTo(a.Id));

        for (int i = 0; i < _fileData.Count; i++)
        {
            var file = _fileData[i];

            foreach (var space in freeSpaces)
            {
                if (space.Length >= file.Length)
                {
                    // Create a new updated struct
                    _fileData[i] = file.WithStart(space.Start);

                    // Update free space list
                    UpdateFreeSpaces(freeSpaces, space, file.Length);
                    break;
                }
            }
        }

        OptimizedFileData = new List<FileBlock>(_fileData);
        return this;
    }
    
    private List<FreeSpace> GetFreeSpaceSpans()
    {
        List<FreeSpace> freeSpaces = new();

        int start = 0;
        for (int i = 0; i < _diskmap.Length;)
        {
            if (_fileData.Exists(f => f.Start == i))
            {
                var block = _fileData.First(f => f.Start == i);
                i += block.Length;
                start = i;
            }
            else
            {
                int spaceStart = i;
                while (i < _diskmap.Length && !_fileData.Exists(f => f.Start == i))
                {
                    i++;
                }
                freeSpaces.Add(new FreeSpace(spaceStart, i - spaceStart));
            }
        }

        return freeSpaces;
    }

    private void UpdateFreeSpaces(List<FreeSpace> freeSpaces, FreeSpace space, int usedLength)
    {
        freeSpaces.Remove(space);

        if (usedLength < space.Length)
        {
            freeSpaces.Add(new FreeSpace(space.Start + usedLength, space.Length - usedLength));
        }

        freeSpaces.Sort((a, b) => a.Start.CompareTo(b.Start)); // Maintain order
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
    
    public long CalculateChecksumPart2()
    {
        long product = 0;

        foreach (var file in OptimizedFileData)
        {
            for (int i = 0; i < file.Length; i++)
            {
                product += (file.Id - '0') * (file.Start + i);
            }
        }

        return product;
    }
}


public readonly struct FileBlock
{
    public short Id { get; }
    public int Start { get; }
    public int Length { get; }

    public FileBlock(short id, int start, int length)
    {
        Id = id;
        Start = start;
        Length = length;
    }

    public FileBlock WithStart(int newStart) => new FileBlock(Id, newStart, Length);
}


public struct FreeSpace(int start, int length) : IEquatable<FreeSpace>
{
    public int Start { get; set; } = start;
    public int Length { get; set; } = length;

    public bool Equals(FreeSpace other)
    {
        return Start == other.Start && Length == other.Length;
    }

    public override bool Equals(object? obj)
    {
        return obj is FreeSpace other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, Length);
    }

    public static bool operator ==(FreeSpace left, FreeSpace right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(FreeSpace left, FreeSpace right)
    {
        return !(left == right);
    }
}