
using Notat.Models;
using System.Collections.ObjectModel;

namespace Notat.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Section> sections { get; set; }
        public string description { get; set; }

        public string id;

        public ItemDetailViewModel(Item item)
        {
            Title = item.title;
            description = item.description;
            id = item.id;
            sections = new ObservableCollection<Section>(item.sections);

            OnPropertyChanged();
        }

        public void addSection(Section section)
        {
            sections.Add(section);
        }
    }
}
