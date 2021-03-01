using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordFinder.Handler.Models;
using WordFinder.Handler.Services.WordFinderService;

namespace WordFinder.Handler.Services.WordFinderServices
{
    /*
     * Separates interface for finder and report write to have room for other implementation in future.
     */
    public class WordHunter : IWordFinder, IWordFinderResultWriter
    {
        public string Delimeters { get; set; }
        public string Article { get; set; }
        public string[] Words { get; set; }
        public List<FoundWord> FoundWords { get; set; } = new List<FoundWord>();
        public string[] Abbreviations { get; set; }

        private char[] _charDelimeters;

        public WordHunter() { }
        private void SetDelimeters()
        {
            _charDelimeters = Delimeters.ToCharArray();
        }

        private bool IsAbbreviation(string word)
        {
            foreach (var abbr in Abbreviations)
            {
                if (abbr.Equals(word))
                    return true;
            }

            return false;
        }
        private bool IsEndingWord(string word)
        {
            return word.IndexOfAny(_charDelimeters) != -1 && !IsAbbreviation(word);
        }

        public short AreInputDataValid()
        {
            if (string.IsNullOrEmpty(Article))
                return -1;

            if (Words == null)
                return -2;

            if (Words.Length == 0)
                return 3;

            return 0;
        }

        public List<string> GetSentences()
        {
            List<string> sentences = new List<string>();

            try
            {
                this.SetDelimeters();
                var allWords = Article.Split(new char[0]);
                var sentence = new StringBuilder();

                foreach (var word in allWords)
                {
                    sentence.Append(string.Concat(word, " "));

                    if (this.IsEndingWord(word))
                    {
                        sentences.Add(sentence.ToString().Trim());
                        sentence = new StringBuilder();
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }

            return sentences;
        }

        private int CountPerSentence(string wordToSearch, string[] source)
        {
            var matchQuery = from word in source
                             where word.ToLower() == wordToSearch.ToLower()
                             select word;
            return matchQuery.Count();
        }

        public short FindAndCount(List<string> sentences)
        {
            if (sentences == null && sentences.Count == 0)
            {
                return -1;
            }

            try
            {
                foreach (var wordToSearch in this.Words)
                {
                    var foundWord = new FoundWord
                    {
                        Word = wordToSearch
                    };
                    var sentenceNumber = 0;

                    for (var i = 0; i < sentences.Count; i++)
                    {
                        sentenceNumber++;
                        var sourceSentence = sentences[i].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        var count = CountPerSentence(wordToSearch, sourceSentence);

                        foundWord.NumOfFoundByWord += count;
                        if (count > 0)
                        {
                            for (var x = 0; x < count; x++) 
                                foundWord.AddInSentenceNumberFound(sentenceNumber);
                        }
                            
                    }

                    FoundWords.Add(foundWord);
                }
            }
            catch (Exception)
            {
                return -4;
            }

            return 0;
        }

        public void WriteResult(List<FoundWord> FoundWords)
        {
            throw new NotImplementedException();
        }
    }
}
