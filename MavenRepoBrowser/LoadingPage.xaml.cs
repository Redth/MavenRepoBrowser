using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage(Action done)
        {
            InitializeComponent();

            this.done = done;
        }

        Action done;

		protected override async void OnAppearing()
		{
			base.OnAppearing();

            try
            {
                await MavenService.LoadAsync();
                activityIndicator.IsRunning = false;

                done?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Failed to load Maven Repository!", "OK");
            }
            finally
            {
                activityIndicator.IsRunning = false;
            }
		}
	}
}
