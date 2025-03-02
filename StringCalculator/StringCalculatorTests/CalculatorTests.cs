using StringCalculator;

namespace StringCalculatorTests
{
    public class CalculatorTests
    {
        [Fact]
        public void GIVEN_EmptyString_WHEN_Adding_THEN_ReturnZero()
        {
            // Arrange
            var calculator = new Calculator();
            string numbers = "";
            const int expectedResult = 0;

            // Act
            int result = calculator.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void GIVEN_OneOrTwoNumbers_WHEN_Adding_THEN_ReturnSum(string numbers, int expectedResult)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            int result = calculator.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
