// See https://aka.ms/new-console-template for more information
using aoc2022._1;
using aoc2022._2;

Console.WriteLine("Hello, World!");

var toRun = new RockPaperScissorsStrategy();
Console.WriteLine(toRun.CalculateScoreForStrategyBasedOnInputs());
Console.WriteLine(toRun.CalculateScoreForStrategyBasedOnOpponentAndOutcome());
Console.ReadLine();