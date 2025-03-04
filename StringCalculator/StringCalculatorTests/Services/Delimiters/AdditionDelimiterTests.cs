
using StringCalculator.Services;
using StringCalculator.Services.Delimiters;

namespace StringCalculatorTests.Services.Delimiters
{
    public class AdditionDelimiterTests
    {
        private AdditionDelimiter _additionDelimeter;

        public AdditionDelimiterTests()
        {
            _additionDelimeter = new AdditionDelimiter();
        }

        [Theory]
        [InlineData("1\n2,3", new string[] { "1", "2", "3" })]
        [InlineData("1\n2", new string[] { "1", "2"})]
        public void GIVEN_AnInputSeparatedByNewLineAndOrComma_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers(string numbers, string[] expectedResult)
        {
            // Act
            string[] result = _additionDelimeter.GetNumbersFromDelimitedString(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimetedInputWithFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "//;\n1;2";
            string[] expectedResult = new string[] { "1", "2" };

            // Act
            string[] result = _additionDelimeter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimetedInputWithoutFirstLine_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "1;2";
            string[] expectedResult = new string[] { "1", "2" };

            // Act
            string[] result = _additionDelimeter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumbers_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "1;-2;-5";
            string[] expectedResult = new string[] { "1", "-2", "-5" };

            // Act
            string[] result = _additionDelimeter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithNumbersGreaterThanOneThousand_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "1;-2;2000";
            string[] expectedResult = new string[] { "1", "-2", "2000" };

            // Act
            string[] result = _additionDelimeter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithDelimiterOfAnyLength_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "//***\n1***2***3";
            string[] expectedResult = new string[] { "1", "2", "3" };

            // Act
            string[] result = _additionDelimeter.GetNumbersFromDelimitedString(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
