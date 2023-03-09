using SaveUp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SaveUp.Model
{
    public class SaveUpModel : ViewModelBase
    {

        [JsonPropertyName("_id")]
        public string? ID { get; set; }


        private string _produkt = string.Empty;
        [JsonPropertyName("produkt")]
        public string? Produkt
        {
            get => _produkt;
            set
            {
                if (_produkt != value)
                {
                    SetProperty(ref _produkt, value);
                }
            }
        }

        private string _kategorie = string.Empty;
        [JsonPropertyName("kategorie")]
        public string? Kategorie
        {
            get => _kategorie;
            set
            {
                if (_kategorie != value)
                {
                    SetProperty(ref _kategorie, value);
                }
            }
        }


        [JsonPropertyName("wert")]
        public double? Wert { get; set; }

        private DateTime _datum;
        [JsonPropertyName("datum")]
        public DateTime Datum
        {
            get => _datum;
            set
            {
                if (_datum != value)
                {
                    SetProperty(ref _datum, value);
                }
            }
        }

        private string _name = string.Empty;
        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    SetProperty(ref _name, value);
                }
            }
        }
    }
}
