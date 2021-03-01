using System;
using System.Collections.Generic;
using System.IO;
using WordFinder.Core;
using WordFinder.Core.Config;
using WordFinder.Core.Models;
using WordFinder.Handler.Services.FileLoaderService;
using WordFinder.Handler.Services.FileWriteServices;
using WordFinder.Handler.Services.WordFinderServices;

namespace WordFinder
{
    class Program
    {
        public static List<Error> Errors = new List<Error>(); 

        //Little exception handling mechanism.
        public static string FindError(string errorCode)
        {
            var err = Errors.Find(e => e.ErrorCode.Equals(errorCode));

            if (err != null)
                return err.ErrorMsg;

            return string.Empty;
        }

        public static void AddError(string errorCode, string msg)
        {
            Errors.Add(new Error { ErrorCode = errorCode, ErrorMsg = msg });
            Console.WriteLine(msg);
        }
        //

        public static void FindOrAddError(string code, string msg)
        {
            var error = FindError(code);

            if (string.IsNullOrEmpty(error))
                AddError(code, msg);
            else
                Console.WriteLine(error);
        }

        public static ConfigItems LoadConfigItems(string basePath)
        {
            var configReader = new ConfigReader(basePath);

            configReader.Load();

            return new ConfigItems
            {
                ArticleFilePath = configReader.FindItem(ConfigItemNames.ArticleFilePath),
                WordsFilePath = configReader.FindItem(ConfigItemNames.WordsFilePath),
                OutputFilePath = configReader.FindItem(ConfigItemNames.OutputFilePath),
                StatementDelimeters = configReader.FindItem(ConfigItemNames.StatementDelimeters),
                OtherDelimeters = configReader.FindItem(ConfigItemNames.OtherDelimeters),
                Abbreviations = configReader.FindItem(ConfigItemNames.Abbreviations)
            };
        }

        public static string LoadArticle(string basePath, string articleFilePath)
        {
            try
            {
                var articleLoader = new ArticleLoader();

                articleLoader.FilePath = Path.Combine(basePath, "Input", articleFilePath);
                articleLoader.Load();

                return articleLoader.Statements;
            }
            catch (Exception)
            {
                FindOrAddError("Err1", "Loading Artcile file failed.");
            }

            return null;
        }

        
        public static string[] LoadWordsToFind(string basePath, string wordsFilePath)
        {
            try
            {
                var wordsLoader = new WordsLoader();

                wordsLoader.FilePath = Path.Combine(basePath, "Input", wordsFilePath);
                wordsLoader.Load();

                return wordsLoader.Words;
            }
            catch (Exception)
            {
                FindOrAddError("Err2", "Loading Words file failed.");
            }

            return null;
        }

        public static void SetPreProcessingException(short result)
        {
            switch (result)
            {
                case -1:
                    FindOrAddError("Err3", "No Article to process.");
                    break;
                case -2:
                    FindOrAddError("Err4", "No Article to process.");
                    break;
                case -3:
                    FindOrAddError("Err5", "No Article to process.");
                    break;
                case -4:
                    FindOrAddError("Err6", "No Article to process.");
                    break;
            }
        }
        public static void Process(string delimeters, string otherDelimeters,
            string article, string[] words, string abbreviations, 
            string basePath, string outputFilename)
        {
            var wordHunter = new WordHunter
            {
                Article = article,
                Words = words,
                Delimeters = delimeters,
                Abbreviations = abbreviations.Split(","),
                OtherDelimeters = otherDelimeters
            };
            
            var sentences = wordHunter.GetSentences();
            short result = wordHunter.FindAndCount(sentences);

            if (result < 0)
            {
                SetPreProcessingException(result);
                Console.WriteLine("There was a problem in word finder processing.");
            }

            try
            {
                var outputWriter = new OutputWriter(wordHunter.FoundWords, outputFilename);

                outputWriter.FilePath = basePath;
                outputWriter.GenerateResult();
                if (outputWriter.Results.Length > 0)
                {
                    outputWriter.Write();
                    Console.WriteLine("Word Finder Processing Completed..");
                    return;
                }

                FindOrAddError("Err6", "There was no results generated.");
            }
            catch (Exception)
            {
                FindOrAddError("Err7", "Writing output file failed.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Word Hunting started!");
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var configItems = LoadConfigItems(basePath);
            var article = LoadArticle(basePath, configItems.ArticleFilePath);
            var words = LoadWordsToFind(basePath, configItems.WordsFilePath);

            Process(configItems.StatementDelimeters, configItems.OtherDelimeters,
                article, words, configItems.Abbreviations, 
                basePath, configItems.OutputFilePath);
        }
    }
}

