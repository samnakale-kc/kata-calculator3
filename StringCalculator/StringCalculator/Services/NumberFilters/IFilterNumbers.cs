using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.NumberFilters
{
    public interface IFilterNumbers
    {
        public int[] FilterOutInvalidNumbers(int[] numbers);
    }
}
