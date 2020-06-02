using Autofac;
using AzureFunctions.Autofac.Configuration;
using StringMatch.Services;
using System;

namespace StringMatch.DIMappings
{
    /// <summary>
    /// Configures the dependency injection in the Azure Function App.
    /// </summary>
    public class DIConfig
    {
        #region Constructors

        /// <summary>
        /// Maps the main runner interface to the concrete Function App service class.
        /// </summary>
        /// <param name="functionName">Name of the function that's being injected into.</param>
        /// <exception cref="ArgumentException">Raised when the provided function app is not recognized.</exception>
        public DIConfig(string functionName)
        {
            Inject(functionName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs the dependency injection based on the received function name.
        /// </summary>
        /// <param name="functionName"><see cref="string"/> representing the name of the function app to inject the service class to.</param>
        /// <exception cref="ArgumentException">Raised when the provided function app is not recognized.</exception>
        private void Inject(string functionName)
        {
            switch (functionName)
            {
                case "StringMatch":
                    DependencyInjection.Initialize(builder =>
                    {
                        builder.RegisterType<Command>().As<ICommand>();
                    }, functionName);

                    break;
                default:
                    throw new ArgumentException("Dependency Injection Error: unexpected function app received.", "functionName");
            }
        }

        #endregion
    }
}
