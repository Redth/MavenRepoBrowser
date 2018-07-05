using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class ArtifactProjectPage : ContentPage
    {
        public ArtifactProjectPage(Project project, string version)
        {
            InitializeComponent();

            Title = project.Name + " - " + project.Version;

            viewModel = new ViewModels.ArtifactProjectViewModel(Navigation, project, version);

            BindingContext = viewModel;
        }

        ViewModels.ArtifactProjectViewModel viewModel;

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var vm = e.SelectedItem as ViewModels.ProjectActionItemViewModel;

            vm?.Command();
        }
    }
}
