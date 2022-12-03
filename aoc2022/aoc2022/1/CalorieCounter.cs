namespace aoc2022._1
{
    internal class CalorieCounter
    {
        private readonly string _inputFilePath;

        public CalorieCounter()
        {
            _inputFilePath = Directory.GetCurrentDirectory() + "\\1\\1_input.txt";
        }

        public int CalculateElfWithMaxCalories()
        {
            var maxCalorieCount = 0;
            foreach(var calorieCount in GetCalorieCountFromFile(_inputFilePath))
            {
                if(calorieCount > maxCalorieCount)
                    maxCalorieCount = calorieCount;
            }

            return maxCalorieCount;
        }

        public int CalculateSumOfTopThreeCalorieCount()
        {
            var maxCalories = new int[3];
            Array.Fill(maxCalories, 0);
            
            foreach (var parsedCalorieCount in GetCalorieCountFromFile(_inputFilePath))
            {
                ReplaceLowestValueIfLower(maxCalories, parsedCalorieCount);
            }
            
            return maxCalories.Sum();
        }

        private static void ReplaceLowestValueIfLower(int[] maxCalories, int newCalorieCount)
        {
            if (newCalorieCount < maxCalories[0])
                return;

            maxCalories[0] = newCalorieCount;
            //With sorting we can always guarantee that the smallest is first
            //Should be faster to do sort every now and then compared to always iterating the three elements?
            Array.Sort(maxCalories);
        }

        private static IEnumerable<int> GetCalorieCountFromFile(string filePathAndName)
        {
            var calorieCount = 0;
            foreach(var line in File.ReadLines(filePathAndName))
            {
                if (string.IsNullOrEmpty(line))
                {
                    yield return calorieCount;
                    calorieCount = 0;
                }

                if (int.TryParse(line, out int parsedCalorieCount))
                    calorieCount += parsedCalorieCount;
            }
        }
    }
}
