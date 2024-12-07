using System.Threading.Tasks.Dataflow;

namespace aoc.day4;

public class Day4
{
    private readonly int _rows;
    private readonly int _cols;
    private const string Word = "XMAS";
    private readonly int _wordLenght = Word.Length;
    private readonly List<string> _grid;

    private readonly int[,] _directions = new int[,]
    {
        { 0, 1 }, // Right
        { 0, -1 }, // Left
        { 1, 0 }, // Down
        { -1, 0 }, // Up
        { 1, 1 }, // Diagonal down-right
        { -1, -1 }, // Diagonal up-left
        { 1, -1 }, // Diagonal down-left
        { -1, 1 } // Diagonal up-right
    };

    public int Part1Count { get; set; } = 0;
    public int Part2Count { get; set; } = 0;

    public Day4(string Filename)
    {
        var grid = File.ReadAllLines(Filename).ToList();
        _rows = grid.Count;
        _cols = grid[0].Length;
        _grid = grid;
    }

    public Day4(List<string> grid)
    {
        _rows = grid.Count;
        _cols = grid[0].Length;
        _grid = grid;
    }


    bool IsValid(int r, int c)
    {
        return r >= 0 && r < _rows && c >= 0 && c < _cols;
    }


    private bool SearchFromPosition(int row, int col, int direction, int directionCol)
    {
        for (int i = 0; i < _wordLenght; i++)
        {
            int nextRow = row + i * direction;
            int nextCol = col + i * directionCol;
            if (!IsValid(nextRow, nextCol) || _grid[nextRow][nextCol] != Word[i])
            {
                return false;
            }
        }

        return true;
    }


    public void FindXmasCount()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                if (_grid[row][col] != Word[0]) continue;
                for (int d = 0; d < _directions.GetLength(0); d++)
                {
                    if (!SearchFromPosition(row, col, _directions[d, 0], _directions[d, 1])) continue;
                    Part1Count++;
                }
            }
        }
    }


    bool IsMasOrSam(char a, char b, char c)
    {
        return (a == 'M' && b == 'A' && c == 'S') || (a == 'S' && b == 'A' && c == 'M');
    }

    public void FindPart2Count()
    {
        Part2Count += Enumerable.Range(1, _rows - 2).
            SelectMany(row => Enumerable.Range(1, _cols - 2)
                .Where(col => _grid[row][col] == 'A' && IsMasOrSam(_grid[row - 1][col - 1], 'A', _grid[row + 1][col + 1]) &&
                              IsMasOrSam(_grid[row - 1][col + 1], 'A', _grid[row + 1][col - 1])))
            .Count();
    }
}