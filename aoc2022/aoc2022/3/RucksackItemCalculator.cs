namespace aoc2022._3;

internal class RucksackItemCalculator
{
    private readonly string _fileInput;

    public RucksackItemCalculator()
    {
        _fileInput = Directory.GetCurrentDirectory() + "\\3\\3_input.txt";
        //_fileInput = Directory.GetCurrentDirectory() + "\\3\\exampleInput.txt";
    }

    public int GetSumOfItemTypePriorities()
    {
        var totalItemTypePriority = 0;
        foreach(var (comp1, comp2) in ParseLineToCompartments(_fileInput))
        {
            var uniqueForThisRuksack = GetCommonUniqueItemTypes(comp1, comp2);
            totalItemTypePriority += uniqueForThisRuksack.Select(ItemPriorityCalculator.GetItemPriority).Sum();
        }

        return totalItemTypePriority;
    }

    public int GetSumOfBadges()
    {
        var totalBadgeValues = 0;
        foreach(var group in ParseLineToGroups(_fileInput))
        {
            var groupBadge = GetGroupBadge(group);
            totalBadgeValues += ItemPriorityCalculator.GetItemPriority(groupBadge);
        }

        return totalBadgeValues;
    }

    private static char GetGroupBadge(string[] groupRucksacks)
    {
        var allUniquesJoined = string.Empty;
        foreach (var rucksack in groupRucksacks)
        {
            foreach(var uniqueChar in GetUniqueItemTypes(rucksack))
                allUniquesJoined += uniqueChar;
        }
            

        var characterGroup = allUniquesJoined.GroupBy(c => c).FirstOrDefault(c => c.Count() == 3);
        if (characterGroup == null)
            throw new ArgumentNullException(nameof(characterGroup), "Could not find a common badge/item for group of rucksacks!");

        return characterGroup.Key;
    }

    private static char[] GetCommonUniqueItemTypes(string firstCompartment, string secondCompartment)
    {
        var firstContents = GetUniqueItemTypes(firstCompartment);
        var secondContents = GetUniqueItemTypes(secondCompartment);

        return firstContents.Intersect(secondContents).ToArray();
    }

    private static char[] GetUniqueItemTypes(string itemTypes)
    {
        var uniqueItemTypes = new HashSet<char>();
        foreach(var ch in itemTypes)
        {
            if(!uniqueItemTypes.Contains(ch))
                uniqueItemTypes.Add(ch);
        }

        return uniqueItemTypes.ToArray();
    }

    private static IEnumerable<(string, string)> ParseLineToCompartments(string fileInput)
    {
        foreach(var line in File.ReadLines(fileInput))
        {
            if(string.IsNullOrEmpty(line)) continue;
            var middle = line.Length / 2;
            yield return (line[..middle], line[middle..]);
        }
    }

    private static IEnumerable<string[]> ParseLineToGroups(string fileInput)
    {
        var currentGroup = new List<string>();
        foreach(var line in File.ReadLines(fileInput))
        {
            if(string.IsNullOrEmpty(line)) continue;

            currentGroup.Add(line);
            if (currentGroup.Count < 3) continue;

            yield return currentGroup.ToArray();
            currentGroup.Clear();
        }
    }
}
