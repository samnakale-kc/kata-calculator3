using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.NumberFilters;
using StringCalculator.Services.Parsers;

namespace StringCalculatorTests.Services.NumberFilters
{
    public class SubtractionNumbersFilterTests
    {
        private SubtractionNumbersFilter _subtractionNumbersFilter;

        public SubtractionNumbersFilterTests()
        {
            _subtractionNumbersFilter = new SubtractionNumbersFilter();
        }

        [Fact]
        public void GIVEN_AnArrayOfNumbersContainingElementsGreaterThanOneThousand_WHEN_FilteringOutInvalidNumbers_THEN_ThrowAnException()
        {
            // Arrange
            int[] inputNumbers = new int[] { 1001, 2, 5 };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _subtractionNumbersFilter.FilterOutInvalidNumbers(inputNumbers));
            Assert.Equal("Numbers greater than 1000 not allowed: 1001", ex.Message);
        }
    }
}
