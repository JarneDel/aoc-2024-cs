namespace aoc.day6;

public class Day6
{
    public Entity[,] Map;
    private readonly Entity[,] _originalMap;
    private PlayerDirection _originalPlayerDirection;
    private Vector2Int _originalPlayerPosition;
    private PlayerDirection _playerDirection = PlayerDirection.Up;
    private bool _hasWon = false;
    private Vector2Int _playerPosition;
    private readonly (int, int) _mapSize;
    private HashSet<Vector2Int> _visitedLocations = new();

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
        new(-1, 0), // Up
        new(0, 1), // Right
        new(1, 0), // Down
        new(0, -1) // Left
    ];

    private static readonly PlayerDirection[] RightTurns =
    [
        PlayerDirection.Right, // Up turns right to Right
        PlayerDirection.Down, // Right turns right to Down
        PlayerDirection.Left, // Down turns right to Left
        PlayerDirection.Up // Left turns right to Up
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


    public int Part2()
    {
        Part1();
        // Clone visited locations for safe parallel access
        var clonedVisitedLocations = new HashSet<Vector2Int>(_visitedLocations);

        // Thread-safe counter for loops
        int totalLoops = 0;

        // Use Parallel.ForEach to process each position in clonedVisitedLocations
        Parallel.ForEach(clonedVisitedLocations, position =>
        {
            // Create local state for the current iteration
            var localMap = _originalMap.Clone() as Entity[,];
            var localPlayerPosition = _originalPlayerPosition;
            var localPlayerDirection = _originalPlayerDirection;
            bool localHasWon = false;
            var localVisitedStates = new HashSet<(Vector2Int Position, PlayerDirection Direction)>();

            // Modify the map for the current position
            if (localMap[position.X, position.Y] == Entity.Empty || localMap[position.X, position.Y] == Entity.Visited)
            {
                localMap[position.X, position.Y] = Entity.Obstacle;
            }

            // Simulate movements until winning or a loop is detected
            while (!localHasWon)
            {
                // Simulate a move
                (localPlayerPosition, localPlayerDirection, localHasWon) = SimulateMove(
                    localMap,
                    localPlayerPosition,
                    localPlayerDirection,
                    _mapSize
                );

                // Detect loops
                bool isAdded = localVisitedStates.Add((localPlayerPosition, localPlayerDirection));
                if (!isAdded)
                {
                    Interlocked.Increment(ref totalLoops);
                    break;
                }
            }
        });

        AmountOfLoops = totalLoops;
        return totalLoops;
    }

    // Helper method to simulate a move
    private (Vector2Int newPosition, PlayerDirection newDirection, bool hasWon) SimulateMove(
        Entity[,] map,
        Vector2Int currentPosition,
        PlayerDirection currentDirection,
        (int Rows, int Cols) mapSize)
    {
        bool obstacleAhead = false;
        PlayerDirection direction = currentDirection;
        Vector2Int position = currentPosition;

        do
        {
            Vector2Int nextPosition = new Vector2Int(
                position.X + DirectionVectors[(int)direction].X,
                position.Y + DirectionVectors[(int)direction].Y
            );

            Entity pos = map[nextPosition.X, nextPosition.Y];
            switch (pos)
            {
                case Entity.Obstacle:
                    direction = TurnRight(direction);
                    obstacleAhead = true;
                    break;
                case Entity.Empty:
                case Entity.Visited:
                    map[position.X, position.Y] = Entity.Visited;
                    position = nextPosition;
                    map[position.X, position.Y] = Entity.Player;
                    obstacleAhead = false;
                    break;
                case Entity.Player:
                    throw new Exception("Player is already there");
            }
        } while (obstacleAhead);

        // Check win condition
        bool hasWon = direction switch
        {
            PlayerDirection.Up => position.X == 0,
            PlayerDirection.Right => position.Y == mapSize.Cols - 1,
            PlayerDirection.Down => position.X == mapSize.Rows - 1,
            PlayerDirection.Left => position.Y == 0,
            _ => throw new ArgumentOutOfRangeException()
        };

        return (position, direction, hasWon);
    }

    // Helper to turn right
    private PlayerDirection TurnRight(PlayerDirection direction)
    {
        return RightTurns[(int)direction];
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
        _visitedLocations.Add(_playerPosition);
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
                    _visitedLocations.Add(_playerPosition);
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
                        map[i, j] = Entity.Player;
                        _playerPosition = new Vector2Int(i, j);
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