namespace aoc;

public class Day1
{
   
    public static async Task<string> RunAsync(string filename)
    {
        List<int> leftList = [];
        List<int> rightList = [];
        using StreamReader file = new(filename);
        while (await file.ReadLineAsync() is { } line)
        {
            var split = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            leftList.Add(int.Parse(split[0]));
            rightList.Add(int.Parse(split[1]));
        }
        
        var result = AddDistances(leftList, rightList);

        
        var result2 = CalculateSimilarityScore(leftList, rightList);
        var result3 = CalulateSimilarityScoreOneLiner(leftList, rightList);
        if (result2 != result3)
        {
            throw new Exception("yeet");
        }

        return $"Day 1 Part1: {result}, Part2: {result3}";
    }


    /// <summary>
    /// more efficient way to calculate similarity score
    /// (o(M+N))
    /// </summary>
    public static int CalculateSimilarityScore(List<int> leftList, List<int> rightList)
    {
        var sameNumbers = rightList.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        return leftList.Sum(number => number * sameNumbers.GetValueOrDefault(number, 0));
    }

    /// <summary>
    /// one liner way but less efficient (o(M*N))
    /// </summary>
    public static int CalulateSimilarityScoreOneLiner(List<int> leftList, List<int> rightList)
    {
        return leftList.Sum(number => number * rightList.Count(x => x == number));
    }
    
    

    public static int AddDistances(List<int> leftList, List<int> rightList)
    {
        leftList.Sort();
        rightList.Sort();
        var differences = leftList.Zip(rightList, (left, right) => Math.Abs(left - right));
        return differences.Sum();
    }
}