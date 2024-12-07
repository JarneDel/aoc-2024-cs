namespace aoc.day5;

public class Day5
{

    public int Part1Result { get; private set; }
    public int Part2Result { get; private set; }
    
    public Day5(string filename)
    {
        var stream = new StreamReader(filename);
        ParseFile(stream);
    }
    
    public Day5(string[] rules, string[] data)
    {
        List<Rule> parsedRules = ParseRules(rules);
        List<int[]> parsedData = data.Select(x => x.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray()).ToList();
        CalculatePart1(parsedRules, parsedData);
        CalculatePart2(parsedRules, parsedData);
    }

    private void ParseFile(StreamReader stream)
    {
        List<Rule> rules = [];
        List<int[]> data = [];
        while (stream.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            if (line.Contains('|'))
            {
                string[] split = line.Split('|', StringSplitOptions.TrimEntries);
                rules.Add(new Rule(int.Parse(split[0]), int.Parse(split[1])));
            }
            else
            {
                string[] items = line.Split(',', StringSplitOptions.TrimEntries);
                data.Add(items.Select(int.Parse).ToArray());
            }
        }
        CalculatePart1(rules, data);
        CalculatePart2(rules, data);
    }
    

    private void CalculatePart1(List<Rule> rules, List<int[]> updates)
    {
        foreach (int[] update in updates)
        {
            if (!IsUpdateCorrect(update, rules)) continue;
            int count = update.Length;
            if (count % 2 != 1) continue;
            int medianIndex = count / 2;
            Part1Result += update[medianIndex];
        }
    }

    private void CalculatePart2(List<Rule> rules, List<int[]> updates)
    {
        foreach (int[] update in updates)
        {
            if (!IsUpdateCorrect(update, rules))
            {
                // try fix update
                List<int> fixedOrder = FixOrder(update.ToList(), rules);
                Part2Result += fixedOrder[fixedOrder.Count / 2];

            }
        }
        
        
        
    }

    private static List<int> FixOrder(List<int> update, List<Rule> rules)
    {
        HashSet<int> updatePages = [..update];
        bool changed;
        do
        {
            changed = false;
            foreach (Rule rule in rules)
            {
                if (!(updatePages.Contains(rule.X) && updatePages.Contains(rule.Y))) continue;

                int posX = update.IndexOf(rule.X);
                int posY = update.IndexOf(rule.Y);

                if (posX <= posY) continue;
                update.RemoveAt(posY);
                posX = update.IndexOf(rule.X);
                update.Insert(posX + 1, rule.Y);
                changed = true;
            }
        } while (changed);
        return update;
    }

    private static List<Rule> ParseRules(string[] rules)
    {
        return rules.Select(x =>
        {
            string[] split = x.Split('|', StringSplitOptions.TrimEntries);
            return new Rule(int.Parse(split[0]), int.Parse(split[1]));
        }).ToList();
    }
    


    private static bool IsUpdateCorrect(int[] update, List<Rule> rules)
    {
        Dictionary<int, int> positions = new();
        for (int i = 0; i < update.Length; i++)
        {
            positions[update[i]] = i;
        }

        HashSet<int> updatePages = [..update];
        return rules.All(rule => !updatePages.Contains(rule.X) || !updatePages.Contains(rule.Y) || positions[rule.X] <= positions[rule.Y]);
    }
 }


public struct Rule(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;
}