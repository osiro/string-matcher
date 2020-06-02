using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace StringMatch.Services
{
    /// <summary>
    /// Builds a JSON response containing all the matches.
    /// </summary>
    public class ResponseBuilder : IResponseBuilder
    {
        #region Properties

        /// <summary>
        /// Represents the list of integers containing the position of all the matches.
        /// </summary>
        private IList<int> Matches { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the builder with a list of matches.
        /// </summary>
        /// <param name="matches">List of integers containing the position of all the matches.</param>
        public ResponseBuilder(IList<int> matches)
        {
            Matches = matches;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds a JSON response based on the matching results.
        /// </summary>
        /// <returns><see cref="ObjectResult"/> to serve as the successful response</returns>
        public ActionResult Run()
        {
            return new ObjectResult(BuildResponseBody()) { StatusCode = 200 };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds a JSON object to serve as the response body.
        /// </summary>
        /// <returns><see cref="JObject"/> containing the position of the matches.</returns>
        private JObject BuildResponseBody()
        {
            return new JObject
            {
                ["type"] = "matches",
                ["attributes"] = new JObject
                {
                    ["positions"] = JArray.FromObject(Matches)
                }
            };
        }

        #endregion
    }
}
