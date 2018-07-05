using System;
using System.Linq;
using System.Collections.ObjectModel;
using MavenNet.Models;

namespace MavenRepoBrowser.ViewModels
{
    public class GroupListViewModel
    {
        public GroupListViewModel()
        {
        }

        public ObservableCollection<Group> MavenGroups =>
            MavenService.Groups;
    }
}
