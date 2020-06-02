using Autofac;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NSubstitute;
using StringMatch.Services;
using Xunit;

namespace StringMatchTests.Services
{
    public class CommandTests
    {
        #region Properties

        private IContainer Container { get; set; }

        private IPrefixTableGenerator TableGeneratorService { get; set; }

        private IMatcher MatcherService { get; set; }

        private IResponseBuilder ResponseService { get; set; }

        #endregion

        #region Constructor

        public CommandTests()
        {
            TableGeneratorService = Substitute.For<IPrefixTableGenerator>();
            MatcherService = Substitute.For<IMatcher>();
            ResponseService = Substitute.For<IResponseBuilder>();
            Container = BuildContainer();
        }

        #endregion

        #region Tests

        [Fact]
        public void Run()
        {
            // Arrange
            var response = new ObjectResult(new JObject()) { StatusCode = 200 };
            ResponseService.Run().Returns(response);
            var command = new Command("text", "subtext");
            command.Container = Container;

            // Act
            var result = command.Run();

            // Assert
            TableGeneratorService.Received().Run();
            MatcherService.Received().Run();
            ResponseService.Received().Run();

            Assert.Equal(result, response);
        }

        #endregion

        #region Private Methods

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(TableGeneratorService).As<IPrefixTableGenerator>();
            builder.RegisterInstance(MatcherService).As<IMatcher>();
            builder.RegisterInstance(ResponseService).As<IResponseBuilder>();
            return builder.Build();
        }

        #endregion
    }
}
