using Notat.Services;
using Notat.Views;
using Xamarin.Forms;

namespace Notat
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ItemDataStore>();
            DependencyService.Register<GoogleLoginService>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
