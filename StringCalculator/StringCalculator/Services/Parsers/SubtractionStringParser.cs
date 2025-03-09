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
        private readonly int[] _defaultListWhenEmptyString = new int[0];
        private readonly IDelimiter _delimiterService;
        private readonly IFilterNumbers _numberFilterService;

        public SubtractionStringParser(IDelimiter delimiterService, IFilterNumbers numbersFilterService) 
        {
            _delimiterService = delimiterService;
            _numberFilterService = numbersFilterService;
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

            return _numberFilterService.FilterOutInvalidNumbers(numbers);
        }
    }
}
