using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WordFinder.Core.Interfaces;
using WordFinder.Core;

namespace WordFinder.Handler.Services.FileLoaderService
{
    public class ArticleLoader : FileBase, IFileLoader
    {
        public string Statements { get; set; }

        public void Load()
        {
            Statements = File.ReadAllText(FilePath);
        }
    }
}
