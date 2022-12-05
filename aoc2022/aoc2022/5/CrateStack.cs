using System.ComponentModel;
using System.Runtime.InteropServices;

namespace aoc2022._5;

internal class CargoStorageWithCrane
{
    private readonly CrateStack[] _possibleCrateStacks;

    public CargoStorageWithCrane()
    {
        _possibleCrateStacks = new CrateStack[9];
        for(int i= 0; i < 9; i++)
            _possibleCrateStacks[i] = new CrateStack();
    }

    public void AddCrate(int stackId, Crate crate)
    {
        _possibleCrateStacks[stackId-1].AddCrate(crate);
    }

    public void MoveCrate(int startPosition, int endPosition)
    {
        _possibleCrateStacks[endPosition-1].AddCrate(_possibleCrateStacks[startPosition-1].RemoveCreate());
    }

    public void MoveMultipleCrates(int startPosition, int endPosition, int numberOfCrates)
    {
        var tempHold = new Stack<Crate>();
        for(var i = 0; i < numberOfCrates; i++)
            tempHold.Push(_possibleCrateStacks[startPosition - 1].RemoveCreate());

        for (int i = 0; i < numberOfCrates; ++i)
            _possibleCrateStacks[endPosition-1].AddCrate(tempHold.Pop());
    }

    public string ReturnTopCrateIdentifiers()
    {
        var crateIds = string.Empty;
        for(int i = 0; i < _possibleCrateStacks.Length; i++)
        {
            crateIds += _possibleCrateStacks[i].GetTopCrateIdentifier();
        }

        return crateIds.Replace(" ", "");
    }
}

internal class CrateStack
{
    private readonly Stack<Crate> CrateStacks = new();

    public Crate RemoveCreate() => CrateStacks.Pop();
    public void AddCrate(Crate crate) => CrateStacks.Push(crate);
    public char GetTopCrateIdentifier()
    {
        if (CrateStacks.Count == 0)
            return ' ';
        return CrateStacks.Peek().Id;
    }
}

internal record Crate(char Id);
