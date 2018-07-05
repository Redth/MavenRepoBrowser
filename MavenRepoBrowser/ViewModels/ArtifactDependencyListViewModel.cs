using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MavenNet.Models;

namespace MavenRepoBrowser.ViewModels
{
    public class ArtifactDependencyListViewModel
    {
        public ArtifactDependencyListViewModel(Project project)
        {
            foreach (var p in project.Dependencies)
                Dependencies.Add(p);
        }

        public ObservableCollection<Dependency> Dependencies { get; set; } = new ObservableCollection<Dependency>();
    }
}
