using Microsoft.AspNetCore.Mvc;

namespace StringMatch.Services
{
    public interface IResponseBuilder
    {
        ActionResult Run();
    }
}