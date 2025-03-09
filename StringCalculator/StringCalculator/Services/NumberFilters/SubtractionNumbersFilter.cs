using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.NumberFilters
{
    public class SubtractionNumbersFilter : IFilterNumbers
    {
        private const int MaximumAllowedNumber = 1000;

        public int[] FilterOutInvalidNumbers(int[] numbers)
        {
            List<int> numberGreaterThanLimit = GetNumbersGreaterThanLimit(numbers);

            if (numberGreaterThanLimit.Count > 0)
            {
                string numberGreaterThanLimitJoined = string.Join(',', numberGreaterThanLimit.ToArray());
                throw new Exception("Numbers greater than 1000 not allowed: " + numberGreaterThanLimitJoined);
            }

            return numbers;
        }

        private List<int> GetNumbersGreaterThanLimit(int[] numbers)
        {
            List<int> result = new List<int>();
            foreach (int number in numbers)
            {
                if (number > MaximumAllowedNumber)
                {
                    result.Add(number);
                }
            }
            return result;
        }
    }
}
