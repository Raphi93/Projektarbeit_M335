﻿using Microsoft.Maui.Controls;
using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void ShowContent()
        {
            Configuration conf = ConfigManager.LoadConfig();
            Config = conf;
        }

        private async void ExecuteSave()
        {
            Configuration conf = ConfigManager.LoadConfig();
            conf.Url = Config.Url;
            conf.SaveUpUrl = Config.SaveUpUrl;
            conf.UserUrl = Config.UserUrl;
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
