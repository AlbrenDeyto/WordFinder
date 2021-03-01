using System;
using WordFinder.Handler.Services.WordFinderServices;
using Xunit;

namespace WordFinderUnitTest
{
    public class WordFinderTester
    {
        //Can add testing loading of article and words files here.

        //For now focus on the real requirement handlers.
        [Fact]
        public void GetSentencesTester()
        {
            var article = @"This is what I learned from Mr. Jones about a paragraph. " +
                          @"A paragraph is a group of words put together to form a group that is usually longer than a sentence. " +
                          @"Paragraphs are often made up of several sentences.";
            var words = new string[] { "This", "is" };
            var abbreviations = "Mr.,Mrs.";
            var wordHunter = new WordHunter
            {
                Article = article,
                Words = words,
                Abbreviations = abbreviations.Split(","),
                Delimeters = ".?!"
            };

            var sentences = wordHunter.GetSentences();

            Assert.True(sentences != null && sentences.Count > 0);
        }

        [Fact]
        public void FindWordsTester()
        {
            var article = @"This is what I learned from Mr. Jones about a paragraph. " +
                          @"A paragraph is a group of words put together to form a group that is usually longer than a sentence. " +
                          @"Paragraphs are often made up of several sentences.";
            var words = new string[] { "xx", "This", "is", "a" };
            var abbreviations = "Mr.,Mrs.";
            var wordHunter = new WordHunter
            {
                Article = article,
                Words = words,
                Abbreviations = abbreviations.Split(","),
                Delimeters = ".?!"
            };

            var sentences = wordHunter.GetSentences();
            var result = wordHunter.FindAndCount(sentences);

            Assert.True(result == 0);

            //Can also check if there really found words.
            //Assert.True(wordHunter.FoundWords != null && wordHunter.FoundWords.Count > 0);
        }
    }
}
