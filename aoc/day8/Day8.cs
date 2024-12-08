using aoc.day6;

namespace aoc.day8;

public class Day8
{

    public Node[,] _map;
    private HashSet<char> _antennaTypes = [];

    public Day8(string[] input)
    {
        _map = ParseInput(input);
    }
    
    public Day8(string filename)
    {
        _map = ParseInput(File.ReadAllLines(filename));
    }

    private Node[,] ParseInput(string[] input)
    {
        Node[,] map = new Node[input.Length, input[0].Length];
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                char character = line[j];
                if (character != '.')
                {
                    _antennaTypes.Add(character);
                }
                map[i, j] = new Node(character);
            }
        }

        return map;
    }


    public int Part1()
    {
        foreach (char antennaType in _antennaTypes)
        {
            // find antennas on the map
            List<Vector2Int> antennas = Enumerable.Range(0, _map.GetLength(0))
                .SelectMany(i => Enumerable.Range(0, _map.GetLength(1))
                    .Where(j => _map[i, j].Antenna == antennaType)
                    .Select(j => new Vector2Int(i, j)))
                .ToList();
            
            // create antinodes
            foreach (Vector2Int antenna in antennas)
            {
                foreach (Vector2Int antenna2 in antennas)
                {
                    if (antenna == antenna2)
                    {
                        continue;
                    }
                    var (node1, node2) = CalculateNodeLocations(antenna, antenna2);
                    Console.WriteLine($"node1: {node1.X} {node1.Y} node2: {node2.X} {node2.Y}");
                    if (IsWithinBounds(node1))
                    {
                        _map[node1.X, node1.Y].IsAntinode = true;
                    }

                    if (IsWithinBounds(node2))
                    {
                        _map[node2.X, node2.Y].IsAntinode = true;
                    }

                }
            }
        }
        return _map.Cast<Node>().Count(node => node.IsAntinode);
    }
    
    private bool IsWithinBounds(Vector2Int position)
    {
        return position.X >= 0 && position.X < _map.GetLength(0) && position.Y >= 0 && position.Y < _map.GetLength(1);
    }

    public (Vector2Int, Vector2Int) CalculateNodeLocations(Vector2Int antenna1, Vector2Int antenna2)
    {
        
        
        // Vector between the antennas
        int deltaX = antenna2.X - antenna1.X;
        int deltaY = antenna2.Y - antenna1.Y;

        // Extend the line to find the two antinodes
        Vector2Int node1 = new(antenna1.X - deltaX, antenna1.Y - deltaY); // Extension on one side
        Vector2Int node2 = new(antenna2.X + deltaX, antenna2.Y + deltaY); // Extension on the other side

        return (node1, node2);
    }

    

}



public record Node(char Antenna)
{
    public bool IsAntinode { get; set; } = false;
    public char Antenna { get; set; } = Antenna;
    public bool IsAntenna => Antenna != '.';
}