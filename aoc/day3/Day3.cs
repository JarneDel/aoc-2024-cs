namespace aoc.day3;

public static class Day3
{
    public static int Part1CalculationFile(string path) => Calculate(System.IO.File.ReadAllText(path), true);

    public static int Part2CalculationFile(string path) => Calculate(System.IO.File.ReadAllText(path), false);

    public static int Calculate(string input, bool ignoreToggle)
    {
        int result = 0, i = 0;
        bool isEnabled = true;

        while (i < input.Length)
        {
            if (i + 4 <= input.Length && input.Substring(i, 4) == "mul(" && isEnabled)
            {
                i += 4;
                int num1 = ReadNumber(input, ref i);
                if (i >= input.Length || input[i] != ',') continue;
                i++; // Increment index after the comma
                int num2 = ReadNumber(input, ref i);
                if (i < input.Length && input[i++] == ')') result += num1 * num2;
            }
            else if (!ignoreToggle && i + 4 <= input.Length && input.Substring(i, 4) == "do()") { isEnabled = true; i += 4; }
            else if (!ignoreToggle && i + 7 <= input.Length && input.Substring(i, 7) == "don't()") { isEnabled = false; i += 7; }
            else i++;
        }
        return result;
    }

    private static int ReadNumber(string input, ref int i)
    {
        int num = 0;
        while (i < input.Length && char.IsDigit(input[i])) num = num * 10 + (input[i++] - '0');
        return num;
    }
}