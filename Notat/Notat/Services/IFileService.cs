using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notat.Services
{
    public interface IFileService
    {
        string getDirectory();
        bool pickDirectorySupported();
        Task<string> pickDirectory();
        void saveFileAsync(string subPath, String data);
        Task<List<string>> getFilesAsync(string subPath);
    }
}
