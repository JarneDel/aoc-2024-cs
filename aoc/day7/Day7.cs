namespace aoc.day7;

public class Day7
{
    private readonly CalibrationEquation[] _calibrationEquations;

    public Day7(string[] input)
    {
        _calibrationEquations = ParseInput(input);
    }

    public Day7(string fileName)
    {
        _calibrationEquations = ParseInput(File.ReadAllLines(fileName));
    }

    private static CalibrationEquation[] ParseInput(string[] input)
    {
        return input.Select(x => new CalibrationEquation(x)).ToArray();
    }

    public long Part1()
    {
        char[] operators = ['+', '*'];
        
        long part1Result = 0;
        Parallel.ForEach(_calibrationEquations, calibrationEquation =>
        {
            if (FindValidCombination(calibrationEquation.Length - 1, operators, calibrationEquation))
            {
                Interlocked.Add(ref part1Result, calibrationEquation.Solution);
            }
        });

        return part1Result;
    }
    
    public long Part2()
    {
        char[] operators = ['+', '*', '|'];

        long part2Result = 0;
        Parallel.ForEach(_calibrationEquations, calibrationEquation =>
        {
            if (FindValidCombination(calibrationEquation.Length - 1, operators, calibrationEquation))
            {
                Interlocked.Add(ref part2Result, calibrationEquation.Solution);
            }
        });

        return part2Result;
    }

    private bool FindValidCombination(int length, char[] operators, CalibrationEquation calibrationEquation)
    {
        return CheckCombination(length, operators, new List<char>(), calibrationEquation);
    }

    private bool CheckCombination(int remainingLength, char[] operators, List<char> currentCombination, CalibrationEquation calibrationEquation)
    {
        if (remainingLength == 0)
        {
            return calibrationEquation.IsEqual(currentCombination);
        }

        foreach (char op in operators)
        {
            currentCombination.Add(op);
            if (CheckCombination(remainingLength - 1, operators, currentCombination, calibrationEquation))
            {
                return true;
            }
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }

        return false;
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
                '|' => long.Parse($"{betweenResult}{Numbers[i + 1]}"),
                _ => throw new ArgumentOutOfRangeException(operators[i].ToString())
            };
        }

        return betweenResult == Solution;
    }
}