using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.Parsers
{
    public class StringParserFactoryTests
    {
        [Theory]
        [InlineData(ParserType.SUBTRACTION)]
        [InlineData(ParserType.ADDITION)]
        public void GIVEN_ValidStringParserType_WHEN_CreatingParser_THEN_ReturnParser(ParserType type)
        {
            // Arrange
            var parserFactory = new ParserFactory();

            // Act
            var result = parserFactory.Create(type);

            // Assert
            Assert.IsAssignableFrom<IStringParser>(result);
        }

        [Fact]
        public void GIVEN_InvalidStringParserType_WHEN_CreatingParser_THEN_ThrowAnException()
        {
            // Arrange
            var parserType = (ParserType) 99;
            var parserFactory = new ParserFactory();

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => parserFactory.Create(parserType));
        }
    }
}
