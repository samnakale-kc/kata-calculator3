using StringCalculator.Services.Delimiters;

namespace StringCalculator.Services.Parsers
{
    public class StringParser : IStringParser
    {
        private readonly int[] _defaultListWhenEmptyString = new int[0];

        private readonly IDelimiter _delimeterService;

        public StringParser(IDelimiter delimeterService)
        {
            _delimeterService = delimeterService;
        }

        public int[] Parse(string input)
        {
            string cleanNumbers = input.Trim();

            if (string.IsNullOrEmpty(cleanNumbers))
            {
                return _defaultListWhenEmptyString;
            }

            string[] numbersList = _delimeterService.GetNumbersFromDelimitedString(cleanNumbers);

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
