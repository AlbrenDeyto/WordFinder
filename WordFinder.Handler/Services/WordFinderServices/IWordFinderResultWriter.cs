using System;
using System.Collections.Generic;
using System.Text;
using WordFinder.Handler.Models;

namespace WordFinder.Handler.Services.WordFinderService
{
    public interface IWordFinderResultWriter
    {
        void WriteResult(List<FoundWord> FoundWords);
    }
}
