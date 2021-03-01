using System;
using System.Collections.Generic;
using System.Text;
using WordFinder.Core;
using WordFinder.Handler.Models;

namespace WordFinder.Handler.Services.FileWriteServices
{
    public abstract class BaseWordFinderWriter : FileBase
    {
        private List<FoundWord> _foundWords;
        private const string _alphabet = "abcdefghijklmnopqrstuvwxyz";

        protected string Filename { get; set; }
        public StringBuilder Results { get; set; }

        private string FormatWordFoundResult(FoundWord foundWord, string lineIndex)
        {
            var formattedStr = string.Format("{0}. {1} ", lineIndex, foundWord.Word) + "{" + foundWord.NumOfFoundByWord + ":";

            if (foundWord.FoundInSentences != null && foundWord.FoundInSentences.Count > 0)
            {
                var sentenceNos = "";
                foreach (var sentenceNum in foundWord.FoundInSentences)
                {
                    sentenceNos += sentenceNum + ",";
                }
                sentenceNos = sentenceNos.Substring(0, sentenceNos.Length - 1);

                formattedStr += sentenceNos + "}";
            }

            return formattedStr;
        }

        private void AddToResults(FoundWord foundWord, string lineIndex)
        {
            var formattedWordResult = this.FormatWordFoundResult(foundWord, lineIndex);

            this.Results.AppendLine(formattedWordResult);
        }
        public void GenerateResult()
        {
            Results = new StringBuilder();
            char[] alphabetArr = _alphabet.ToCharArray();
            int alphabetLen = _alphabet.Length;
            int lineCtr = 1;
            int alphabetIndex = 0;
            int alphabetOccurences = 1;

            foreach (var foundWord in _foundWords)
            {
                string lineIndex = "";
                char alphabetChar = ' ';

                if (lineCtr > alphabetLen)
                {
                    alphabetOccurences++;
                    alphabetIndex = 0;
                    lineCtr = 1;
                }
                alphabetChar = alphabetArr[alphabetIndex];
                lineIndex = lineIndex.PadLeft(alphabetOccurences, alphabetChar);

                this.AddToResults(foundWord, lineIndex);
                lineCtr++;
                alphabetIndex++;
            }
        }

        public BaseWordFinderWriter(List<FoundWord> foundWords)
        {
            _foundWords = foundWords;
        }
        
    }
}
