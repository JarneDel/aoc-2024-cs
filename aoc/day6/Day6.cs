using System.Numerics;

namespace aoc.day6;

public class Day6
{
    public Entity[,] Map;
    private readonly Entity[,] _originalMap;
    private PlayerDirection _originalPlayerDirection;
    private Vector2Int _originalPlayerPosition;
    private PlayerDirection _playerDirection;
    private bool _hasWon = false;
    private Vector2Int _playerPosition;
    private readonly (int, int) _mapSize;

    public int VisitedLocationCount { get; set; }
    public int AmountOfLoops { get; set; }

    public Day6(string filename)
    {
        Map = Convert(File.ReadAllLines(filename).ToList());
        _originalMap = Map.Clone() as Entity[,] ?? throw new Exception("Map is null");
        _mapSize = (Map.GetLength(0), Map.GetLength(1));
    }

    public Day6(List<string> mapRaw)
    {
        Map = Convert(mapRaw);
        _originalMap = Map.Clone() as Entity[,] ?? throw new Exception("Map is null");
        _mapSize = (Map.GetLength(0), Map.GetLength(1));
    }
    
    
    private static readonly Vector2Int[] DirectionVectors =
    [
        new(-1,0), // Up
        new(0,1),  // Right
        new(1,0),  // Down
        new(0,-1)  // Left
    ];
    private static readonly PlayerDirection[] RightTurns =
    [
        PlayerDirection.Right, // Up turns right to Right
        PlayerDirection.Down,  // Right turns right to Down
        PlayerDirection.Left,  // Down turns right to Left
        PlayerDirection.Up     // Left turns right to Up
    ];
    


    public void Part1()
    {
        while (!_hasWon)
        {
            Move();
            CheckIsWin();
        }

        VisitedLocationCount = Map.Cast<Entity>().Count(e => e == Entity.Visited);
    }


    public void Part2()
    {
        Part1();
        List<Vector2Int> visitedLocations = new();
        for (int i = 0; i < _mapSize.Item1; i++)
        {
            for (int j = 0; j < _mapSize.Item2; j++)
            {
                if (this[new Vector2Int(i, j)] == Entity.Visited)
                {
                    visitedLocations.Add(new Vector2Int(i, j));
                }
            }
        }
        
        Reset();

        HashSet<(Vector2Int Position, PlayerDirection Direction)> visitedStates = [];

        for (int i = 0; i < visitedLocations.Count; i++)
        {

            Vector2Int position = visitedLocations[i];
            if (this[position] == Entity.Empty || this[position] == Entity.Visited)
            {
                this[position] = Entity.Obstacle;
            }


            while (!_hasWon)
            {
                Move();
                CheckIsWin();
                bool isAdded = visitedStates.Add((_playerPosition, _playerDirection));

                if (!isAdded)
                {
                    AmountOfLoops += 1;
                    break;
                }
            } 
            Reset();
            visitedStates.Clear();
        }
    }

    private void Reset()
    {
        for (int i = 0; i < _originalMap.GetLength(0); i++)
        {
            for (int j = 0; j < _originalMap.GetLength(1); j++)
            {
                Map[i, j] = _originalMap[i, j];
            }
        }        
        _playerPosition = _originalPlayerPosition;
        _playerDirection = _originalPlayerDirection;
        _hasWon = false;
    }


    public Entity this[Vector2Int position]
    {
        get => Map[position.X, position.Y];
        set => Map[position.X, position.Y] = value;
    }

    private void CheckIsWin()
    {
        bool hasWon = _playerDirection switch
        {
            PlayerDirection.Up => _playerPosition.X == 0,
            PlayerDirection.Right => _playerPosition.Y == _mapSize.Item2 - 1,
            PlayerDirection.Down => _playerPosition.X == _mapSize.Item1 - 1,
            PlayerDirection.Left => _playerPosition.Y == 0,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (!hasWon) return;
        _hasWon = true;
        this[_playerPosition] = Entity.Visited;
    }



    private void Move()
    {
        bool obstacleAhead = false;
        do
        {
            Vector2Int nextPosition = WhereTo();
            Entity pos = this[nextPosition];
            switch (pos)
            {
                case Entity.Obstacle:
                    _playerDirection = TurnRight();
                    obstacleAhead = true;
                    break;
                case Entity.Empty:
                case Entity.Visited:
                    this[_playerPosition] = Entity.Visited;
                    _playerPosition = nextPosition;
                    this[_playerPosition] = Entity.Player;
                    VisitedLocationCount += 1;
                    obstacleAhead = false;
                    break;
                case Entity.Player:
                    throw new Exception("Player is already there");
            }
        } while (obstacleAhead);
    }

    private PlayerDirection TurnRight() => RightTurns[(int)_playerDirection];


    private Vector2Int WhereTo()
    {
        return new Vector2Int(
            _playerPosition.X + DirectionVectors[(int)_playerDirection].X,
            _playerPosition.Y + DirectionVectors[(int)_playerDirection].Y
        );
    }
 

    private Entity[,] Convert(List<string> mapRaw)
    {
        Entity[,] map = new Entity[mapRaw.Count, mapRaw[0].Length];
        for (int i = 0; i < mapRaw.Count; i++)
        {
            string line = mapRaw[i];
            for (int j = 0; j < line.Length; j++)
            {
                char character = line[j];
                switch (character)
                {
                    case '#':
                        map[i, j] = Entity.Obstacle;
                        break;
                    case '^':
                    case '>':
                    case '<':
                    case 'v':
                        map[i, j] = Entity.Player;
                        _playerPosition = new Vector2Int(i, j);
                        _playerDirection = character switch
                        {
                            '^' => PlayerDirection.Up,
                            '>' => PlayerDirection.Right,
                            '<' => PlayerDirection.Left,
                            'v' => PlayerDirection.Down,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        _originalPlayerDirection = _playerDirection;
                        _originalPlayerPosition = _playerPosition;
                        break;
                    case '.':
                    default:
                        map[i, j] = Entity.Empty;
                        break;
                }
            }
        }

        return map;
    }
}

public enum Entity
{
    Empty,
    Obstacle,
    Player,
    Visited
}

public enum PlayerDirection
{
    Up,
    Right,
    Down,
    Left
}

public struct Vector2Int(int x, int y) : IEquatable<Vector2Int>
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public override string ToString() => $"<{X}, {Y}>";

    public override bool Equals(object? obj) => obj is Vector2Int other && Equals(other);

    public bool Equals(Vector2Int other) => X == other.X && Y == other.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);
    public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);
    public static bool operator !=(Vector2Int a, Vector2Int b) => !a.Equals(b);
}
