using Notat.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Notat.UWP.Services
{
    class UwpFileService : IFileService
    {
        StorageFolder workingDir;

        private void init()
        {
            if (workingDir != null)
            {
                return;
            }

            Task<StorageFolder> task =  Windows.Storage.AccessCache.StorageApplicationPermissions.
                                            FutureAccessList.GetFolderAsync("PickedFolderToken").AsTask();
            //Why force use of async stuff?!??!
            task.Wait();
            StorageFolder folder = task.Result;

            if (folder == null)
            {
                folder = ApplicationData.Current.RoamingFolder;
            }

            this.workingDir = folder;
        }

        public string getDirectory()
        {
            init();
            return workingDir.Path;
        }

        public bool pickDirectorySupported() { return true; }

        public async Task<string> pickDirectory()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                Debug.WriteLine("Picked folder: " + folder.Name);

                workingDir = folder;
            }
            return folder.Path;
        }

        public async void saveFileAsync(string subPath, string data)
        {
            init();
            StorageFile file = await workingDir.CreateFileAsync(subPath, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, data);
        }

        public async Task<List<string>> getFilesAsync(string subPath)
        {
            init();
            IReadOnlyList<StorageFile> files = await workingDir.GetFilesAsync();

            List<string> datas = new List<string>();
            foreach (StorageFile file in files){
                string data = await FileIO.ReadTextAsync(file);
                datas.Add(data);
            }
            return datas;
        }

    }
}
