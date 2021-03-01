using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordFinder.Core.Interfaces;
using WordFinder.Handler.Models;

namespace WordFinder.Handler.Services.FileWriteServices
{
    public class OutputWriter : BaseWordFinderWriter, IFileWriter
    {
        public OutputWriter(List<FoundWord> foundWords, string filename)
            : base(foundWords)
        {
            this.Filename = filename;
        }

        public void Write()
        {
            var filePathTarget = Path.Combine(this.FilePath, "Output", this.Filename);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePathTarget))
            {
                file.WriteLine(this.Results.ToString()); 
            }
        }
    }
}
