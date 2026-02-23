using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace project_RYS.Forms
{
    internal class ConfigManager
    {
        private static readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        public static string GetJsonFilePath()
        {
            if (File.Exists(configFilePath))
            {
                var configData = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(configFilePath));
                if (configData != null && configData.ContainsKey("JsonFilePath"))
                {
                    return configData["JsonFilePath"];
                }
            }
            return null;
        }

        public static void SetJsonFilePath(string jsonFilePath)
        {
            var configData = new Dictionary<string, string>
        {
            { "JsonFilePath", jsonFilePath }
        };
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configData, Formatting.Indented));
        }
    }
}
