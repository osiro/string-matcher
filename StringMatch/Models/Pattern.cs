using System;

namespace StringMatch.Models
{
    /// <summary>
    /// Pattern contains information about the subtext and the preffix table based on
    /// the Knuth-Morris-Pratt algorithm.
    /// </summary>
    public class Pattern : IPattern
    {
        #region Properties

        /// <summary>
        /// Represents the content of the pattern to match against any provided text.
        /// </summary>
        public string Subtext { get; set; }

        /// <summary>
        /// Table of integers representing the preffixes of Knuth–Morris–Pratt algorithm.
        /// </summary>
        public int[] PrefixTable { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the model with the subtext.
        /// </summary>
        /// <param name="subtext"></param>
        public Pattern(string subtext)
        {
            if (string.IsNullOrEmpty(subtext))
            {
                throw new ArgumentNullException("subtext can't be null", "subtext");
            }

            Subtext = subtext.ToLowerInvariant();
            var subtextLength = Subtext.Length;

            /// The array of integers of the prefix table starts with the number of slots equal to the length of the pattern subtext.
            PrefixTable = new int[subtextLength];
        }

        #endregion
    }
}
