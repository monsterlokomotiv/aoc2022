using System.Runtime.CompilerServices;

namespace aoc2022._2;

internal class RockPaperScissorsStrategy
{
    private const int Rock = 0;
    private const int Paper = 1;
    private const int Scissors = 2;
    private const int Loss = 0;
    private const int LossValue = 0;
    private const int Draw = 1;
    private const int DrawValue = 3;
    private const int Win = 2;
    private const int WinValue = 6;

    private readonly string _fileInputPath;

    private readonly Dictionary<char, int> _playerShapeIndex = new()
    {
        {'X', Rock },
        {'Y', Paper },
        {'Z', Scissors }
    };

    private readonly Dictionary<char, int> _opponentShapeIndex = new()
    {
        { 'A', Rock },
        { 'B', Paper },
        { 'C', Scissors }
    };

    private readonly Dictionary<char, int> _outComeIndex = new()
    {
        {'X', Loss },
        {'Y', Draw },
        {'Z', Win } 
    };

    private readonly Dictionary<int, int> _shapeScoreMap = new()
    {
        { Rock, 1 },
        { Paper, 2 },
        { Scissors, 3 }
    };

    private readonly Dictionary<int, int> _scoreMap = new()
    {
        { Loss, LossValue },
        { Draw, DrawValue },
        { Win, WinValue }
    };

    //SelectionOutComeMap yields the points based on two players input
    private readonly int[,] _selectionOutComeMap;

    //DesiredShapeMap yields the shape value based on opponent input and desired game outcome
    private readonly int[,] _desiredShapeMap;

    public RockPaperScissorsStrategy()
    {
        _fileInputPath = Directory.GetCurrentDirectory() + "\\2\\2_input.txt";
        //_fileInputPath = Directory.GetCurrentDirectory() + "\\2\\exampleInput.txt";

        _desiredShapeMap = new int[3, 3];
        InitializeDesiredShapeMap();
        _selectionOutComeMap = new int[3, 3];
        InitializeSelectionOutcomeMap();
        
    }

    public int CalculateScoreForStrategyBasedOnInputs()
    {
        var totalScore = 0;
        foreach(var (opponentSelection, playerSelection) in ParseLineToSelections(_fileInputPath))
        {
            totalScore += CalculateScoreForSelection(opponentSelection, playerSelection);
        }

        return totalScore;
    }

    public int CalculateScoreForStrategyBasedOnOpponentAndOutcome()
    {
        var totalScore = 0;
        foreach (var (opponentSelection, outcome) in ParseLineToSelections(_fileInputPath))
        {
            totalScore += CalculateScoreForSelectionAndOutcome(opponentSelection, outcome);
        }

        return totalScore;
    }

    private int CalculateScoreForSelection(char opponentSelection, char playerSelection)
    {
        var playerShapeIndex = _playerShapeIndex[playerSelection];
        return _selectionOutComeMap[_opponentShapeIndex[opponentSelection], playerShapeIndex] + _shapeScoreMap[playerShapeIndex];
    }

    private int CalculateScoreForSelectionAndOutcome(char opponentSelection, char desiredOutcome)
    {
        var opponentSelectionIndex = _opponentShapeIndex[opponentSelection];
        var outComeIndex = _outComeIndex[desiredOutcome];
        var desiredShapeIndex = _desiredShapeMap[opponentSelectionIndex, outComeIndex];

        return _scoreMap[outComeIndex] + _shapeScoreMap[desiredShapeIndex];
    }

    private void InitializeDesiredShapeMap()
    {
        //OpponentSelection, DesiredOutcome
        _desiredShapeMap[Rock, Loss] = Scissors;
        _desiredShapeMap[Rock, Draw] = Rock;
        _desiredShapeMap[Rock, Win] = Paper;
        _desiredShapeMap[Paper, Loss] = Rock;
        _desiredShapeMap[Paper, Draw] = Paper;
        _desiredShapeMap[Paper, Win] = Scissors;
        _desiredShapeMap[Scissors, Loss] = Paper;
        _desiredShapeMap[Scissors, Draw] = Scissors;
        _desiredShapeMap[Scissors, Win] = Rock;
    }

    private void InitializeSelectionOutcomeMap()
    {
        //OpponentSelection, PlayerSelection
        _selectionOutComeMap[Rock, Rock] = DrawValue; 
        _selectionOutComeMap[Rock, Paper] = WinValue;
        _selectionOutComeMap[Rock, Scissors] = LossValue;
        _selectionOutComeMap[Paper, Rock] = LossValue;
        _selectionOutComeMap[Paper, Paper] = DrawValue;
        _selectionOutComeMap[Paper, Scissors] = WinValue;
        _selectionOutComeMap[Scissors, Rock] = WinValue;
        _selectionOutComeMap[Scissors, Paper] = LossValue;
        _selectionOutComeMap[Scissors, Scissors] = DrawValue;
    }

    private static IEnumerable<(char, char)> ParseLineToSelections(string fileInput)
    {
        foreach (var line in File.ReadLines(fileInput))
        {
            var chars = line.Trim().Split(' ');
            yield return new(chars[0][0], chars[1][0]);
        }
    }
}