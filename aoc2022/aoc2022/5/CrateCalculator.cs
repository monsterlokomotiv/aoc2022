namespace aoc2022._5;

internal class CrateCalculator
{
    private readonly string _fileInput;
    private readonly CargoStorageWithCrane _cargoStorageWithCrane = new();

    public CrateCalculator()
    {
        //_fileInput = Directory.GetCurrentDirectory() + "\\5\\exampleInput.txt";
        _fileInput = Directory.GetCurrentDirectory() + "\\5\\5_input.txt";
    }

    public string GetTopCrateIdentifiersAfterMovingIndividually()
    {
        var moves = InitializeAndGetMoves(_fileInput);
        foreach (var moveSet in moves)
            PerformCrateMovesOneByOne(moveSet);
        return _cargoStorageWithCrane.ReturnTopCrateIdentifiers();
    }

    public string GetTopCrateIdentifiersUsingBulkMove()
    {
        var moves = InitializeAndGetMoves(_fileInput);
        foreach (var moveSet in moves)
            PerformMovesAsSet(moveSet);
        return _cargoStorageWithCrane.ReturnTopCrateIdentifiers();
    }

    private void InitializeCargoStorage(List<string> initialSetupLines)
    {
        //Last line should be the number of stacks
        var finalLineIndex = initialSetupLines.Count - 1;

        for(var i = initialSetupLines.Count-1; i >= 0; i--)
        {
            for(var column = 0; column < initialSetupLines[i].Length; column++)
            {
                var currLetter = initialSetupLines[i][column];
                if (!char.IsLetter(currLetter) || currLetter == '[' || currLetter == ']')
                    continue;

                var stackId = int.Parse(new ReadOnlySpan<char>(initialSetupLines[finalLineIndex][column]));
                _cargoStorageWithCrane.AddCrate(stackId, new Crate(currLetter));
            }
        }
    }

    private MoveSet[] InitializeAndGetMoves(string fileInput)
    {
        List<string> initialSetupLines = new();
        List<string> moveLines = new();
        var shouldTakeNextLineAsMoves = false;

        foreach (var line in File.ReadLines(fileInput))
        {
            if (string.IsNullOrWhiteSpace(line.Trim()))
            {
                //File splits using empty line to separate intial setup and moves
                shouldTakeNextLineAsMoves = true;
                continue;
            }

            if (shouldTakeNextLineAsMoves)
            {
                moveLines.Add(line);
                continue;
            }

            initialSetupLines.Add(line);
        }

        InitializeCargoStorage(initialSetupLines);

        return ParseLinesToMoveSet(moveLines.ToArray());
    }

    private static MoveSet[] ParseLinesToMoveSet(string[] lines)
    {
        List<MoveSet> moves = new();
        foreach (var line in lines)
        {
            //Note to self - of course this can be done better/more dynamic but good enough for now
            var segments = line.Split(' ');
            if(int.TryParse(segments[1], out int numberOfMoves) &&
                int.TryParse(segments[3], out int from) &&
                int.TryParse(segments[5], out int to))
                moves.Add(new MoveSet(numberOfMoves, from, to));
        }
        return moves.ToArray();
    }

    private void PerformCrateMovesOneByOne(MoveSet set)
    {
        for(int i = 0; i < set.NumberOfMoves; i++)
        {
            _cargoStorageWithCrane?.MoveCrate(set.From, set.To);
        }
    }

    private void PerformMovesAsSet(MoveSet set)
    {
        _cargoStorageWithCrane.MoveMultipleCrates(set.From, set.To, set.NumberOfMoves);
    }
}

internal record MoveSet(int NumberOfMoves, int From, int To);
