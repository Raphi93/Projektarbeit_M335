﻿using Newtonsoft.Json;
using SaveUp.Model;
using System.Net.Http;
using SaveUp.View;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace SaveUp.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        private Command _cmdSave;
        private Command _cmdLogout;
        private Configuration _setting = new Configuration();
        private bool _login;
        private bool _logout;

        public LoginViewModel()
        {
            _cmdSave = new Command(ExecuteSave);
            _cmdLogout = new Command(ExecuteLogout);
            IsLoginOrNot();
        }

        private void IsLoginOrNot()
        {
            Configuration conf = ConfigManager.LoadConfig();

            if (string.IsNullOrEmpty(conf.ApiKey))
            {
                Login = true;
                Logout = false;
            }
            else
            {
                Logout = true;
                Login = false;
            }
        }


        private void ExecuteLogout()
        {
            Configuration conf = ConfigManager.LoadConfig();
            conf.ApiKey = "";
            ConfigManager.SaveConfig(conf);
            Login = true;
            Logout = false;
        }

        private async void ExecuteSave()
        {
            string name = Config.UserName;
            string passwd = Config.Password;

            try
            {
                Configuration conf = ConfigManager.LoadConfig();
                if (conf != null && !string.IsNullOrEmpty(conf.Url) && !string.IsNullOrEmpty(conf.UserUrl))
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    // Deaktiviere Zertifikatsvalidierung
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                    HttpClient client = new HttpClient(handler);


                    var request = new HttpRequestMessage(HttpMethod.Post, $"{conf.Url}{conf.UserUrl}");
                    request.Content = new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            name = name,
                            passwort = passwd
                        }),
                            Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);

                    var statusCode = "Status Code: " + response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        string apikey = data;
                        conf.ApiKey = apikey;
                        if (apikey != "")
                        {
                            Login = false;
                            Logout = true;
                            await Application.Current.MainPage.DisplayAlert("Information", "Die wurden angemeldet", "OK");
                        }else
                        {
                            await Application.Current.MainPage.DisplayAlert("Information", "Falsche Daten", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Error: {response.ReasonPhrase}", "OK");
                    }
                    conf.UserName = name;
                    conf.Password = passwd;
                    ConfigManager.SaveConfig(conf);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Information", "Um diese Funktion nutzen zu können, müssen Sie die Einstellungen mit den richtigen Angaben vervollständigen.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public Command CmdSave => _cmdSave;
        public Command CmdLogout => _cmdLogout;

        public Configuration Config
        {
            get => _setting;
            set => SetProperty(ref _setting, value);
        }
        public bool Logout
        {
            get => _logout;
            set => SetProperty(ref _logout, value);
        }
        public bool Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

    }
}