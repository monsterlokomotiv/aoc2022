namespace aoc2022._4;

internal class ElfAssignmentCalculator
{
    private readonly string _fileInput;

    public ElfAssignmentCalculator()
    {
        //_fileInput = Directory.GetCurrentDirectory() + "\\4\\exampleInput.txt";
        _fileInput = Directory.GetCurrentDirectory() + "\\4\\4_input.txt";
    }

    public int CalculateNumberOfCompletelyOverlappingAssignments()
    {
        var totalRedundancies = 0;
        foreach (var assignmentPair in ParseLinesToAssignments(_fileInput))
        {
            totalRedundancies += GetNumberOfCompleteOverlapsInAssignments(assignmentPair);
        }
        return totalRedundancies;
    }

    public int CalculateNumberOfAnyOverlappingAssignment()
    {
        var totalRedundancies = 0;
        foreach (var assignmentPair in ParseLinesToAssignments(_fileInput))
        {
            totalRedundancies += GetNumberOfAnyOverlapsInAssignments(assignmentPair);
        }
        return totalRedundancies;
    }

    private static int GetNumberOfCompleteOverlapsInAssignments(ElfAssignment[] assignments)
    {
        var numberOfRedundancies = 0;

        for(int i = 0; i < assignments.Length-1; i++)
        {
            var next = i + 1;
            if (assignments[i].Start <= assignments[next].Start && assignments[i].End >= assignments[next].End ||
                assignments[next].Start <= assignments[i].Start && assignments[next].End >= assignments[i].End)
                numberOfRedundancies++;
        }

        return numberOfRedundancies;
    }

    private static int GetNumberOfAnyOverlapsInAssignments(ElfAssignment[] assignments)
    {
        var maxSections = assignments.Max(e => e.End);
        var sections = new int[maxSections];

        var sectionId = 0;
        for(int i = 0; i < sections.Length; i++)
        {
            sectionId = i + 1;
            sections[i] = assignments.Count(a => a.Start <= sectionId && a.End >= sectionId);
        }

        return sections.Any(e => e > 1) ? 1 : 0;
    }

    private static IEnumerable<ElfAssignment[]> ParseLinesToAssignments(string fileInput)
    {
        List<ElfAssignment> createdAssignments = new();
        foreach(var line in File.ReadLines(fileInput))
        {
            if(string.IsNullOrEmpty(line)) continue;

            createdAssignments.Clear();
            var assignments = line.Split(',');
            
            foreach(var assignment in assignments)
                createdAssignments.Add(MapToAssignment(assignment));

            yield return createdAssignments.ToArray();    
        }
    }

    private static ElfAssignment MapToAssignment(string assignmentDetails)
    {
        var dets = assignmentDetails.Split('-');
        if(dets.Length != 2)
            throw new ArgumentOutOfRangeException(nameof(assignmentDetails), "Invalid specification of assignment for elf!");

        return new ElfAssignment(int.Parse(dets[0]), int.Parse(dets[1]));
    }
}

internal record ElfAssignment(int Start, int End);
