using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            const int defaultSum = 0;

            string cleanNumbers = numbers.Trim();

            if (cleanNumbers.Length == 0)
            {
                return defaultSum;
            }

            string[] numbersList = cleanNumbers.Split(',');
            int sum = 0;

            for (int i = 0; i < numbersList.Length; i++)
            {
                sum = sum + int.Parse(numbersList[i]);
            }

            return sum;
        }
    }
}
