using System;
using System.IO;
using System.Threading.Tasks;

namespace MavenRepoBrowser
{
    public interface IDownloadService
    {
        Task SaveStreamAsync(string filename, Stream stream);
    }
}
