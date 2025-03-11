using StringCalculator.Services.Parsers;

namespace StringCalculator
{
    public class Calculator
    {
        private IParserFactory _stringParserFactory;

        public Calculator(IParserFactory parserFactory) 
        {
            _stringParserFactory = parserFactory;
        }

        public int Add(string numbers)
        {
            var parser = _stringParserFactory.Create(ParserType.ADDITION);
            int[] inputs = parser.Parse(numbers);
            int sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum = sum + inputs[i];
            }

            return sum;
        }

        public int Subtract(string numbers)
        {
            var parser = _stringParserFactory.Create(ParserType.SUBTRACTION);
            int[] numbersList = parser.Parse(numbers);

            int result = 0;

            foreach (int number in numbersList)
            {
                result -= number;
            }

            return result;
        }
    }
}
