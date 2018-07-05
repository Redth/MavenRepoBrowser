using System;
using System.Collections.Generic;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class GroupListPage : ContentPage
    {
        public GroupListPage(INavigation navigation)
        {
            Nav = navigation;

            InitializeComponent();
            viewModel = new ViewModels.GroupListViewModel();
            BindingContext = viewModel;
        }

        ViewModels.GroupListViewModel viewModel;

        public INavigation Nav { get; set; }
        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var group = e.SelectedItem as Group;

            App.Instance.CloseDrawer();

            var n = Nav ?? this.Navigation;

            await n.PopToRootAsync(false);
            await n.PushAsync(new ArtifactListPage(group), !App.Instance.IsDesktop);
        }
    }
}
