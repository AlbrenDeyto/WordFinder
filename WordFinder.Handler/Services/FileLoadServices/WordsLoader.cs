using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordFinder.Core;
using WordFinder.Core.Interfaces;

namespace WordFinder.Handler.Services.FileLoaderService
{
    public class WordsLoader : FileBase, IFileLoader
    {
        public string[] Words { get; set; }

        public void Load()
        {
            Words = File.ReadAllLines(FilePath);
        }
    }
}
