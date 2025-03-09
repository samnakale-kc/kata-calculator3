using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public class SubtractionDelimiter : IDelimiter
    {
        private const string CustomDelimeterNewLineStartString = "##";
        private const string NewLineCharacter = "\n";
        private const string MultiCharacterDelimeterStartString = "[";
        private const string MultiCharacterDelimeterEndString = "]";

        private readonly string[] _defaultdelimeters = [",", "\n"];

        public string[] GetNumbersFromDelimitedString(string input)
        {
            string[] delimetersToSplitStringBy = _defaultdelimeters;
            bool inputHasFirstLineWithDelimeter = DoesInputHaveFirstLineWithDelimeter(input);
            bool inputHasCustomDelimeter = DoesInputHaveCustomDelimeter(input);

            if (!inputHasFirstLineWithDelimeter && inputHasCustomDelimeter)
            {
                string customDelimeter = GetCustomDelimeterFromInputWithoutFirstLine(input);
                delimetersToSplitStringBy = [customDelimeter];
            }
            else if (inputHasFirstLineWithDelimeter)
            {
                string customDelimeter = GetCustomDelimeterFromFirstLine(input);
                input = RemoveFirstLineFromCustomDelimetedInput(input);
                delimetersToSplitStringBy = [customDelimeter];
            }

            return input.Split(delimetersToSplitStringBy, StringSplitOptions.RemoveEmptyEntries);
        }

        private string RemoveFirstLineFromCustomDelimetedInput(string input)
        {
            if (!DoesInputHaveFirstLineWithDelimeter(input))
            {
                return input;
            }

            var stringSections = input.Split(NewLineCharacter).ToList();
            stringSections.RemoveAt(0); // Removes the first line directly

            return string.Join(NewLineCharacter, stringSections);
        }

        private string GetCustomDelimeterFromFirstLine(string input)
        {
            return input.Split(NewLineCharacter)[0]
                .Replace(CustomDelimeterNewLineStartString, string.Empty)
                .Replace(MultiCharacterDelimeterEndString, string.Empty)
                .Replace(MultiCharacterDelimeterStartString, string.Empty);
        }

        private string GetCustomDelimeterFromInputWithoutFirstLine(string input)
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

        private bool DoesInputHaveCustomDelimeter(string input)
        {
            var defaultDelimetersList = _defaultdelimeters.ToList();

            foreach (char currentInputCharacter in input)
            {
                bool currentCharIsNotContainedInDefaultDelimeters = defaultDelimetersList.Contains(currentInputCharacter.ToString());
                if (!char.IsDigit(currentInputCharacter) && !currentCharIsNotContainedInDefaultDelimeters)
                {
                    return true;
                }
            }

            return false;
        }

        private bool DoesInputHaveFirstLineWithDelimeter(string input)
        {
            return input.StartsWith(CustomDelimeterNewLineStartString) && input.Contains(NewLineCharacter);
        }
    }
}
