using Moq;
using StringCalculator.Services.Delimiters;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Parsers
{
    public class SubtractionStringParserTests
    {
        private readonly SubtractionStringParser _stringParser;
        private readonly Mock<IDelimiter> _delimiterServiceMock;

        public SubtractionStringParserTests()
        {
            _delimiterServiceMock = new Mock<IDelimiter>();
            _stringParser = new SubtractionStringParser(_delimiterServiceMock.Object);
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
        [InlineData("1", new string[] { "1" }, new int[] { 1 })]
        [InlineData("1,2", new string[] { "1", "2" }, new int[] { 1, 2 })]
        public void GIVEN_OneOrTwoNumbers_WHEN_Parsing_THEN_ReturnListOfNumbers(string numbers, string[] expectedDelimiterServiceResponse, int[] expectedResult)
        {
            // Arrange
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(numbers)).Returns(expectedDelimiterServiceResponse);

            // Act
            int[] result = _stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputSeparatedByNewLine_WHEN_Parsing_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "1\n2,3";
            string[] expectedDelimiterServiceResponse = new string[] { "1", "2", "3" };
            int[] expectedResult = new int[] { 1, 2, 3 };
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(input)).Returns(expectedDelimiterServiceResponse);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterDefinedOnFirstLine_WHEN_Parsing_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "##;\n1;2";
            int[] expectedResult = new int[] { 1, 2 };
            string[] expectedDelimiterServiceResponse = new string[] { "1", "2" };
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(input)).Returns(expectedDelimiterServiceResponse);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterWithoutFirstLine_WHEN_Parsing_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "1;2";
            int[] expectedResult = new int[] { 1, 2 };
            string[] expectedDelimiterServiceResponse = new string[] { "1", "2" };
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(input)).Returns(expectedDelimiterServiceResponse);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumber_WHEN_Parsing_THEN_ConvertNegativeNumbersToPositiveNumbersAndReturnDifference()
        {
            // Arrange
            string input = "10;-2";
            int[] expectedResult = new int[] { 10, 2 };
            string[] expectedNumbersFromDelimeterService = ["10", "-2"];
            _delimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(input)).Returns(expectedNumbersFromDelimeterService);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
