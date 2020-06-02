using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StringMatch.Services;
using System.Collections.Generic;
using Xunit;

namespace StringMatchTests.Services
{
    public class ResponseBuilderTests
    {
        [Fact]
        public void Run()
        {
            // Arrange
            var matches = new List<int> { 1, 2 };
            var responseBuilder = new ResponseBuilder(matches);
            var expectedResult = new JObject
            {
                ["Value"] = new JObject
                {
                    ["type"] = "matches",
                    ["attributes"] = new JObject
                    {
                        ["positions"] = new JArray { 1, 2 }
                    }
                },
                ["Formatters"] = new JArray(),
                ["ContentTypes"] = new JArray(),
                ["DeclaredType"] = null,
                ["StatusCode"] = 200
            };

            // Act
            var result = responseBuilder.Run();
            var serializedObject = JsonConvert.SerializeObject(result, Formatting.None);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedResult.ToString(Formatting.None), serializedObject);
        }
    }
}
