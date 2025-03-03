using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator;
using StringCalculator.Services.NumberFilters;

namespace StringCalculatorTests.Services.NumberFilters
{
    public class AdditionNumbersFilterTests
    {
        private AdditionNumbersFilter _additionNumbersFilter;

        public AdditionNumbersFilterTests() 
        { 
            _additionNumbersFilter = new AdditionNumbersFilter();
        }

        [Fact]
        public void GIVEN_InputWithNegativeNumbers_WHEN_FilteringOutInvalidNumbers_THEN_ThrowAnException()
        {
            // Arrange
            int[] inputNumbers = new int[] { 1, -2, -5 };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _additionNumbersFilter.FilterOutInvalidNumbers(inputNumbers));
            Assert.Equal("negatives not allowed -2,-5", ex.Message);
        }
    }
}
