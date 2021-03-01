using System;
using System.Collections.Generic;
using System.Text;

namespace WordFinder.Core.Interfaces
{
    /*
     * Similar to IFileLoader, there could be different ways to handle writing to output file.
     * */
    public interface IFileWriter
    {
        void Write();
    }
}
