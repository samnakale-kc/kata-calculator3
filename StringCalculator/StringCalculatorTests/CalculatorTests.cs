using Moq;
using StringCalculator;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests
{
    public class CalculatorTests
    {
        Mock<IStringParser> _stringParserMock;
        Calculator _calculator;
        public CalculatorTests() 
        {
            _stringParserMock = new Mock<IStringParser>();
            _calculator = new Calculator( _stringParserMock.Object );
        }

        [Fact]
        public void GIVEN_EmptyString_WHEN_Adding_THEN_ReturnZero()
        {
            // Arrange
            string numbers = string.Empty;
            const int expectedResult = 0;
            int[] parserResponse = new int[0];
            _stringParserMock.Setup(s => s.Parse(string.Empty)).Returns(parserResponse);

            // Act
            int result = _calculator.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void GIVEN_OneOrTwoNumbers_WHEN_Adding_THEN_ReturnSum(string numbers, int expectedResult)
        {
            // Arrange 
            _stringParserMock.Setup(s => s.Parse("1")).Returns([1]);
            _stringParserMock.Setup(s => s.Parse("1,2")).Returns([1, 2]);

            // Act
            int result = _calculator.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1\n2,3", 6)]
        [InlineData("1\n2", 3)]
        public void GIVEN_AnInputSeparatedByNewLine_WHEN_Adding_THEN_ReturnSum(string numbers, int expectedResult)
        {
            // Arrange 
            _stringParserMock.Setup(s => s.Parse("1\n2,3")).Returns(new int[] { 1, 2, 3 });
            _stringParserMock.Setup(s => s.Parse("1\n2")).Returns(new int[] { 1, 2 });

            // Act
            int result = _calculator.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimetedInput_WHEN_Adding_THEN_ReturnSum()
        {
            // Arrange
            string inputNumbers = "//;\n1;2";
            int expectedResult = 3;
            _stringParserMock.Setup(s => s.Parse(inputNumbers)).Returns(new int[] { 1, 2 });

            // Act
            int result = _calculator.Add(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_CustomDelimetedInputWithoutFirstLine_WHEN_Adding_THEN_ReturnSum()
        {
            // Arrange
            string inputNumbers = "1;2";
            int expectedResult = 3;
            _stringParserMock.Setup(s => s.Parse(inputNumbers)).Returns(new int[] { 1, 2 });

            // Act
            int result = _calculator.Add(inputNumbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumbers_WHEN_Adding_THEN_ThrowAnException()
        {
            // Arrange
            string inputNumbers = "1;-2;-5";
            string[] expectedNumbersFromDelimeterService = ["1", "-2", "-5"];
            _stringParserMock.Setup(s => s.Parse(inputNumbers)).Returns(new int[] { 1, -2, -5 });

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _calculator.Add(inputNumbers));
            Assert.Equal("negatives not allowed -2,-5", ex.Message);
        }
    }
}
