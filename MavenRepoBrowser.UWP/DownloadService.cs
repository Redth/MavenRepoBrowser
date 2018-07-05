using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

[assembly: Xamarin.Forms.Dependency(typeof(MavenRepoBrowser.UWP.DownloadService))]
namespace MavenRepoBrowser.UWP
{
    public class DownloadService : IDownloadService
    {
        public DownloadService()
        {
        }

        public async Task SaveStreamAsync(string filename, Stream stream)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = filename
            };

            var extension = Path.GetExtension(filename);
            savePicker.FileTypeChoices.Add(extension, new List<string>() { extension });

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                using (var outputStream = fileStream.GetOutputStreamAt(0))
                using (var writeStream = outputStream.AsStreamForWrite())
                {
                    await stream.CopyToAsync(writeStream);
                }
            }
        }
    }
}
