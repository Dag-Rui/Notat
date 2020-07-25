using Google.Apis.Util.Store;
using Notat.Models;
using Notat.Services;
using Notat.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Notat.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {

        ItemDataStore dataStore = DependencyService.Get<ItemDataStore>();

        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send(this, "AddItem", new Item(viewModel));
            base.OnDisappearing();
        }

        private void onTextSectionClicked(object sender, System.EventArgs e)
        {

        }

        private void onImageSectionClicked(object sender, System.EventArgs e)
        {

        }

        private async void addSectionClicked(object sender, System.EventArgs e)
        {
            string action = await DisplayActionSheet("Add new section", "Cancel", null,
                "Single image",
                "Text");

            if (action.Equals("Single image"))
            {
                viewModel.addSection(new ImageSection()
                {
                    Header = "Test"
                });
            }
            else if (action.Equals("Text"))
            {
                viewModel.addSection(new TextSection()
                {
                    Header = "Test"
                });
            }
        }
    }
}