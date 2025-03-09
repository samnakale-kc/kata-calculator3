using Moq;
using StringCalculator.Services.Delimiters;
using StringCalculator.Services.NumberFilters;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Parsers
{
    public class SubtractionStringParserTests
    {
        private readonly SubtractionStringParser _stringParser;
        private readonly Mock<IDelimiter> _delimiterServiceMock;
        private readonly Mock<IFilterNumbers> _numbersFilterServiceMock;

        public SubtractionStringParserTests()
        {
            _delimiterServiceMock = new Mock<IDelimiter>();
            _numbersFilterServiceMock = new Mock<IFilterNumbers>();

            _stringParser = new SubtractionStringParser(_delimiterServiceMock.Object, _numbersFilterServiceMock.Object);
        }

        [Fact]
        public void GIVEN_EmptyString_WHEN_ParsingNumbers_THEN_ReturnAListWithNoNumbers()
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
        public void GIVEN_OneOrTwoNumbers_WHEN_ParsingNumbers_THEN_ReturnListOfNumbers(string numbers, string[] expectedDelimiterServiceResponse, int[] expectedResult)
        {
            // Arrange
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(numbers)).Returns(expectedDelimiterServiceResponse);
            _numbersFilterServiceMock.Setup(x => x.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputSeparatedByNewLine_WHEN_ParsingNumbers_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "1\n2,3";
            string[] expectedDelimiterServiceResponse = new string[] { "1", "2", "3" };
            int[] expectedResult = new int[] { 1, 2, 3 };
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(input)).Returns(expectedDelimiterServiceResponse);
            _numbersFilterServiceMock.Setup(x => x.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterDefinedOnFirstLine_WHEN_ParsingNumbers_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "##;\n1;2";
            int[] expectedResult = new int[] { 1, 2 };
            string[] expectedDelimiterServiceResponse = new string[] { "1", "2" };
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(input)).Returns(expectedDelimiterServiceResponse);
            _numbersFilterServiceMock.Setup(x => x.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterWithoutFirstLine_WHEN_ParsingNumbers_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "1;2";
            int[] expectedResult = new int[] { 1, 2 };
            string[] expectedDelimiterServiceResponse = new string[] { "1", "2" };
            _delimiterServiceMock.Setup(x => x.GetNumbersFromDelimitedString(input)).Returns(expectedDelimiterServiceResponse);
            _numbersFilterServiceMock.Setup(x => x.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumber_WHEN_ParsingNumbers_THEN_ConvertNegativeNumbersToPositiveNumbersAndReturnDifference()
        {
            // Arrange
            string input = "10;-2";
            int[] expectedResult = new int[] { 10, 2 };
            string[] expectedNumbersFromDelimeterService = ["10", "-2"];
            _delimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(input)).Returns(expectedNumbersFromDelimeterService);
            _numbersFilterServiceMock.Setup(x => x.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithNumbersGreaterThanOneThousand_WHEN_ParsingNumbers_THEN_ThrowAnException()
        {
            // Arrange
            string inputNumbers = "1001;-2;-5";
            string[] expectedNumbersFromDelimeterService = ["1001", "-2", "-5"];
            string expectedErrorMessage = "Numbers greater than 1000 not allowed: 1001";
            _delimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(inputNumbers)).Returns(expectedNumbersFromDelimeterService);
            _numbersFilterServiceMock.Setup(x => x.FilterOutInvalidNumbers(It.IsAny<int[]>())).Throws(new Exception("Numbers greater than 1000 not allowed: 1001"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _stringParser.Parse(inputNumbers));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterOfMoreThanOneCharacterDefinedOnFirstLine_WHEN_ParsingNumbers_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "##;;\n1;;2";
            int[] expectedResult = new int[] { 1, 2 };
            string[] expectedNumbersFromDelimeterService = ["1", "2"];
            _delimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(input)).Returns(expectedNumbersFromDelimeterService);
            _numbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(It.IsAny<int[]>())).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GIVEN_InputWithCustomDelimeterrOfMoreThanOneCharacterWithoutFirstLine_WHEN_ParsingNumbers_THEN_ReturnListOfIntegers()
        {
            // Arrange
            string input = "1;;;2";
            int[] expectedResult = new int[] { 1, 2 };
            string[] expectedNumbersFromDelimeterService = ["1", "2"];
            _delimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(input)).Returns(expectedNumbersFromDelimeterService);
            _numbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(It.IsAny<int[]>())).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(input);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
