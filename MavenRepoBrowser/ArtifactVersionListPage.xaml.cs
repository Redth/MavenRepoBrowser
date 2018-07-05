using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class ArtifactVersionListPage : ContentPage
    {
        public ArtifactVersionListPage(Artifact mavenArtifact)
        {
            InitializeComponent();

            Title = mavenArtifact.Id;

            viewModel = new ViewModels.ArtifactVersionListViewModel(mavenArtifact);

            BindingContext = viewModel;
        }

        ViewModels.ArtifactVersionListViewModel viewModel;

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var version = e.SelectedItem as ViewModels.ArtifactVersionViewModel;

            var project = await MavenService.GetProjectAsync(viewModel.MavenArtifact, version.Version);

            if (project != null)
                await Navigation.PushAsync(new ArtifactProjectPage(project, version.Version));
        }
    }
}
