
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleLoginPage : ContentPage
    {

        Services.GoogleLoginService loginService = DependencyService.Get<Services.GoogleLoginService>();

        public GoogleLoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            authCodeButton.IsVisible = false;
            authCodeEntry.IsVisible = false;

            if (loginService.isAuthenticated()) {
                authButton.IsVisible = false;
                statusLabel.Text = "Logged in!";
            }
            else
            {
                authButton.IsVisible = true;
                statusLabel.Text = "Log in to upload/download files from Google Drive";
            }
        }

        TaskCompletionSource<string> authCodeInputSource;

        private void AuthenticateWithGoogleClicked(object sender, System.EventArgs e)
        {
            authCodeInputSource = new TaskCompletionSource<string>();
            loginService.initDriveAsync(authCodeInputSource.Task);

            authCodeButton.IsVisible = true;
            authCodeEntry.IsVisible = true;
        }

        private void AuthCodeClicked(object sender, System.EventArgs e)
        {
            authCodeInputSource.SetResult(authCodeEntry.Text);
        }
    }
}