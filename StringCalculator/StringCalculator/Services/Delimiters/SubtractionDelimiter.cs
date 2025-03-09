using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public class SubtractionDelimiter : IDelimiter
    {
        private const string CustomDelimiterNewLineStartString = "##";
        private const string NewLineCharacter = "\n";
        private const string MultiCharacterDelimiterStartString = "[";
        private const string MultiCharacterDelimiterEndString = "]";

        private readonly string[] _defaultDelimiters = [",", "\n"];

        public string[] GetNumbersFromDelimitedString(string input)
        {
            string[] delimitersToSplitStringBy = _defaultDelimiters;
            bool inputHasFirstLineWithDelimiter = DoesInputHaveFirstLineWithDelimiter(input);
            bool inputHasCustomDelimiter = DoesInputHaveCustomDelimiter(input);
            bool inputHasMultipleDelimeters = DoesInputHaveMultipleDelimeters(input);

            if (!inputHasFirstLineWithDelimiter && inputHasCustomDelimiter)
            {
                delimitersToSplitStringBy = new string[] { GetCustomDelimiterFromInputWithoutFirstLine(input) };
            }
            else if (inputHasMultipleDelimeters)
            {
                delimitersToSplitStringBy = GetCustomDelimitersFromInputWithMultipleDelimiters(input);
            }
            else if (inputHasFirstLineWithDelimiter)
            {
                delimitersToSplitStringBy = new string[] { GetCustomDelimiterFromFirstLine(input) };
            }

            if (inputHasFirstLineWithDelimiter || inputHasMultipleDelimeters)
            {
                input = RemoveFirstLineFromCustomDelimetedInput(input);
            }

            return input.Split(delimitersToSplitStringBy, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool DoesInputHaveMultipleDelimeters(string input)
        {
            string startsOfDelimeterLine = CustomDelimiterNewLineStartString + MultiCharacterDelimiterStartString;
            string endOfDelimeterLine = MultiCharacterDelimiterEndString + NewLineCharacter;

            return input.StartsWith(startsOfDelimeterLine) && input.Contains(endOfDelimeterLine);
        }

        private string RemoveFirstLineFromCustomDelimetedInput(string input)
        {
            if (!DoesInputHaveFirstLineWithDelimiter(input))
            {
                return input;
            }

            var stringSections = input.Split(NewLineCharacter).ToList();
            stringSections.RemoveAt(0); // Removes the first line directly

            return string.Join(NewLineCharacter, stringSections);
        }

        private string GetCustomDelimiterFromFirstLine(string input)
        {
            return input.Split(NewLineCharacter)[0]
                .Replace(CustomDelimiterNewLineStartString, string.Empty)
                .Replace(MultiCharacterDelimiterEndString, string.Empty)
                .Replace(MultiCharacterDelimiterStartString, string.Empty);
        }

        private string GetCustomDelimiterFromInputWithoutFirstLine(string input)
        {
            string delimiter = string.Empty;

            foreach (char currentInputCharacter in input)
            {
                bool currentCharacterIsNotNumberAndNotNegativeSign = !char.IsDigit(currentInputCharacter) && currentInputCharacter != '-';

                if (! string.IsNullOrEmpty(delimiter) && !currentCharacterIsNotNumberAndNotNegativeSign)
                {
                    break;
                }

                if (currentCharacterIsNotNumberAndNotNegativeSign)
                {
                    delimiter += currentInputCharacter.ToString();
                }
            }

            return delimiter;
        }

        private bool DoesInputHaveCustomDelimiter(string input)
        {
            var defaultDelimitersList = _defaultDelimiters.ToList();

            foreach (char currentInputCharacter in input)
            {
                bool currentCharIsNotContainedInDefaultDelimiters = defaultDelimitersList.Contains(currentInputCharacter.ToString());
                if (!char.IsDigit(currentInputCharacter) && !currentCharIsNotContainedInDefaultDelimiters)
                {
                    return true;
                }
            }

            return false;
        }

        private bool DoesInputHaveFirstLineWithDelimiter(string input)
        {
            return input.StartsWith(CustomDelimiterNewLineStartString) && input.Contains(NewLineCharacter);
        }

        private string[] GetCustomDelimitersFromInputWithMultipleDelimiters(string input)
        {
            string firstLine = input.Split(NewLineCharacter)[0].Replace(CustomDelimiterNewLineStartString, string.Empty);

            string[] delimiters = firstLine.Split(MultiCharacterDelimiterEndString, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < delimiters.Length; i++)
            {
                string delimiter = delimiters[i];
                string cleanDelimiter = delimiter.Replace(MultiCharacterDelimiterStartString, string.Empty).Replace(MultiCharacterDelimiterEndString, string.Empty);

                delimiters[i] = cleanDelimiter;
            }

            return delimiters;
        }
    }
}
