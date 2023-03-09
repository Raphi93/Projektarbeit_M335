using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using SaveUp.ViewModel;
using System.Threading.Tasks;

namespace SaveUp.Model
{
    public class Configuration : ViewModelBase
    {
        private string _apiKey = string.Empty;
        public string ApiKey
        {
            get => _apiKey;
            set
            {
                if (_apiKey != value)
                {
                    SetProperty(ref _apiKey, value);
                }
            }
        }

        private string _url = string.Empty;
        public string Url
        {
            get => _url;
            set
            {
                if (_url != value)
                {
                    SetProperty(ref _url, value);
                }
            }
        }

        private string _saveUpUrl = string.Empty;
        public string SaveUpUrl
        {
            get => _saveUpUrl;
            set
            {
                if (_saveUpUrl != value)
                {
                    SetProperty(ref _saveUpUrl, value);
                }
            }
        }

        private string _userUrl = string.Empty;
        public string UserUrl
        {
            get => _userUrl;
            set
            {
                if (_userUrl != value)
                {
                    SetProperty(ref _userUrl, value);
                }
            }
        }

        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    SetProperty(ref _userName, value);
                }
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    SetProperty(ref _password, value);
                }
            }
        }
    }

    public static class ConfigManager
    {
        private static string configPath = Path.Combine(FileSystem.AppDataDirectory, "config.json");

        public static Configuration LoadConfig()
        {
            if (File.Exists(configPath))
            {
                string jsonString = File.ReadAllText(configPath);
                return JsonSerializer.Deserialize<Configuration>(jsonString);
            }
            else
            {
                return new Configuration();
            }
        }

        public static void SaveConfig(Configuration config)
        {
            string jsonString = JsonSerializer.Serialize(config);
            File.WriteAllText(configPath, jsonString);
        }
    }
}
