namespace aoc.day2;

public static class Day2
{
    public static async Task<int> Part1Calculation(string[] input)
    {
        return input.Count(Line =>
        {
            var numbers = Line.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return Enumerable.Range(1, numbers.Count - 1).All(i =>
            {
                int diff = Math.Abs(numbers[i] - numbers[i - 1]);
                return diff <= 3 && diff != 0 &&
                       (numbers[1] > numbers[0] ? numbers[i] >= numbers[i - 1] : numbers[i] <= numbers[i - 1]);
            });
        });
    }

    public static async Task<int> Part2Calculation(string[] input) =>
        input.Count(line =>
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return IsSafe(numbers) || Enumerable.Range(0, numbers.Count)
                .Any(i => IsSafe(numbers.Where((_, index) => index != i).ToList()));
        });

    
    public static async Task<int> Part2Calculation(string filename)
    {
        using StreamReader reader = new(filename);
        int safeCount = 0;

        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            if (IsSafe(numbers) || Enumerable.Range(0, numbers.Count).Any(i => IsSafe(numbers.Where((_, index) => index != i).ToList())))
            {
                safeCount++;
            }
        }

        return safeCount;
    }


    
    private static bool IsSafe(List<int> numbers) =>
        Enumerable.Range(1, numbers.Count - 1).All(i =>
            Math.Abs(numbers[i] - numbers[i - 1]) is > 0 and <= 3 &&
            (numbers[1] > numbers[0] ? numbers[i] >= numbers[i - 1] : numbers[i] <= numbers[i - 1]));


    public static async Task<int> Part1CalculationWithStreamReader(string filename)
    {
        using StreamReader file = new(filename);
        int count = 0;
        while (await file.ReadLineAsync() is { } line)
        {
            var numbers = line.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            if (Enumerable.Range(1, numbers.Count - 1).All(i =>
                {
                    int diff = Math.Abs(numbers[i] - numbers[i - 1]);
                    return diff <= 3 && diff != 0 &&
                           (numbers[1] > numbers[0] ? numbers[i] >= numbers[i - 1] : numbers[i] <= numbers[i - 1]);
                }))
            {
                count++;
            }
        }

        return count;
    }
}