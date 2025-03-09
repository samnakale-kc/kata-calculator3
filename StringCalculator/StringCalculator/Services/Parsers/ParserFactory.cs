using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.Delimiters;
using StringCalculator.Services.NumberFilters;

namespace StringCalculator.Services.Parsers
{
    public class ParserFactory: IParserFactory
    {
        public IStringParser Create(ParserType type)
        {
            return type switch
            {
                ParserType.ADDITION => new AdditionStringParser(new AdditionDelimiter(), new AdditionNumbersFilter()),
                ParserType.SUBTRACTION => new SubtractionStringParser(new SubtractionDelimiter(), new SubtractionNumbersFilter()),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
