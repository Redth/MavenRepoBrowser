using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MavenRepoBrowser
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FileViewerPage : ContentPage
	{
		public FileViewerPage (string contents)
		{
			InitializeComponent ();

            labelContents.Text = contents;
		}
	}
}