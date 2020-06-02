using StringMatch.Models;
using System;
using System.Collections.Generic;

namespace StringMatch.Services
{
    public class Matcher : IMatcher
    {
        #region Properties

        /// <summary>
        /// Represents an instance of the <see cref="Pattern"/>.
        /// </summary>
        private Pattern Pattern { get; set; }

        /// <summary>
        /// Represents the content to match the <see cref="Pattern"/> subtext against.
        /// </summary>
        private string Text { get; set; }

        /// <summary>
        /// Array of integers that represents the position of matches on the provided Text.
        /// </summary>
        private List<int> Matches { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises the service class with a text and an instance of <see cref="Pattern"/>
        /// </summary>
        /// <param name="text">Text representing the string to match the pattern subtext against.</param>
        /// <param name="pattern">Instance of <see cref="Pattern"/> contained a subtext and a calculated Prefix Table.</param>
        public Matcher(string text, Pattern pattern)
        {
            this.Pattern = pattern ?? throw new ArgumentNullException("Pattern has not been provided", "pattern");

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Text has not been provided", "text");
            }

            this.Text = text.ToLowerInvariant();
            this.Matches = new List<int>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Uses the Knuth-Morris-Pratt algorithm to calculate the matches and
        /// return a <see cref="List{int}"/> containing the positions of matches.
        /// </summary>
        public List<int> Run()
        {
            Match();
            return Matches;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs the match of a pattern against the text via the Knuth-Morris-Pratt
        /// algorithm.
        /// </summary>
        private void Match()
        {
            var subtext = Pattern.Subtext;
            var textLength = Text.Length;
            var patternLength = subtext.Length;
            var prefixTable = Pattern.PrefixTable;
            var patternIndex = 0;
            var textIndex = 0;

            while (textIndex < textLength)
            {
                if (subtext[patternIndex] == Text[textIndex])
                {
                    patternIndex++;
                    textIndex++;
                }

                /// When the all characters of the pattern has matched against the text.
                if (patternIndex == patternLength)
                {
                    Matches.Add(textIndex - patternIndex);

                    /// Shift the patternIndex according to the prefix table.
                    patternIndex = prefixTable[patternIndex - 1];
                }
                else if (textIndex < textLength && subtext[patternIndex] != Text[textIndex])
                {
                    if (patternIndex != 0)
                    {
                        patternIndex = prefixTable[patternIndex - 1];
                    }
                    else
                    {
                        textIndex++;
                    }
                }
            }
        }

        #endregion
    }
}
