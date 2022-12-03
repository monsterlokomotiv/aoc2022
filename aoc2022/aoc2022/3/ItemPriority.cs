namespace aoc2022._3;

public static class ItemPriorityCalculator
{
    private static readonly Dictionary<char, int> _itemPriorities = new Dictionary<char, int>();
    static ItemPriorityCalculator()
    {
        AddChars('a', 'z', 1);
        AddChars('A', 'Z', 27);
    }

    private static void AddChars(char start, char end, int priorityValueStart)
    {
        var charStartIndex = (int)start;
        var charEndIndex = (int)end;

        for(int i = charStartIndex; i <= charEndIndex; i++)
        {
            _itemPriorities.Add(Convert.ToChar(i), i-charStartIndex+priorityValueStart);
        }
    }

    public static int GetItemPriority(char character)
    {
        if (!_itemPriorities.TryGetValue(character, out var priority))
            throw new ArgumentOutOfRangeException(nameof(character), $"{character} is not supported or was not initialized properly!");
        return priority;
    }
}
