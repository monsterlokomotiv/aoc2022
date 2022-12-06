namespace aoc2022._6;

internal sealed class StartOfDetector
{
    private readonly int _numberOfCharsRequiredToBeUnique;

    public StartOfDetector(int numberOfCharsRequiredToBeUnique)
    {
        _numberOfCharsRequiredToBeUnique = numberOfCharsRequiredToBeUnique;
    }

    public int GetNumberOfCharsProcessedForDetection(string input)
    {
        var counter = 0;
        var characterCheck = new Queue<char>();
        while(counter < input.Length)
        {
            characterCheck.Enqueue(input[counter]);

            if(characterCheck.Count > _numberOfCharsRequiredToBeUnique)
            {
                characterCheck.Dequeue();
            }

            if(characterCheck.Count == _numberOfCharsRequiredToBeUnique && characterCheck.Distinct().Count() == _numberOfCharsRequiredToBeUnique)
            {
                return counter+1; //We actually start on 1 but we stay one lower for indexing
            }

            counter++;
        }
        return -1;
    }
}
