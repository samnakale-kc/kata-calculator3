using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.Delimiters;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Delimiters
{
    public class SubtractionDelimiterTests
    {
        private SubtractionDelimiter _subtractionDelimiter;

        public SubtractionDelimiterTests()
        {
            _subtractionDelimiter = new SubtractionDelimiter();
        }

        [Fact]
        public void GIVEN_AnInputDelimitedByComma_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers()
        {
            // Arrange
            string input = "1,2,3";
            string[] expectedResult = new string[] { "1", "2", "3" };

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_AnInputSeparatedByNewLineAndOrComma_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers()
        {
            // Arrange
            string input = "1\n2,3";
            string[] expectedResult = new string[] { "1", "2", "3" };

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterDefinedOnFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers()
        {
            // Arrange
            string input = "##;\n1;2";
            string[] expectedResult = new string[] { "1", "2" };

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterWithoutFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers()
        {
            // Arrange
            string input = "1;2;3";
            string[] expectedResult = new string[] { "1", "2", "3" };

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumber_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbersIncludingNegativeSign()
        {
            // Arrange
            string inputNumbers = "10;-2";
            string[] expectedResult = ["10", "-2"];

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimitedInputOfMoreThanOneCharacterWithFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "##;*;\n1;*;2";
            string[] expectedResult = ["1", "2"];

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimitedInputOfMoreThanOneCharacterWithoutFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "1;*;2";
            string[] expectedResult = ["1", "2"];

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimitedInputOfMoreThanOneDelimeterWithMoreThanOneCharacterWithFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "##[***][%%%]\n1***2%%%5";
            string[] expectedResult = ["1", "2", "5"];

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimitedInputOfMoreThanOneDelimeterWithFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "##[*][%]\n1*2%5";
            string[] expectedResult = ["1", "2", "5"];

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_AnInputWithLettersAsNumbers_WHEN_GettingNumbersFromDelimetedString_THEN_ReturnNumbersRepresentedByLetters()
        {
            // Arrange
            string inputNumbers = "##[*][%]\na*b%c";
            string[] expectedResult = ["a", "b", "c"];

            // Act
            string[] result = _subtractionDelimiter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
