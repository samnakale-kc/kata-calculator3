using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public interface IDelimiter
    {
        string[] GetNumbersFromDelimitedString(string input);
    }
}
