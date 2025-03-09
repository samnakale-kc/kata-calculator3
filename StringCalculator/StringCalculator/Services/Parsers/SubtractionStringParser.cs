using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Parsers
{
    public class SubtractionStringParser : IStringParser
    {
        private readonly int[] _defaultListWhenEmptyString = new int[0];

        public int[] Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return _defaultListWhenEmptyString;
            }

            string[] delimeters = [",", "\n"];
            string[] numbersArray = input.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

            int listLength = numbersArray.Length;
            int[] numbers = new int[listLength];

            for (int i = 0; i < listLength; i++)
            {
                numbers[i] = int.Parse(numbersArray[i]);
            }

            return numbers;
        }
    }
}
