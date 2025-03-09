using Moq;
using StringCalculator;
using StringCalculator.Services.Delimiters;
using StringCalculator.Services.NumberFilters;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Parsers
{
    public class AdditionStringParserTests
    {
        AdditionStringParser _stringParser;
        Mock<IDelimiter> _additionDelimiterServiceMock;
        Mock<IFilterNumbers> _additionNumbersFilterServiceMock;

        public AdditionStringParserTests()
        {
            _additionDelimiterServiceMock = new Mock<IDelimiter>();
            _additionNumbersFilterServiceMock = new Mock<IFilterNumbers>();
            _stringParser = new AdditionStringParser(_additionDelimiterServiceMock.Object, _additionNumbersFilterServiceMock.Object);
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
        public void GIVEN_OneOrTwoNumbers_WHEN_Parsing_THEN_ReturnListOfNumbers(string numbers, string[] expectedNumbersFromDelimeterService, int[] expectedResult)
        {
            // Arrange
            _additionDelimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(numbers)).Returns(expectedNumbersFromDelimeterService);
            _additionNumbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1\n2,3", new string[] { "1", "2", "3" }, new int[] { 1, 2, 3 })]
        [InlineData("1\n2", new string[] { "1", "2" }, new int[] { 1, 2 })]
        public void GIVEN_AnInputSeparatedByNewLine_WHEN_Parsing_THEN_ReturnListOfNumbers(string numbers, string[] expectedNumbersFromDelimeterService, int[] expectedResult)
        {
            // Arrange
            _additionDelimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(numbers)).Returns(expectedNumbersFromDelimeterService);
            _additionNumbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

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
            string[] expectedNumbersFromDelimeterService = new string[] { "1", "2" };
            _additionDelimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(inputNumbers)).Returns(expectedNumbersFromDelimeterService);
            _additionNumbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

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
            string[] expectedNumbersFromDelimeterService = new string[] { "1", "2" };
            _additionDelimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(inputNumbers)).Returns(expectedNumbersFromDelimeterService);
            _additionNumbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumbers_WHEN_Parsing_THEN_ThrowAnException()
        {
            // Arrange
            string inputNumbers = "1;-2;-5";
            string[] expectedNumbersFromDelimeterService = new string[] { "1", "-2", "-5" };
            string expectedErrorMessage = "negatives not allowed -2,-5";
            _additionDelimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(inputNumbers)).Returns(expectedNumbersFromDelimeterService);
            _additionNumbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(new int[] {1, -2, -5})).Throws(new ArgumentException("negatives not allowed -2,-5"));

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _stringParser.Parse(inputNumbers));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void GIVEN_InputWithDelimiterOfAnyLength_WHEN_Parsing_THEN_ReturnNumbers()
        {
            // Arrange
            string inputNumbers = "//***\n1***2***3";
            int[] expectedResult = new int[] { 1, 2, 3 };
            string[] expectedNumbersFromDelimeterService = new string[] { "1", "2", "3" };
            _additionDelimiterServiceMock.Setup(s => s.GetNumbersFromDelimitedString(inputNumbers)).Returns(expectedNumbersFromDelimeterService);
            _additionNumbersFilterServiceMock.Setup(s => s.FilterOutInvalidNumbers(expectedResult)).Returns(expectedResult);

            // Act
            int[] result = _stringParser.Parse(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
