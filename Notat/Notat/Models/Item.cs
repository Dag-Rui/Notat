using Notat.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Notat.Models
{
    public class Item
    {

        public List<Section> sections { get; set; } 
        public string title { get; set; }
        public string description { get; set; }
        public string id { get; set; }

        public Item()
        {
            title = "Title";
            description = "Description";
            id = Guid.NewGuid().ToString();
            sections = new List<Section>();
        }

        public Item(ItemDetailViewModel model)
        {
            sections = new List<Section>(model.sections);
            title = model.Title;
            description = model.description;
            id = model.id;
        }

        public string findFirstImage()
        {
            foreach(Section section in sections){
                if (section.GetType() == typeof(ImageSection))
                {
                    ImageSection imageSection = (ImageSection)section;
                    return imageSection.Image;
                }
            }
            return null;
        }
    }
}