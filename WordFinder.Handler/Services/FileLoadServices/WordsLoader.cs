using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordFinder.Core.Interfaces;

namespace WordFinder.Handler.Services.FileLoaderService
{
    public class WordsLoader : IFileLoader
    {
        public string FilePath { get; set; }
        public string[] Words { get; set; }

        public string[] Abbreviations { get; set; }

        public void Load()
        {
            Words = File.ReadAllLines(FilePath);
        }
    }
}
