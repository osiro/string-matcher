using StringMatch.Models;
using StringMatch.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace StringMatchTests.Services
{
    public class MatcherTests
    {
        #region Testing Data

        public static IEnumerable<object[]> BuildMatches()
        {
            var text = "This is a test and testing all the matches.";

            return new List<object[]>
            {
                new object[] { text, "test", new List<int> { 10, 19 } },
                new object[] { text, "is", new List<int> { 2, 5 } },
                new object[] { text, "TeSt", new List<int> { 10, 19 } },
                new object[] { text, "hello", new List<int> { } },
            };
        }

        #endregion

        #region Method Tests

        [Theory]
        [MemberData(nameof(BuildMatches))]
        public void Run(string text, string subtext, IList<int> matches)
        {
            // Arrange
            var pattern = new Pattern(subtext);
            new PrefixTableGenerator(pattern).Run();
            var service = new Matcher(text, pattern);

            // Act
            var result = service.Run();

            // Assert
            Assert.Equal(matches, result);
        }

        [Fact]
        public void RunWithoutText()
        {
            // Arrange
            var pattern = new Pattern("hi");

            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Matcher(string.Empty, pattern));

            // Assert
            Assert.Equal("text (Parameter 'Text has not been provided')", exception.Message);

        }
        [Fact]
        public void RunWithoutPattern()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Matcher("hi", null));

            // Assert
            Assert.Equal("pattern (Parameter 'Pattern has not been provided')", exception.Message);

        }

        #endregion
    }
}
