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
        private const string StartOfDelimiterStartIdentifier = "<";
        private const string EndOfDelimiterEndIdentifier = ">";

        private readonly string[] _defaultDelimiters = [",", "\n"];

        public string[] GetNumbersFromDelimitedString(string input)
        {
            bool inputHasFirstLineWithDelimiter = DoesInputHaveFirstLineWithDelimiter(input);
            bool inputHasCustomDelimiter = DoesInputHaveCustomDelimiter(input);
            bool inputHasMultipleDelimiters = DoesInputHaveMultipleDelimiters(input);
            bool inputDeclaresDelimiterSeparators = DoesInputHaveDelimiterSeparatorIdentifiers(input);

            string[] delimitersToSplitStringBy = GetDelimitersToSplitStringBy(
                input,
                inputDeclaresDelimiterSeparators,
                inputHasFirstLineWithDelimiter,
                inputHasCustomDelimiter,
                inputHasMultipleDelimiters);

            if (inputHasMultipleDelimiters || inputHasFirstLineWithDelimiter || inputDeclaresDelimiterSeparators)
            {
                input = RemoveFirstLineFromCustomDelimitedInput(input);
            }

            return input.Split(delimitersToSplitStringBy, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] GetDelimitersToSplitStringBy(string input, bool inputDeclaresDelimiterSeparators, bool inputHasFirstLineWithDelimiter, bool inputHasCustomDelimiter, bool inputHasMultipleDelimiters)
        {
            if (inputDeclaresDelimiterSeparators)
            {
                return GetCustomDelimitersFromStringWithDelimiterIdentifiers(input).ToArray();
            }

            if (!inputHasFirstLineWithDelimiter && inputHasCustomDelimiter)
            {
                return new string[] { GetCustomDelimiterFromInputWithoutFirstLine(input) };
            }

            if (inputHasMultipleDelimiters)
            {
                return GetCustomDelimitersFromInputWithMultipleDelimiters(input).ToArray();
            }

            if (inputHasFirstLineWithDelimiter)
            {
                return new string[] { GetCustomDelimiterFromFirstLine(input) };
            }

            return _defaultDelimiters;
        }

        private static IEnumerable<string> GetCustomDelimitersFromStringWithDelimiterIdentifiers(string input)
        {
            string firstLine = input.Split(NewLineCharacter)[0];
            char[] firstLineCharacters = firstLine.ToCharArray();
            int positionOfStartDelimiterIdentifier = input.IndexOf(StartOfDelimiterStartIdentifier);
            int positionOfEndDelimiterIdentifier = input.IndexOf(EndOfDelimiterEndIdentifier);

            char delimiterStartIdentifier = firstLineCharacters[positionOfStartDelimiterIdentifier + 1];
            char delimiterEndIdentifier = firstLineCharacters[positionOfEndDelimiterIdentifier + 1];

            string delimiterDeclarationsPrefixedWithCustomDelimiterNewLineStartString = firstLine.Substring(positionOfEndDelimiterIdentifier + 2);
            string delimiterDeclarations = delimiterDeclarationsPrefixedWithCustomDelimiterNewLineStartString.Replace(CustomDelimiterNewLineStartString, string.Empty);

            string[] delimiters = delimiterDeclarations.Split(delimiterStartIdentifier, StringSplitOptions.RemoveEmptyEntries);

            foreach (string delimiter in delimiters)
            {
                yield return delimiter.Replace(delimiterEndIdentifier.ToString(), string.Empty);
            }
        }

        private bool DoesInputHaveDelimiterSeparatorIdentifiers(string input)
        {
            int positionOfEndDelimiterIdentifier = input.IndexOf(EndOfDelimiterEndIdentifier);
            return input.StartsWith(StartOfDelimiterStartIdentifier)
                && input.Length > positionOfEndDelimiterIdentifier
                && input.ToArray()[positionOfEndDelimiterIdentifier].ToString() == EndOfDelimiterEndIdentifier;
        }

        private bool DoesInputHaveMultipleDelimiters(string input)
        {
            string startsOfDelimiterLine = CustomDelimiterNewLineStartString + MultiCharacterDelimiterStartString;
            string endOfDelimiterLine = MultiCharacterDelimiterEndString + NewLineCharacter;

            return input.StartsWith(startsOfDelimiterLine) && input.Contains(endOfDelimiterLine);
        }

        public IEnumerable<string> GetCustomDelimitersFromInputWithMultipleDelimiters(string input)
        {
            string firstLinePrefixedWithCustomDelimiterNewLineStartString = input.Split(NewLineCharacter)[0];
            string firstLine = firstLinePrefixedWithCustomDelimiterNewLineStartString.Replace(CustomDelimiterNewLineStartString, string.Empty);
            string[] delimiters = firstLine.Split(MultiCharacterDelimiterEndString, StringSplitOptions.RemoveEmptyEntries);

            foreach (string delimiter in delimiters)
            {
                yield return delimiter.Replace(MultiCharacterDelimiterStartString, string.Empty).Replace(MultiCharacterDelimiterEndString, string.Empty);
            }
        }

        private string RemoveFirstLineFromCustomDelimitedInput(string input)
        {
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
            const char negativeSignCharacter = '-';

            foreach (char currentInputCharacter in input)
            {
                bool currentCharacterIsNotNumberAndNotNegativeSign = !char.IsDigit(currentInputCharacter) && currentInputCharacter != negativeSignCharacter;

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

        private bool DoesInputHaveCustomDelimiter(string input)
        {
            var defaultDelimitersList = _defaultDelimiters.ToList();

            foreach (char currentInputCharacter in input)
            {
                bool currentCharIsNotContainedInDefaultDelimiters = defaultDelimitersList.Contains(currentInputCharacter.ToString());
                if (!char.IsDigit(currentInputCharacter) && !currentCharIsNotContainedInDefaultDelimiters && currentInputCharacter != '-')
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
    }
}
