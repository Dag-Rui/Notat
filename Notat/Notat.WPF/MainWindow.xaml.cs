using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace Notat.WPF
{
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Forms.Init();
            LoadApplication(new Notat.App());
        }
    }
}