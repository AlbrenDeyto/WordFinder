using System;
using System.Collections.Generic;
using System.Text;

namespace WordFinder.Core.Interfaces
{
    /*
     * Used this instead of abstract for loading text file. Can be used for other implementation like loading file
     * from other format. In addition, did not used abstract class since Load returns two different format
     * one is string, and the other is array of string.
     */
    public interface IFileLoader
    {
        void Load();
    }
}
