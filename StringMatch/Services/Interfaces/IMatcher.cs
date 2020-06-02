using System.Collections.Generic;

namespace StringMatch.Services
{
    public interface IMatcher
    {
        List<int> Run();
    }
}