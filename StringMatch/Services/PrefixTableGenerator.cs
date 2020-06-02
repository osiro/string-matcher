using StringMatch.Models;
using System;

namespace StringMatch.Services
{
    public class PrefixTableGenerator : IPrefixTableGenerator
    {
        #region Properties

        /// <summary>
        /// Represents an instance of the <see cref="Pattern"/>.
        /// </summary>
        private Pattern Pattern { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialised the generator with a pattern
        /// </summary>
        /// <param name="pattern">Instance of <see cref="Pattern"/></param>
        /// <exception cref="ArgumentNullException">Raised when pattern is not provided.</exception>
        /// <exception cref="ArgumentException">Raised when pattern subtext is null or empty.</exception>
        public PrefixTableGenerator(Pattern pattern)
        {
            this.Pattern = pattern ?? throw new ArgumentNullException("Pattern has not been provided", "pattern");

            if (string.IsNullOrEmpty(pattern.Subtext))
            {
                throw new ArgumentException("Pattern subtext is null or empty", "pattern");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate the Knuth-Morris-Prat prefix table and assign to the provided <see cref="Pattern"/>.
        /// </summary>
        /// <example>
        ///     var pattern = new Pattern("RACECAR");
        ///     var service = new PrefixTableGenerator(pattern);
        ///     service.Run();
        ///     Console.WriteLine(pattern.PrefixTable);
        ///     # => 0 0 0 0 0 0 1
        /// </example>
        public void Run()
        {
            Pattern.PrefixTable = CalculatePrefix();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculate the prefix table based on the Knuth-Morris-Pratt algorithm.
        /// </summary>
        /// <returns>An array of integer representing the calculated prefixes.</returns>
        private int[] CalculatePrefix()
        {
            var subtext = Pattern.Subtext;
            var patternLength = subtext.Length;

            /// Initialises the prefix table with 0's.
            var prefixTable = new int[patternLength];

            /// Current Index starts with 1 as the very first Prefix Table above starts with 0.
            for (int currentIndex = 1; currentIndex < patternLength; currentIndex++)
            {
                /// Represents the index of the longest prefix that has been found in Pattern subtext.
                int longestPrefixIndex = prefixTable[currentIndex - 1];

                while (longestPrefixIndex > 0 && subtext[longestPrefixIndex] != subtext[currentIndex])
                {
                    longestPrefixIndex = prefixTable[longestPrefixIndex];
                }

                /// Check if a prefix exists.
                if (subtext[longestPrefixIndex] == subtext[currentIndex])
                {
                    longestPrefixIndex++;
                }

                prefixTable[currentIndex] = longestPrefixIndex;
            }

            return prefixTable;
        }

        #endregion
    }
}
