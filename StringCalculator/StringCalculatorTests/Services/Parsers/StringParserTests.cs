
using StringCalculator;
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
    }
}
