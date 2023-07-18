using SimGreenHearts.Mobile.Views.Auth;

namespace SimGreenHearts.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
        }
    }
}