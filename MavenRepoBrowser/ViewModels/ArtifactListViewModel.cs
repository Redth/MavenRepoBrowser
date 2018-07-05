using System;
using System.Linq;
using System.Collections.ObjectModel;
using MavenNet.Models;
using System.Collections.Generic;

namespace MavenRepoBrowser.ViewModels
{
    public class ArtifactListViewModel
    {
        public ArtifactListViewModel(Group mavenGroup)
        {
            foreach (var a in mavenGroup.Artifacts.OrderBy (a => a.GroupId + "." + a.Id))
                MavenArtifacts.Add(a);
        }

        public ObservableCollection<Artifact> MavenArtifacts { get; set; } = new ObservableCollection<Artifact>();
    }
}
