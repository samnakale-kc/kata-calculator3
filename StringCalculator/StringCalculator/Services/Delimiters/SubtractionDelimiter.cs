using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public class SubtractionDelimiter : IDelimiter
    {
        private const string CustomDelimeterPrefix = "##";
        private const string NewLineCharacter = "\n";
        private readonly string[] _defaultDelimiters = { ",", "\n" };

        public string[] GetNumbersFromDelimitedString(string input)
        {
            var delimitersToUse = _defaultDelimiters;

            if (HasCustomDelimiterInFirstLine(input))
            {
                string customDelimiter = GetCustomDelimiterFromFirstLine(input);
                input = RemoveFirstLineFromInput(input);
                delimitersToUse = new[] { customDelimiter };
            }
            else if (HasCustomDelimiterInBody(input))
            {
                string customDelimiter = GetCustomDelimiterFromBody(input);
                delimitersToUse = new[] { customDelimiter };
            }

            return input.Split(delimitersToUse, StringSplitOptions.RemoveEmptyEntries);
        }

        private string RemoveFirstLineFromInput(string input)
        {
            if (!HasCustomDelimiterInFirstLine(input))
            {
                return input;
            }

            var sections = input.Split(NewLineCharacter).Skip(1); // Skip the first line directly
            return string.Join(NewLineCharacter, sections);
        }

        private string GetCustomDelimiterFromFirstLine(string input)
        {
            return input.Split(NewLineCharacter)[0].Replace(CustomDelimeterPrefix, string.Empty);
        }

        private string GetCustomDelimiterFromBody(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c) && !_defaultDelimiters.Contains(c.ToString()))
                {
                    return c.ToString();
                }
            }

            return string.Empty; // This should not occur under normal conditions
        }

        private bool HasCustomDelimiterInBody(string input)
        {
            return input.Any(c => !char.IsDigit(c) && !_defaultDelimiters.Contains(c.ToString()));
        }

        private bool HasCustomDelimiterInFirstLine(string input)
        {
            return input.StartsWith(CustomDelimeterPrefix) && input.Contains(NewLineCharacter);
        }
    }
}
