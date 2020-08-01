using Microsoft.WindowsAPICodePack.Dialogs;
using Notat.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Notat.WPF.Services
{
    class WpfFileService : IFileService
    {
        private const string WORKING_DIR_KEY = "WorkingDir";

        DirectoryInfo workingDir;

        private void init()
        {
            if (workingDir != null)
            {
                return;
            }

            string folder = Properties.Settings.Default.WorkingDir;

            if (string.IsNullOrEmpty(folder))
            {
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Notat");
            }

            workingDir = new DirectoryInfo(folder);
            if (!workingDir.Exists)
            {
                workingDir.Create();
            }
        }

        public string getDirectory()
        {
            init();
            return workingDir.FullName;
        }

        public bool pickDirectorySupported() { return true; }

        public async Task<string> pickDirectory()
        {

            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select folder to store files";
            dlg.IsFolderPicker = true;

            string folder;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                folder = dlg.FileName;
            }
            else
            {
                return null;
            }


            if (folder == null)
                return null;

            workingDir = new DirectoryInfo(folder);

            Properties.Settings.Default.WorkingDir = folder;
            Properties.Settings.Default.Save();

            return folder;
        }

        public async void saveFileAsync(string subPath, string data)
        {
            init();
            FileInfo fileInfo = new FileInfo(Path.Combine(workingDir.FullName, subPath));

            StreamWriter writer = fileInfo.CreateText();
            await writer.WriteAsync(data);
            writer.Flush();
        }

        public async Task<List<string>> getFilesAsync(string subPath)
        {
            init();

            List<string> datas = new List<string>();

            foreach (FileInfo file in workingDir.GetFiles())
            {
                StreamReader streamReader = file.OpenText();

                string data = await streamReader.ReadToEndAsync();
                datas.Add(data);
            }

            return datas;
        }

    }
}
