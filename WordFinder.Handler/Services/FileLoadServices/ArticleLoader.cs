using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WordFinder.Core.Interfaces;

namespace WordFinder.Handler.Services.FileLoaderService
{
    public class ArticleLoader : IFileLoader
    {
        public string FilePath { get; set; }
        public string Statements { get; set; }

        public void Load()
        {
            Statements = File.ReadAllText(FilePath);
        }
    }
}
