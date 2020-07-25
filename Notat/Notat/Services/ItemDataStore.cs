using Newtonsoft.Json;
using Notat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Gma.DataStructures.StringSearch;

namespace Notat.Services
{
    public class ItemDataStore
    {
        public const string FILE_PREFIX = ".nota";
        
        IFileService fileService = DependencyService.Get<IFileService>();

        readonly List<Item> items = new List<Item>();
        readonly SuffixTrie<Item> cache = new SuffixTrie<Item>(3);

        public ItemDataStore()
        {

        }

        public void saveItemAsync(Item item)
        {

            int index = indexOf(item);
            if (index == -1)
            {
                items.Add(item);
            }
            else
            {
                items[index] = item;
            }
            saveAsync(item);
        }

        private void saveAsync(Item item)
        {
            string json = JsonConvert.SerializeObject(item);

            string filename = Regex.Replace(item.title, "^[a-zA-Z0-9](?:[a-zA-Z0-9 ._-]*[a-zA-Z0-9])?\\.[a-zA-Z0-9_-]+$", "");

            fileService.saveFileAsync(filename + FILE_PREFIX, json);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            List<string> datas = await fileService.getFilesAsync("");
            List<Item> items = new List<Item>();

            foreach (string data in datas)
            {
                Item item = JsonConvert.DeserializeObject<Item>(data);
                items.Add(item);
            }

            return items;
        }

        private int indexOf(Item item)
        {
            if (item.id == null)
            {
                return -1;
            }

            for (int i = 0; i < items.Count(); i++)
            {
                if (items.ElementAt(i).id == item.id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}