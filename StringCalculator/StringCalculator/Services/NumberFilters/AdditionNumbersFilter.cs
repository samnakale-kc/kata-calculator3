using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.NumberFilters
{
    public class AdditionNumbersFilter : IFilterNumbers
    {
        private const int NumberLimit = 1000;

        public int[] FilterOutInvalidNumbers(int[] numbers)
        {
            List<int> negetiveNumbers = GetNegativeNumbers(numbers);

            if (negetiveNumbers.Count > 0)
            {
                throw new ArgumentException("negatives not allowed " + string.Join(",", negetiveNumbers.ToArray()));
            }

            int[] numberBelowLimit = FilterOutNumbersGreaterThanTheLimit(numbers);

            return numberBelowLimit;
        }

        private int[] FilterOutNumbersGreaterThanTheLimit(int[] numbers)
        {
            List<int> numbersAllowed = new List<int>();

            foreach (int number in numbers)
            {
                if (number <= NumberLimit)
                {
                    numbersAllowed.Add(number);
                }
            }

            return numbersAllowed.ToArray();
        }

        private static List<int> GetNegativeNumbers(int[] numbers)
        {
            List<int> negativeNumbers = new List<int>();

            foreach (int number in numbers)
            {
                if (number < 0)
                {
                    negativeNumbers.Add(number);
                }
            }

            return negativeNumbers;
        }
    }
}
