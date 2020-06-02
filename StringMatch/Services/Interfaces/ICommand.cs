using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace StringMatch.Services
{
    public interface ICommand
    {
        IContainer Container { get; set; }

        void InitializeService(string text, string subtext);
        ActionResult Run();
    }
}