namespace aoc.day5;

public class Day5
{

    public int Part1Result { get; set; }
    
    public Day5(string filename)
    {
        var stream = new StreamReader(filename);
        List<string> rules = [];
        List<string> data = [];
        while (stream.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            if (line.Contains('|'))
            {
                rules.Add(line);
            }
            else
            {
                data.Add(line);
            }
        }
        CalculatePart1(rules.ToArray(), data.ToArray());
    }

    public Day5(string[] rules, string[] data)
    {
        CalculatePart1(rules, data);
    }


    private void CalculatePart1(string[] rules, string[] data)
    {
        List<Rule> parsedRules = rules.Select(x =>
        {
            string[] split = x.Split('|', StringSplitOptions.TrimEntries);
            return new Rule(int.Parse(split[0]), int.Parse(split[1]));
        }).ToList();

        List<List<int>> updates = data.Select(p =>
        {
            string[] items=  p.Split(',', StringSplitOptions.TrimEntries);
            return items.Select(int.Parse).ToList();
        }).ToList();
        
        foreach (List<int> update in updates)
        {
            if (!IsUpdateCorrect(update, parsedRules)) continue;
            int count = update.Count;
            if (count % 2 != 1) continue;
            int medianIndex = count / 2;
            Part1Result += update[medianIndex];
        }
    }



    static bool IsUpdateCorrect(List<int> update, List<Rule> rules)
    {
        Dictionary<int, int> positions = new();
        for (int i = 0; i < update.Count; i++)
        {
            positions[update[i]] = i;
        }

        HashSet<int> updatePages = [..update];
        return rules.All(rule => !updatePages.Contains(rule.X) || !updatePages.Contains(rule.Y) || positions[rule.X] <= positions[rule.Y]);
    }
    
    
    
    
}


public struct Rule
{
    public int X;
    public int Y;
    public Rule(int x, int y)
    {
        X = x;
        Y = y;
    }
}