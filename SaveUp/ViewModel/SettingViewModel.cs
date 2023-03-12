using SaveUp.Model;

namespace SaveUp.ViewModel
{
    class SettingViewModel : ViewModelBase
    {
        private Command _cmdSave;
        private Configuration _setting = new Configuration();

        public SettingViewModel()
        {
            _cmdSave = new Command(ExecuteSave);
            ShowContent();
        }

        /// <summary>
        /// Methode zum Anzeigen der Konfiguration
        /// </summary>
        private void ShowContent()
        {
            Configuration conf = ConfigManager.LoadConfig();
            Config = conf;
        }


        /// <summary>
        /// Methode zum Speichern der Konfiguration
        /// </summary>
        private async void ExecuteSave()
        {
            Configuration conf = ConfigManager.LoadConfig();
            conf.Url = Config.Url;
            conf.SaveUpUrl = Config.SaveUpUrl;
            conf.UserUrl = Config.UserUrl;
            conf.Delete = Config.Delete;
            ConfigManager.SaveConfig(conf);
            await Application.Current.MainPage.DisplayAlert("Information", "Die Daten sind gespeichert", "OK");
        }

        public Command CmdSave => _cmdSave;

        public Configuration Config
        {
            get => _setting;
            set => SetProperty(ref _setting, value);
        }

    }
}
