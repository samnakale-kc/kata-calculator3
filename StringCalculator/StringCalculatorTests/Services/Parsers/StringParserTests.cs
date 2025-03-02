using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Parsers
{
    public class StringParserTests
    {
        StringParser _stringParser;
        public StringParserTests()
        {
            _stringParser = new StringParser();
        }

        [Fact]
        public void GIVEN_EmptyString_WHEN_Parsing_THEN_ReturnAListWithNoNumbers()
        {
            // Arrange
            string numbers = string.Empty;
            int[] expectedResult = new int[0];

            // Act
            int[] result = _stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1", new int[] { 1 })]
        [InlineData("1,2", new int[] { 1, 2 })]
        public void GIVEN_OneOrTwoNumbers_WHEN_Parsing_THEN_ReturnListOfNumbers(string numbers, int[] expectedResult)
        {
            // Act
            int[] result = _stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1\n2,3", new int[] { 1, 2, 3 })]
        [InlineData("1\n2", new int[] { 1, 2 })]
        public void GIVEN_AnInputSeparatedByNewLine_WHEN_Parsing_THEN_ReturnListOfNumbers(string numbers, int[] expectedResult)
        {
            // Act
            int[] result = _stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimetedInput_WHEN_Parsing_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "//;\n1;2";
            int[] expectedResult = new int[] { 1, 2 };

            // Act
            int[] result = _stringParser.Parse(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimetedInputWithoutFirstLine_WHEN_Parsing_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "1;2";
            int[] expectedResult = new int[] { 1, 2 };

            // Act
            int[] result = _stringParser.Parse(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
