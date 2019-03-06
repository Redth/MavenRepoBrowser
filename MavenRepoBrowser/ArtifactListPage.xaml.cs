using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class ArtifactListPage : ContentPage
    {
        public ArtifactListPage(string groupId)
        {
            InitializeComponent();

            Title = groupId;

            viewModel = new ViewModels.ArtifactListViewModel(groupId);

            BindingContext = viewModel;
        }

        ViewModels.ArtifactListViewModel viewModel;

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedArtifact = e.SelectedItem as Artifact;

            await Navigation.PushAsync(new ArtifactVersionListPage(selectedArtifact.GroupId, selectedArtifact.Id));

            // listView.SelectedItem = null;
        }
    }
}
