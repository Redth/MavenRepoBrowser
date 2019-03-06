using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class ArtifactVersionListPage : ContentPage
    {
        public ArtifactVersionListPage(string groupId, string artifactId)
        {
            InitializeComponent();

            var mavenArtifact = MavenService.GetArtifact(groupId, artifactId);

            if (mavenArtifact == null)
                return;

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

            //listView.SelectedItem = null;
        }
    }
}
