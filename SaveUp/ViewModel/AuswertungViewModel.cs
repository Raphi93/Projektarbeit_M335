using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaveUp.ViewModel
{
    public class AuswertungViewModel : ViewModelBase
    {
        private List<SaveUpModel> _saveUp;
        private SaveUpModel _selectedSaveUp;
        private Command _cmdDeleteOne;
        private Command _cmdDelete;
        private double _gessamt;

        public AuswertungViewModel()
        {
            _cmdDeleteOne = new Command(ExecuteDeleteOne);
            _cmdDelete = new Command(ExecuteDelete);
            GetSaveUp();
        }

        private async void ExecuteDelete()
        {
            try
            {
                Configuration conf = ConfigManager.LoadConfig();
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                var client = new HttpClient(handler);

                var request = new HttpRequestMessage(HttpMethod.Delete, $"{conf.Url}{conf.SaveUpUrl}Name/{conf.UserName}");
                request.Headers.Add("ApiKey", conf.ApiKey);

                var response = await client.SendAsync(request);

                var statusCode = "Status Code: " + response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    GetSaveUp();
                    await Application.Current.MainPage.DisplayAlert("Information", "Die Daten sind Gelöscht", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error: {response.ReasonPhrase}", "OK");
                }
            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void ExecuteDeleteOne()
        {
            if (_selectedSaveUp == null)
            {
                await Application.Current.MainPage.DisplayAlert("Information", "Sie müssen eins Auswählen", "OK");
            }
            else
            {
                try
                {
                    Configuration conf = ConfigManager.LoadConfig();
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                    var client = new HttpClient(handler);

                    var request = new HttpRequestMessage(HttpMethod.Delete, $"{conf.Url}{conf.SaveUpUrl}{SelectedSaveUp.ID}");
                    request.Headers.Add("ApiKey", conf.ApiKey);

                    var response = await client.SendAsync(request);

                    var statusCode = "Status Code: " + response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        GetSaveUp();
                        await Application.Current.MainPage.DisplayAlert("Information", "Die Daten sind Gelöscht", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Error: {response.ReasonPhrase}", "OK");
                    }


                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        private async void GetSaveUp()
        {
            try
            {
                Configuration conf = ConfigManager.LoadConfig();
                if (conf != null && !string.IsNullOrEmpty(conf.Url) && !string.IsNullOrEmpty(conf.UserUrl))
                {
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                    var client = new HttpClient(handler);

                    var request = new HttpRequestMessage(HttpMethod.Get, $"{conf.Url}{conf.SaveUpUrl}{conf.UserName}");
                    request.Headers.Add("ApiKey", conf.ApiKey);

                    var response = await client.SendAsync(request);

                    var statusCode = "Status Code: " + response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        var datas = await response.Content.ReadAsStringAsync();
                        var tempData = JsonSerializer.Deserialize<List<SaveUpModel>>(datas);
                        
                        foreach (var Item in tempData)
                        {
                            Item.DayTime = Item.Datum.ToString("dd.MM.yyyy");
                            Gesammt += (double)Item.Wert;
                        }
                        SaveUp = tempData;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Error: {response.ReasonPhrase}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public List<SaveUpModel> SaveUp
        {
            get => _saveUp;
            set => SetProperty(ref _saveUp, value);
        }

        public SaveUpModel SelectedSaveUp
        {
            get => _selectedSaveUp;
            set => SetProperty(ref _selectedSaveUp, value);
        }

        public double Gesammt
        {
            get => _gessamt;
            set => SetProperty(ref _gessamt, value);
        }

        public Command CmdDeleteOne => _cmdDeleteOne;
        public Command CmdDelete => _cmdDelete;
    }
}
