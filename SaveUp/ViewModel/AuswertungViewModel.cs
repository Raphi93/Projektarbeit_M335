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

        public AuswertungViewModel()
        {
            GetSaveUp();
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
    }
}
