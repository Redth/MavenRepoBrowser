using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class ArtifactListPage : ContentPage
    {
        public ArtifactListPage(Group mavenGroup)
        {
            InitializeComponent();

            Title = mavenGroup.Id;

            viewModel = new ViewModels.ArtifactListViewModel(mavenGroup);

            BindingContext = viewModel;
        }

        ViewModels.ArtifactListViewModel viewModel;

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedArtifact = e.SelectedItem as Artifact;

            await Navigation.PushAsync(new ArtifactVersionListPage(selectedArtifact));
        }
	}
}
