using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WordFinder.Core.Config
{
    public class ConfigReader
    {
        private const string ConfigFile = "config.json";

        public string BasePath { get; set; }

        private Dictionary<string, string> ConfigItems { get; set; }

        public ConfigReader()
        {

        }
        public ConfigReader(string basePath)
        {
            BasePath = basePath;
        }
        public void Load()
        {
            var path = Path.Combine(BasePath, ConfigFile);

            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();

                ConfigItems = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
        }

        public string FindItem(string itemName)
        {
            string item = String.Empty;
            bool hasFound = ConfigItems.TryGetValue(itemName, out item);

            if (hasFound)
            {
                return item;
            }

            return null;
        }
    }
}
