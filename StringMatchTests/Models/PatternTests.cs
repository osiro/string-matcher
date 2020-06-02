using StringMatch.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StringMatchTests.Models
{
    public class PatternTests
    {
        #region Property Tests

        [Theory]
        [InlineData("new value")]
        [InlineData("")]
        public void Subtext(string value)
        {
            // Arrange
            var pattern = new Pattern("test");

            // Act
            pattern.Subtext = value;

            // Assert
            Assert.Equal(value, pattern.Subtext);
        }

        [Fact]
        public void NullSubtext()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Pattern(null));

            // Assert
            Assert.Equal("subtext (Parameter 'subtext can't be null')", exception.Message);
        }

        [Fact]
        public void PrefixTable()
        {
            // Arrange
            var value = new int[] { 1, 2, 3 };
            var pattern = new Pattern("test");

            // Act
            pattern.PrefixTable = value;

            // Assert
            Assert.Equal(value, pattern.PrefixTable);
        }

        #endregion

        #region Constructor Tests

        [Theory]
        [InlineData("Test")]
        public void PatternConstructor(string value)
        {
            // Arrange, Act
            var pattern = new Pattern(value);

            // Arrange
            Assert.Equal(value.ToLowerInvariant(), pattern.Subtext);
            Assert.NotNull(pattern.PrefixTable);
            Assert.Equal(value.Length, pattern.PrefixTable.Length);
        }

        #endregion
    }
}
