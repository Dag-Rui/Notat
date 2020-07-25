using Notat.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Notat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreferencesPage : ContentPage
    {

        IFileService fileService = DependencyService.Get<IFileService>();

        public PreferencesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (fileService.pickDirectorySupported())
            {
                directoryEntry.Text = fileService.getDirectory();
            }
            else
            {
                directoryEntry.IsEnabled = false;
                directoryEntry.Text = "Directory is based on app settings";
            }
        }

        protected override void OnDisappearing()
        {

        }

        private async void pickDirectoryClicked(object sender, EventArgs e)
        {
            string directory = await fileService.pickDirectory();
            directoryEntry.Text = directory;
        }
    }

}
