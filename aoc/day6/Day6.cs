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
        Reset();

        int totalPossibleExtraObstacles = _mapSize.Item1 * _mapSize.Item2;
        for (int i = 0; i < totalPossibleExtraObstacles; i++)
        {
            HashSet<(Vector2Int Position, PlayerDirection Direction)> visitedStates = [];

            Vector2Int position = new(i / _mapSize.Item2, i % _mapSize.Item2);
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
                    Console.WriteLine("crossing at " + _playerPosition + " with direction " + _playerDirection);
                    AmountOfLoops += 1;
                    break;
                }
            } 
            Reset();
        }
    }

    private void Reset()
    {
        Map.Reset(_originalMap);
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
        // if player -> down, and player position is at the bottom of the map -> win, idem for the other directions
        switch (_playerDirection)
        {
            case PlayerDirection.Up:
                if (_playerPosition.X == 0)
                {
                    _hasWon = true;
                }

                break;
            case PlayerDirection.Right:
                if (_playerPosition.Y == _mapSize.Item2 - 1)
                {
                    _hasWon = true;
                }

                break;
            case PlayerDirection.Down:
                if (_playerPosition.X == _mapSize.Item1 - 1)
                {
                    _hasWon = true;
                }

                break;
            case PlayerDirection.Left:
                if (_playerPosition.Y == 0)
                {
                    _hasWon = true;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_hasWon)
        {
            this[_playerPosition] = Entity.Visited;
        }
    }


    private void Move()
    {
        bool isObstacleInFront = false;
        do
        {
            Vector2Int nextPosition = WhereTo();
            Entity pos = this[nextPosition];
            switch (pos)
            {
                case Entity.Obstacle:
                    _playerDirection = TurnRight();
                    isObstacleInFront = true;
                    break;
                case Entity.Empty:
                case Entity.Visited:
                    this[_playerPosition] = Entity.Visited;
                    _playerPosition = nextPosition;
                    this[_playerPosition] = Entity.Player;
                    isObstacleInFront = false;
                    break;
                case Entity.Player:
                    throw new Exception("Player is already there");
            }
        } while (isObstacleInFront);
    }

    private PlayerDirection TurnRight()
    {
        return _playerDirection switch
        {
            PlayerDirection.Up => PlayerDirection.Right,
            PlayerDirection.Right => PlayerDirection.Down,
            PlayerDirection.Down => PlayerDirection.Left,
            PlayerDirection.Left => PlayerDirection.Up,
            _ => throw new ArgumentOutOfRangeException()
        };
    }


    private Vector2Int WhereTo()
    {
        switch (_playerDirection)
        {
            case PlayerDirection.Up:
                return _playerPosition with { X = _playerPosition.X - 1 };
            case PlayerDirection.Right:
                return _playerPosition with { Y = _playerPosition.Y + 1 };
            case PlayerDirection.Down:
                return _playerPosition with { X = _playerPosition.X + 1 };
            case PlayerDirection.Left:
                return _playerPosition with { Y = _playerPosition.Y - 1 };
            default:
                throw new ArgumentOutOfRangeException();
        }
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

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.X + b.X, a.Y + b.Y);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new Vector2Int(a.X - b.X, a.Y - b.Y);
    public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);
    public static bool operator !=(Vector2Int a, Vector2Int b) => !a.Equals(b);
}

public static class MapExtensions
{
    public static void Reset(this Entity[,] map, Entity[,] originalMap)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = originalMap[i, j];
            }
        }
    }
}