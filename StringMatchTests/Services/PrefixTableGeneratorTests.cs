using StringMatch.Models;
using StringMatch.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace StringMatchTests.Services
{
    public class PrefixTableGeneratorTests
    {

        #region Testing Data

        public static IEnumerable<object[]> GetSubtexts()
        {
            return new List<object[]>
            {
                new object[] { "ab", new int[] { 0, 0 } },
                new object[] { "test", new int[] { 0, 0, 0, 1 } },
                new object[] { "acacagt", new int[] { 0, 0, 1, 2, 3, 0, 0 } },
                new object[] { "flanders", new int[] { 0, 0, 0, 0, 0, 0, 0, 0 } },
                new object[] { "atag", new int[] { 0, 0, 1, 0 } },
            };
        }

        #endregion

        #region Method Tests

        [Theory]
        [MemberData(nameof(GetSubtexts))]
        public void Run(string subtext, int[] expectedResult)
        {
            // Arrange
            var pattern = new Pattern(subtext);
            var service = new PrefixTableGenerator(pattern);

            // Act
            service.Run();

            // Assert
            Assert.Equal(expectedResult, pattern.PrefixTable);
        }

        [Fact]
        public void RunWithoutPattern()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new PrefixTableGenerator(null));

            // Assert
            Assert.Equal("pattern (Parameter 'Pattern has not been provided')", exception.Message);
        }

        #endregion
    }
}
