using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.Delimiters;

namespace StringCalculator.Services.Parsers
{
    public class SubtractionStringParser : IStringParser
    {
        private readonly int[] _defaultListWhenEmptyString = new int[0];
        private readonly IDelimiter _delimiterService;

        public SubtractionStringParser(IDelimiter delimiterService) 
        {
            _delimiterService = delimiterService;
        }

        public int[] Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return _defaultListWhenEmptyString;
            }

            string[] numbersArray = _delimiterService.GetNumbersFromDelimitedString(input);

            int listLength = numbersArray.Length;
            int[] numbers = new int[listLength];

            for (int i = 0; i < listLength; i++)
            {
                int currentNumber = int.Parse(numbersArray[i]);
                numbers[i] = Math.Abs(currentNumber);
            }

            return numbers;
        }
    }
}
