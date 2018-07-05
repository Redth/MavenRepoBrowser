using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(MavenRepoBrowser.Mac.DownloadService))]
namespace MavenRepoBrowser.Mac
{
    public class DownloadService : IDownloadService
    {
        public DownloadService()
        {
        }

        public async Task SaveStreamAsync(string filename, Stream stream)
        {
            var dirs = Foundation.NSSearchPath.GetDirectories(Foundation.NSSearchPathDirectory.DownloadsDirectory, Foundation.NSSearchPathDomain.User, true);

            var ddir = dirs.FirstOrDefault();

            if (ddir != null)
            {
                var file = Path.Combine(ddir, filename);

                using (var fs = File.Create(file))
                    await stream.CopyToAsync(fs);

				AppKit.NSWorkspace.SharedWorkspace.ActivateFileViewer(new Foundation.NSUrl[] {
					Foundation.NSUrl.FromFilename(file)
				});

            }
        }
    }
}
