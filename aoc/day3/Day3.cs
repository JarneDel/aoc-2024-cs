namespace aoc.day3;

public static class Day3
{
    public static int Part1CalculationFile(string path)
    {
        var input = System.IO.File.ReadAllText(path);
        return Part1Calculation(input);
    }
    public static int Part2CalculationFile(string path)
    {
        var input = System.IO.File.ReadAllText(path);
        return Part2Calculation(input);
    }

    public static int Part1Calculation(string input)
    {
        int result = 0;
        int i = 0;
        while (i < input.Length)
        {
            if (input.Length - i < 4)
            {
                break;
            }

            if (input.Substring(i, 4) != "mul(")
            {
                i += 1;
            }
            else
            {
                i += 4;
                int num = 0;
                while (int.TryParse(input[i].ToString(), out int number))
                {
                    num = num * 10 + number;
                    i += 1;
                }

                if (input[i] == ',')
                {
                    i += 1;
                }
                else
                {
                    continue;
                }

                int num2 = 0;
                while (int.TryParse(input[i].ToString(), out int number))
                {
                    num2 = num2 * 10 + number;
                    i += 1;
                }

                if (input[i] == ')')
                {
                    i += 1;
                    result += num * num2;
                }
            }
        }

        return result;
    }

    public static int Part2Calculation(string input)
    {
        int result = 0;
        int i = 0;
        bool isEnabled = true;
        while (i < input.Length)
        {
            if (input.Length - i < 4)
            {
                break;
            }

            if (input.Substring(i, 4) == "mul(" && isEnabled)
            {
                i += 4;
                int num = 0;
                while (int.TryParse(input[i].ToString(), out int number))
                {
                    num = num * 10 + number;
                    i += 1;
                }

                if (input[i] == ',')
                {
                    i += 1;
                }
                else
                {
                    continue;
                }

                int num2 = 0;
                while (int.TryParse(input[i].ToString(), out int number))
                {
                    num2 = num2 * 10 + number;
                    i += 1;
                }

                if (input[i] == ')')
                {
                    i += 1;
                    result += num * num2;
                }
            }
            else if (input.Substring(i, 4) == "do()")
            {
                isEnabled = true;
                i += 4;
            }
            else if (input.Length - i >= 7 && input.Substring(i, 7) == "don't()")
            {
                isEnabled = false;
                i += 7;
            }
            else
            {
                i += 1;
            }
        }

        return result;
    }
}