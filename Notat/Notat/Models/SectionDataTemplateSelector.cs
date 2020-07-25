using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notat.Models
{
    class SectionDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate textSectionTemplate { get; set; }
        public DataTemplate imageSectionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() == typeof(TextSection))
            {
                return textSectionTemplate;
            }
            if (item.GetType() == typeof(ImageSection))
            {
                return imageSectionTemplate;
            }
            throw new InvalidOperationException("Item not registered " + item.GetType().Name);
        }
    }
}
