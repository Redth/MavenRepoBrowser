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
    }
}
