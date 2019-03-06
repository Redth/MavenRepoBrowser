using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class ArtifactDependencyListPage : ContentPage
    {
        public ArtifactDependencyListPage(Project project)
        {
            InitializeComponent();

            viewModel = new ViewModels.ArtifactDependencyListViewModel(project);

            BindingContext = viewModel;
        }

        ViewModels.ArtifactDependencyListViewModel viewModel;

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var dep = e.SelectedItem as Dependency;

            var project = await MavenService.GetProjectAsync(dep.GroupId, dep.ArtifactId, dep.Version);

            if (project != null)
                await Navigation.PushAsync(new ArtifactProjectPage(project, dep.Version));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //listView.SelectedItem = null;
        }

    }
}
