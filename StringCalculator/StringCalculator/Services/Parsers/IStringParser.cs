using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Parsers
{
    public interface IStringParser
    {
        int[] Parse(string input);
    }
}
