namespace aoc2022._6;

internal class CommSystemFixer
{
    private readonly string _fileInput;
    public CommSystemFixer()
    {
        //_fileInput = Directory.GetCurrentDirectory() + "\\6\\exampleInput.txt";
        _fileInput = Directory.GetCurrentDirectory() + "\\6\\6_input.txt";
    }

    public int GetNumberOfCharsToProcessForStartOfPacket()
    {
        var messageInput = File.ReadAllText(_fileInput);
        var detector = new StartOfDetector(4);
        return detector.GetNumberOfCharsProcessedForDetection(messageInput);
    }

    public int GetNumberOfCharsToProcessForStartOfMessage()
    {
        var messageInput = File.ReadAllText(_fileInput);
        var detector = new StartOfDetector(14);
        return detector.GetNumberOfCharsProcessedForDetection(messageInput);
    }
}
