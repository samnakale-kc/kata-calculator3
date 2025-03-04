using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public class AdditionDelimiter : IDelimiter
    {
        private const string CustomDelimiterPrefix = "//";
        private const string NewLineCharacter = "\n";
        private readonly string[] _defaultDelimiters = { ",", "\n" };
        private const string MultiCharacterDelimiterStartString = "[";
        private const string MultiCharacterDelimiterEndString = "]";

        public string[] GetNumbersFromDelimitedString(string input)
        {
            string[] delimitersToSplitBy = _defaultDelimiters;

            if (InputHasMultipleDelimiters(input))
            {
                delimitersToSplitBy = GetCustomDelimitersFromInputWithMultipleDelimiters(input);
                input = ExtractInputWithoutFirstLine(input);
            }
            else if(HasCustomDelimiterLine(input))
            {
                delimitersToSplitBy = new string[] { GetCustomDelimiterFromFirstLine(input) };
                input = ExtractInputWithoutFirstLine(input);                
            }
            else if (HasCustomDelimiter(input))
            {
                delimitersToSplitBy = new string[] { GetCustomDelimiterFromInput(input) };
            }

            return input.Split(delimitersToSplitBy, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool InputHasMultipleDelimiters(string input)
        {
            string startOfDelimiterLine = CustomDelimiterPrefix + MultiCharacterDelimiterStartString;
            string endOfDelimiterLine = MultiCharacterDelimiterEndString + NewLineCharacter;

            return input.StartsWith(startOfDelimiterLine) && input.Contains(endOfDelimiterLine);
        }

        private string[] GetCustomDelimitersFromInputWithMultipleDelimiters(string input)
        {
            string firstLine = input.Split(NewLineCharacter)[0];
            string firstLineWithoutPrefix = firstLine.Replace(CustomDelimiterPrefix, string.Empty);

            string[] delimiters = firstLineWithoutPrefix.Split(MultiCharacterDelimiterEndString, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < delimiters.Length; i++)
            {
                string delimiter = delimiters[i];
                string cleanDelimeter = delimiter.Replace(MultiCharacterDelimiterStartString, string.Empty).Replace(MultiCharacterDelimiterEndString, string.Empty);
                delimiters[i] = cleanDelimeter;
            }

            return delimiters;
        }

        private bool HasCustomDelimiterLine(string input)
        {
            return input.StartsWith(CustomDelimiterPrefix) && input.Contains(NewLineCharacter);
        }

        private string GetCustomDelimiterFromFirstLine(string input)
        {
            return input.Split(NewLineCharacter)[0].Replace(CustomDelimiterPrefix, string.Empty);
        }

        private string ExtractInputWithoutFirstLine(string input)
        {
            var stringSections = input.Split(NewLineCharacter).ToList();
            stringSections.RemoveAt(0); // Removes the first line directly

            return string.Join(NewLineCharacter, stringSections);
        }

        private bool HasCustomDelimiter(string input)
        {
            foreach (char currentInputCharacter in input)
            {
                if (!char.IsDigit(currentInputCharacter) && !_defaultDelimiters.Contains(currentInputCharacter.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetCustomDelimiterFromInput(string input)
        {
            foreach (char currentInputCharacter in input)
            {
                if (!char.IsDigit(currentInputCharacter))
                {
                    return currentInputCharacter.ToString();
                }
            }

            return string.Empty; // Technically, we should never reach here
        }
    }
}
