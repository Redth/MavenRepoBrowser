using System;
using System.Collections.Generic;
using System.Linq;
using MavenNet.Models;

namespace MavenRepoBrowser.ViewModels
{
    public class ArtifactVersionListViewModel
    {
        public ArtifactVersionListViewModel(Artifact mavenArtifact)
        {
            MavenArtifact = mavenArtifact;

            MavenArtifactVersions.AddRange(mavenArtifact.Versions.Reverse().Select(v => new ArtifactVersionViewModel { Version = v }));
        }

        public Artifact MavenArtifact { get; set; }

        public List<ArtifactVersionViewModel> MavenArtifactVersions { get; set; } = new List<ArtifactVersionViewModel>();
    }

    public class ArtifactVersionViewModel
    {
        public string Version { get; set; }
    }
}
