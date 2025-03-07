using StringCalculator.Services.Parsers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StringCalculator
{
    public class Calculator
    {
        private IStringParser _stringParser;

        public Calculator(IStringParser parser) 
        {
            _stringParser = parser;
        }

        public int Add(string numbers)
        {
            int[] inputs = _stringParser.Parse(numbers);
            int sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum = sum + inputs[i];
            }

            return sum;
        }

        public int Subtract(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            int result = 0;
            string[] numbersArray = numbers.Split(',');

            foreach (string number in numbersArray)
            {
                result -= int.Parse(number);
            }

            return result;
        }
    }
}
