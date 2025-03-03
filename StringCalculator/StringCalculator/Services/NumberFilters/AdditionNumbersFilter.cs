using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.NumberFilters
{
    public class AdditionNumbersFilter : IFilterNumbers
    {
        public int[] FilterOutInvalidNumbers(int[] numbers)
        {
            List<int> negetiveNumbers = GetNegativeNumbers(numbers);

            if (negetiveNumbers.Count > 0)
            {
                throw new ArgumentException("negatives not allowed " + string.Join(",", negetiveNumbers.ToArray()));
            }

            return numbers;
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
