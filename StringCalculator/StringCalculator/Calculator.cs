using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.Parsers;

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
    }
}
