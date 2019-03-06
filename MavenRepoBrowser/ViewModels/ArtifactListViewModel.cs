using System;
using System.Linq;
using System.Collections.ObjectModel;
using MavenNet.Models;
using System.Collections.Generic;

namespace MavenRepoBrowser.ViewModels
{
    public class ArtifactListViewModel
    {
        public ArtifactListViewModel(string groupId)
        {
            var mavenGroup = MavenService.GetGroup(groupId);

            if (mavenGroup == null)
                return;

            foreach (var a in mavenGroup.Artifacts.OrderBy (a => a.GroupId + "." + a.Id))
                MavenArtifacts.Add(a);
        }

        public ObservableCollection<Artifact> MavenArtifacts { get; set; } = new ObservableCollection<Artifact>();
    }
}
