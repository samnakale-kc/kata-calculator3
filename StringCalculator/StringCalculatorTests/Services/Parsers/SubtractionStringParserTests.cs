using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Parsers
{
    public class SubtractionStringParserTests
    {
        [Fact]
        public void GIVEN_EmptyString_WHEN_Parsing_THEN_ReturnAListWithNoNumbers()
        {
            // Arrange
            string numbers = string.Empty;
            int[] expectedResult = new int[0];
            var stringParser = new SubtractionStringParser();

            // Act
            int[] result = stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1", new int[] { 1 })]
        [InlineData("1,2", new int[] { 1, 2 })]
        public void GIVEN_OneOrTwoNumbers_WHEN_Parsing_THEN_ReturnListOfNumbers(string numbers, int[] expectedResult)
        {
            // Arrange
            var stringParser = new SubtractionStringParser();

            // Act
            int[] result = stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputSeparatedByNewLine_WHEN_Parsing_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "1\n2,3";
            int[] expectedResult = new int[] { 1, 2, 3 };
            var stringParser = new SubtractionStringParser();

            // Act
            int[] result = stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
