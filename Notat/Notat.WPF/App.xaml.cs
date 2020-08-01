using Notat.WPF.Services;
using Xamarin.Forms;

namespace Notat.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        public App()
        {
            DependencyService.Register<WpfFileService>();
        }
    }
}
