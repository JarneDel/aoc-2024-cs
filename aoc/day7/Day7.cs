namespace aoc.day7;

public class Day7
{
    private static readonly char[] Operators = ['+', '*'];
    public long Part1Result = 0;
    private readonly CalibrationEquation[]? _calibrationEquations = null;
    private readonly StreamReader? _streamReader = null;
    
    
    public Day7(string[] input)
    {
        CalibrationEquation[] parsed = ParseInput(input);
        _calibrationEquations = parsed;

    }

    public Day7(string fileName)
    {
        _streamReader = new StreamReader(fileName);
    }
    
    public long Part1()
    {
        if (_calibrationEquations != null)
        {
            foreach (CalibrationEquation calibrationEquation in _calibrationEquations)
            {
                List<List<char>> combinations = GetAllPossibleCombination(calibrationEquation.Length - 1);
                if (combinations.Any(combination => calibrationEquation.IsEqual(combination)))
                {
                    Part1Result += calibrationEquation.Solution;
                }
            }
        }
        else if (_streamReader != null)
        {
            while (_streamReader.ReadLine() is { } line)
            {
                CalibrationEquation calibrationEquation = new(line);
                List<List<char>> combinations = GetAllPossibleCombination(calibrationEquation.Length - 1);
                if (combinations.Any(combination => calibrationEquation.IsEqual(combination)))
                {
                    Part1Result += calibrationEquation.Solution;
                }
            }
        }
        return Part1Result;
    }
    
    private static CalibrationEquation[] ParseInput(string[] input)
    {
        return input.Select(x => new CalibrationEquation(x)).ToArray();
    }

    private List<List<char>> GetAllPossibleCombination(int length)
    {
        List<List<char>> result = [];
        int totalCombinations = (int)Math.Pow(Operators.Length, length);

        for (int i = 0; i < totalCombinations; i++)
        {
            List<char> combination = [..new char[length]];
            int temp = i;

            for (int j = 0; j < length; j++)
            {
                
                combination[j] = Operators[temp % Operators.Length];
                temp /= Operators.Length;
            }

            result.Add(combination);
        }

        return result;
    }



    
    
    
}

public struct CalibrationEquation
{
    public CalibrationEquation(string input)
    {
        string[] split = input.Split([':'], StringSplitOptions.RemoveEmptyEntries);
        Solution = long.Parse(split[0]);
        Numbers = split[1].Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }
    public CalibrationEquation(int solution, int[] numbers)
    {
        Solution = solution;
        Numbers = numbers;
    }
    
    public long Solution { get; set; }
    public int[] Numbers { get; set; }

    public int Length => Numbers.Length;
    

    public bool IsEqual(List<char> operators)
    {
        long betweenResult = Numbers[0];
        for (int i = 0; i < operators.Count; i++)
        {
            if (i + 1 >= Numbers.Length) throw new ArgumentOutOfRangeException();
            betweenResult = operators[i] switch
            {
                '*' => betweenResult * Numbers[i + 1],
                '+' => betweenResult + Numbers[i + 1],
                _ => throw new ArgumentOutOfRangeException(operators[i].ToString())
            };
        }
        return betweenResult == Solution;

    }
    
    
}