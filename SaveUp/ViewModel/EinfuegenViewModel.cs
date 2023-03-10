using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaveUp.ViewModel
{
    public class EinfuegenViewModel : ViewModelBase
    {
        private Command _cmdSave;
        private SaveUpModel _saveUp = new SaveUpModel();
        private string _date;

        public EinfuegenViewModel()
        {
            _cmdSave = new Command(ExecuteSave);
            GetDate();
            SaveUp.Kategorie = "Lebensmittel";
        }

        private void GetDate()
        {
            DateTime now = DateTime.Now;
            Date = now.ToString("dd.MM.yyyy");
        }

        private async void ExecuteSave()
        {

            if ((SaveUp.Wert != null) && (!String.IsNullOrWhiteSpace(SaveUp.Produkt)))
            {
                try
                {
                    SaveUp.Produkt = SaveUp.Produkt.Trim();
                    SaveUp.Datum = DateTime.Now;
                    SaveUp.Wert = Math.Round((double)SaveUp.Wert, 2);

                    Configuration conf = ConfigManager.LoadConfig();

                    SaveUp.Name = conf.UserName;

                    if (conf != null && !string.IsNullOrEmpty(conf.Url) && !string.IsNullOrEmpty(conf.UserUrl))
                    {

                        var handler = new HttpClientHandler();
                        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                        var client = new HttpClient(handler);


                        var json = JsonSerializer.Serialize(SaveUp);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var request = new HttpRequestMessage(HttpMethod.Post, $"{conf.Url}{conf.SaveUpUrl}");
                        request.Headers.Add("ApiKey", conf.ApiKey);
                        request.Content = content;

                        var response = await client.SendAsync(request);
                        await Application.Current.MainPage.DisplayAlert("Information", "Die Daten sind Gespeichert", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Information", "Sie müssen alles eintragen", "OK");
            }
        }

        public SaveUpModel SaveUp
        {
            get => _saveUp;
            set => SetProperty(ref _saveUp, value);
        }

        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public Command CmdSave => _cmdSave;
    }
}
