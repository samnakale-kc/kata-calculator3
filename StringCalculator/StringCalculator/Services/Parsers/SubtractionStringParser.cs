using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.Delimiters;
using StringCalculator.Services.NumberFilters;

namespace StringCalculator.Services.Parsers
{
    public class SubtractionStringParser : IStringParser
    {
        private readonly int[] _defaultListWhenEmptyString = Array.Empty<int>();
        private readonly IDelimiter _delimiterService;
        private readonly IFilterNumbers _numberFilterService;

        public SubtractionStringParser(IDelimiter delimiterService, IFilterNumbers numberFilterService)
        {
            _delimiterService = delimiterService;
            _numberFilterService = numberFilterService;
        }

        public int[] Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return _defaultListWhenEmptyString;
            }

            string[] numbersArray = _delimiterService.GetNumbersFromDelimitedString(input);

            int[] numbers = ConvertStringsToNumbers(numbersArray);

            return _numberFilterService.FilterOutInvalidNumbers(numbers);
        }

        private int[] ConvertStringsToNumbers(string[] numbersArray)
        {
            int listLength = numbersArray.Length;
            int[] numbers = new int[listLength];

            for (int i = 0; i < listLength; i++)
            {
                int? currentNumber = ConvertStringToNumber(numbersArray[i]);

                if (currentNumber.HasValue)
                {
                    numbers[i] = currentNumber.Value;
                }
            }

            return numbers;
        }

        private static int? ConvertStringToNumber(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    result.Append(c);
                    continue;
                }

                int? letterToNumber = ConvertLetterToNumber(c);
                if (letterToNumber.HasValue)
                {
                    result.Append(letterToNumber.Value);
                }
            }

            return result.Length > 0 ? (int?)int.Parse(result.ToString()) : null;
        }

        private static int? ConvertLetterToNumber(char letter)
        {
            return letter >= 'a' && letter <= 'j' ? letter - 'a' + 1 : (int?)null;
        }
    }
}
