using System;
using System.Collections.Generic;
using System.Text;

namespace WordFinder.Handler.Models
{
    /*
     * Holds the count and which statement word is found.
     */
    public class FoundWord
    {
        public string Word { get; set; }
        public int NumOfFoundByWord { get; set; } = 0;
        public List<int> FoundInSentences { get; set; } = new List<int>();


        public void AddInSentenceNumberFound(int sentenceNumber)
        {
            FoundInSentences.Add(sentenceNumber);
        }
    }
}
