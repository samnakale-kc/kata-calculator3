﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services.Delimiters;

namespace StringCalculatorTests.Services.Delimiters
{
    public class SubtractionDelimiterTests
    {
        private SubtractionDelimiter _subtractionDelimeter;

        public SubtractionDelimiterTests()
        {
            _subtractionDelimeter = new SubtractionDelimiter();
        }

        [Fact]
        public void GIVEN_AnInputDelimitedByComma_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers()
        {
            // Arrange
            string input = "1,2,3";
            string[] expectedResult = new string[] { "1", "2", "3" };

            // Act
            string[] result = _subtractionDelimeter.GetNumbersFromDelimitedString(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GIVEN_AnInputSeparatedByNewLineAndOrComma_WHEN_GettingNumbersFromDelimitedString_THEN_ReturnListOfNumbers()
        {
            // Arrange
            string input = "1\n2,3";
            string[] expectedResult = new string[] { "1", "2", "3" };

            // Act
            string[] result = _subtractionDelimeter.GetNumbersFromDelimitedString(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
