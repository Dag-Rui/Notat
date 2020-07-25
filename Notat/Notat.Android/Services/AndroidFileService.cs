using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Notat.Services;

namespace Notat.Droid.Services
{
    public class AndroidFileService : IFileService
    {

        public bool pickDirectorySupported()
        {
            return false;
        }

        public void pickDirectory()
        {
            throw new NotImplementedException();
        }

        public string getDirectory()
        {
            return Android.OS.Environment.DataDirectory.AbsolutePath;
        }

        public void saveFileAsync(string subPath, string data)
        {
            throw new NotImplementedException();
        }
    }
}