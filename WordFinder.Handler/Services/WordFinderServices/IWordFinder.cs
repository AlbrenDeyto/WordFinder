using System;
using System.Collections.Generic;
using System.Text;
using WordFinder.Handler.Models;

namespace WordFinder.Handler.Services.WordFinderService
{
    public interface IWordFinder
    {
        string Delimeters { get; set; }
        string Article { get; set; }
        string[] Words { get; set; }
        string[] Abbreviations { get; set; }

        List<FoundWord> FoundWords { get; set; }

        short AreInputDataValid();

        List<string> GetSentences();
        short FindAndCount(List<string> sentences);
    }
}
