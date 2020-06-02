using Autofac;
using Microsoft.AspNetCore.Mvc;
using StringMatch.Models;
using System.Collections.Generic;

namespace StringMatch.Services
{
    /// <summary>
    /// Command service class orchestrate the calls to all the other service classes
    /// via the dependency injection pattern.
    /// </summary>
    public class Command : ICommand
    {
        #region Properties

        private string Text { get; set; }

        private string Subtext { get; set; }

        public IContainer Container { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the Command class with the text, subtext and also configures the container for
        /// dependency injection.
        /// </summary>
        /// <param name="text">Represents the text from which the pattern will me matched against.</param>
        /// <param name="subtext">Represents the subtext to match the provided text.</param>
        public void InitializeService(string text, string subtext)
        {
            Text = text;
            Subtext = subtext;
            Container = InitializeContainer();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the matcher process and return a JSON response body containing the list of matching positions.
        /// </summary>
        /// <returns>A <see cref="ActionResult"/> representing a successful response.</returns>
        public ActionResult Run()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var pattern = BuildPattern();
                pattern = BuildPrefixTable(scope, pattern);
                var matches = FindMatches(scope, pattern);
                return BuildResponse(scope, matches);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises the container to resolve services for dependency injection.
        /// </summary>
        /// <returns>Instances of <see cref="IContainer"/> with service dependencies.</returns>
        private IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PrefixTableGenerator>().As<IPrefixTableGenerator>();
            builder.RegisterType<Matcher>().As<IMatcher>();
            builder.RegisterType<ResponseBuilder>().As<IResponseBuilder>();
            return builder.Build();
        }

        /// <summary>
        /// Builds a <see cref="Patter"/> based on the provided subtext.
        /// </summary>
        /// <returns>Instance implementing <see cref="IPattern"/></returns>
        private IPattern BuildPattern()
        {
            return new Pattern(Subtext);
        }

        /// <summary>
        /// Runs the prefix table algorithm.
        /// </summary>
        /// <param name="scope">Scope of the dependency injection.</param>
        /// <param name="pattern">Instance implementing <see cref="IPattern"/></param>
        /// <returns><see cref="Pattern"/> containing a prefix table.</returns>
        private IPattern BuildPrefixTable(ILifetimeScope scope, IPattern pattern)
        {
            var service = scope.Resolve<IPrefixTableGenerator>(
                new NamedParameter("pattern", pattern)
            );

            service.Run();
            return pattern;
        }

        /// <summary>
        /// Matches a subtext agains the provided text.
        /// </summary>
        /// <param name="scope">Scope of the dependency injection.</param>
        /// <param name="pattern">Instance implementing <see cref="IPattern"/></param>
        /// <returns>A <see cref="IList{int}"/> containing indexes representing the position of the matches.</returns>
        private List<int> FindMatches(ILifetimeScope scope, IPattern pattern)
        {
            var service = scope.Resolve<IMatcher>(
                new NamedParameter("text", Text),
                new NamedParameter("pattern", pattern)
            );

            return service.Run();
        }

        /// <summary>
        /// Builds a HTTP response body out of the found matches.
        /// </summary>
        /// <param name="scope">Scope of the dependency injection.</param>
        /// <param name="matches">List of the position of the matches.</param>
        /// <returns><see cref="ActionResult"/> representing a successful response.</returns>
        private ActionResult BuildResponse(ILifetimeScope scope, IList<int> matches)
        {
            var service = scope.Resolve<IResponseBuilder>(
                new NamedParameter("matches", matches)
            );

            return service.Run();
        }

        #endregion
    }
}
