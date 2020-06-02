using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using StringMatch.Services;
using AzureFunctions.Autofac;
using StringMatch.DIMappings;
using Newtonsoft.Json.Linq;

namespace StringMatch
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class StringMatch
    {
        [FunctionName("StringMatch")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Inject] ICommand command)
        {
            try
            {
                string text = req.Query["text"];
                string subtext = req.Query["subtext"];

                command.InitializeService(text, subtext);
                return command.Run();
            }
            catch (Exception ex)
            {
                var error = new JObject
                {
                    ["error"] = ex.Message
                };

                return new ObjectResult(error) { StatusCode = 400 };
            }

        }
    }
}
