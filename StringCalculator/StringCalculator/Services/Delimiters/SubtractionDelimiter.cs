using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public class SubtractionDelimiter : IDelimiter
    {
        public string[] GetNumbersFromDelimitedString(string input)
        {
            string[] delimeters = [",", "\n"];
            string[] numbersArray = input.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            return numbersArray;
        }
    }
}
