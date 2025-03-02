using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Parsers
{
    public class StringParser : IStringParser
    {
        private readonly int[] _defaultListWhenEmptyString = new int[0];

        public int[] Parse(string input)
        {
            string cleanNumbers = input.Trim();

            if (string.IsNullOrEmpty(cleanNumbers))
            {
                return _defaultListWhenEmptyString;
            }

            string[] numbersList = cleanNumbers.Split(',');
            int listLength = numbersList.Length;
            int[] numbers = new int[listLength];

            for (int i = 0; i < listLength; i++)
            {
                int number = int.Parse(numbersList[i]);
                numbers[i] = number;
            }

            return numbers;
        }
    }
}
