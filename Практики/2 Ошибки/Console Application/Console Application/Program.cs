using System.Globalization;

namespace Percents
{
    class Program
    {
        static void Main()
        {
            string userInput = Console.ReadLine();

            Console.WriteLine(Calculate(userInput));
        }

        public static double Calculate(string userInput)
        {
            var threeNumbers = userInput.Split(" ");
            double summ = double.Parse(threeNumbers[0], CultureInfo.InvariantCulture);
            double yearPercent = double.Parse(threeNumbers[1], CultureInfo.InvariantCulture);
            double countOfMonth = double.Parse(threeNumbers[2], CultureInfo.InvariantCulture);

            double monthPercent = (yearPercent / 12) / 100;

            for (int i = 0; i < countOfMonth; i++)
            {
                summ = summ * (1 + monthPercent);
            }

            return summ;
        }
    }
}