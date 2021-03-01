using System;
using System.Collections.Generic;
using System.IO;
using WordFinder.Core;
using WordFinder.Core.Config;
using WordFinder.Core.Models;
using WordFinder.Handler.Services.FileLoaderService;
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

            return err.ErrorMsg;
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

            if (error == null)
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
                    FindOrAddError("Err3", "No Article to process.");
                    break;
                case -3:
                    FindOrAddError("Err3", "No Article to process.");
                    break;
                case -4:
                    FindOrAddError("Err3", "No Article to process.");
                    break;
            }
        }
        public static void Process(string delimeters, string article, string[] words, string abbreviations)
        {
            var wordHunter = new WordHunter
            {
                Article = article,
                Words = words,
                Delimeters = delimeters,
                Abbreviations = abbreviations.Split(",")
            };
            short result = 0;
            var sentences = wordHunter.GetSentences();

            //wordLocator.FindAndCount();

            if (result < 0)
            {
                SetPreProcessingException(result);
                Console.WriteLine("There was a problem in word finder processing.");
            }

            Console.WriteLine("Word Finder Processing Completed..");

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var configItems = LoadConfigItems(basePath);
            var article = LoadArticle(basePath, configItems.ArticleFilePath);
            var words = LoadWordsToFind(basePath, configItems.WordsFilePath);

            Process(configItems.StatementDelimeters, article, words, configItems.Abbreviations);
        }
    }
}

