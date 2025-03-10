using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services.Delimiters
{
    public class SubtractionDelimiter : IDelimiter
    {
        private const string CustomDelimeterNewLineStartString = @"##";
        private const string NewLineCharacter = "\n";
        private const string MultiCharacterDelimeterStartString = "[";
        private const string MultiCharacterDelimeterEndString = "]";

        private const string StartOfDelimeterStartIdentifier = "<";
        private const string EndOfDelimeterEndIdentifier = ">";
        private const int PositionOfStartDelimeterIdentifier = 0;
        private const int PositionOfEndDelimeterIdentifier = 2;

        private readonly string[] _defaultdelimeters = [",", "\n"];

        public string[] GetNumbersFromDelimitedString(string input)
        {
            bool inputHasFirstLineWithDelimeter = DoesInputHaveFirstLineWithDelimeter(input);
            bool inputHasCustomDelimeter = DoesInputHaveCustomDelimeter(input);
            bool inputHasMultipleDelimeters = DoesInputHaveMultipleDelimeters(input);
            bool inputDeclaresDelimeterSeparators = DoesInputHaveDelimeterSeparatorIdentifiers(input);

            string[] delimetersToSplitStringBy = GetDelimitersToSplitStringBy(
                input, 
                inputDeclaresDelimeterSeparators, 
                inputHasFirstLineWithDelimeter, 
                inputHasCustomDelimeter, 
                inputHasMultipleDelimeters);

            if (inputHasMultipleDelimeters || inputHasFirstLineWithDelimeter || inputDeclaresDelimeterSeparators)
            {
                input = RemoveFirstLineFromCustomDelimetedInput(input);
            }

            return input.Split(delimetersToSplitStringBy, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] GetDelimitersToSplitStringBy(string input, bool inputDeclaresDelimeterSeparators, bool inputHasFirstLineWithDelimeter, bool inputHasCustomDelimeter, bool inputHasMultipleDelimeters)
        {
            if (inputDeclaresDelimeterSeparators)
            {
                return GetCustomDelimetersFromStringWithDelimeterIdentifiers(input);
            }

            if (!inputHasFirstLineWithDelimeter && inputHasCustomDelimeter)
            {
                return new string[] { GetCustomDelimiterFromInputWithoutFirstLine(input) };
            }
            
            if (inputHasMultipleDelimeters)
            {
                return GetCustomDelimetersFromInputWithMuliDelimeters(input);
            }
            
            if (inputHasFirstLineWithDelimeter)
            {
                return new string[] { GetCustomDelimeterFromFirstLine(input) };
            }

            return _defaultdelimeters;
        }

        private static string[] GetCustomDelimetersFromStringWithDelimeterIdentifiers(string input)
        {
            string firstLine = input.Split(NewLineCharacter)[0];
            char[] firstLineCharacters = firstLine.ToCharArray();
            char delimeterStartIdentifier = firstLineCharacters[PositionOfStartDelimeterIdentifier + 1];
            char delimeterEndIdentifier = firstLineCharacters[PositionOfEndDelimeterIdentifier + 1];

            string delimeterDeclarations = firstLine.Substring(PositionOfEndDelimeterIdentifier + 2)
                .Replace(CustomDelimeterNewLineStartString, string.Empty);

            string[] result = delimeterDeclarations.Split(delimeterStartIdentifier, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Replace(delimeterEndIdentifier.ToString(), string.Empty);
            }

            return result;
        }

        private bool DoesInputHaveDelimeterSeparatorIdentifiers(string input)
        {
            return input.StartsWith(StartOfDelimeterStartIdentifier)
                && input.Length > PositionOfEndDelimeterIdentifier
                && input.ToArray()[PositionOfEndDelimeterIdentifier].ToString() == EndOfDelimeterEndIdentifier;
        }

        private bool DoesInputHaveMultipleDelimeters(string input)
        {
            string startsOfDelimeterLine = CustomDelimeterNewLineStartString + MultiCharacterDelimeterStartString;
            string endOfDelimeterLine = MultiCharacterDelimeterEndString + NewLineCharacter;

            return input.StartsWith(startsOfDelimeterLine) && input.Contains(endOfDelimeterLine);
        }

        public string[] GetCustomDelimetersFromInputWithMuliDelimeters(string input)
        {
            string firstLine = input.Split(NewLineCharacter)[0].Replace(CustomDelimeterNewLineStartString, string.Empty);

            string[] delimeters = firstLine.Split(MultiCharacterDelimeterEndString, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < delimeters.Length; i++)
            {
                string delimeter = delimeters[i];
                string cleanDelimeter = delimeter.Replace(MultiCharacterDelimeterStartString, string.Empty).Replace(MultiCharacterDelimeterEndString, string.Empty);

                delimeters[i] = cleanDelimeter;
            }

            return delimeters;
        }

        private string RemoveFirstLineFromCustomDelimetedInput(string input)
        {
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

        private string GetCustomDelimiterFromInputWithoutFirstLine(string input)
        {
            string delimiter = string.Empty;

            foreach (char currentInputCharacter in input)
            {
                bool currentCharacterIsNotNumberAndNotNegativeSign = !char.IsDigit(currentInputCharacter) && currentInputCharacter != '-';

                if (!string.IsNullOrEmpty(delimiter) && !currentCharacterIsNotNumberAndNotNegativeSign)
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
                if (!char.IsDigit(currentInputCharacter) && !currentCharIsNotContainedInDefaultDelimeters && currentInputCharacter != '-')
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
