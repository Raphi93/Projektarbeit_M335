using System.Net;

namespace SaveUp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Deaktiviere Zertifikatsvalidierung
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            MainPage = new AppShell();
        }
    }
}