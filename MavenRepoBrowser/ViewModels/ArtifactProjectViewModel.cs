using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MavenNet.Models;
using Xamarin.Forms;

namespace MavenRepoBrowser.ViewModels
{
    public class ArtifactProjectViewModel
    {
        public ArtifactProjectViewModel(INavigation navigation, Project project, string version)
        {
            Items.Add(new ProjectInfoItemViewModel("Name", project.Name));
            Items.Add(new ProjectInfoItemViewModel("Version", version));
            Items.Add(new ProjectInfoItemViewModel("Type", project.Packaging));
            Items.Add(new ProjectActionItemViewModel("Download", async () =>
            {
                var svc = DependencyService.Get<IDownloadService>();

                if (svc != null)
                {
                    var filename = project.ArtifactId + "-" + version + "." + project.Packaging.TrimStart('.');

                    using (var stream = await MavenService.GetArtifactFileAsync(project.GroupId, project.ArtifactId, version, project.Packaging))
                        await svc.SaveStreamAsync(filename, stream);
                }
            }));
            Items.Add(new ProjectActionItemViewModel("Dependencies", () => {
                navigation.PushAsync(new ArtifactDependencyListPage(project));
            }));
        }

        public Project MavenProject { get; set; }

        public ObservableCollection<ProjectItemViewModel> Items { get; set; } = new ObservableCollection<ProjectItemViewModel>();
    }

    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(string name)
        {
            Name = name;
        }

        public Project Project { get; set; }

        public string Name { get; set; }
    }

    public class ProjectActionItemViewModel : ProjectItemViewModel
    {
        public ProjectActionItemViewModel(string name, Action cmd) : base(name)
        {
            Command = cmd;
        }

        public Action Command { get; set; }
    }

    public class ProjectInfoItemViewModel : ProjectItemViewModel
    {
        public ProjectInfoItemViewModel(string name, string value) : base (name)
        {
            Value = value;
        }

        public string Value { get; set; }
    }

    public class ProjectItemViewModelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProjectInfoItemViewModelTemplate { get; set; }
        public DataTemplate ProjectActionItemViewModelTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ProjectActionItemViewModel)
                return ProjectActionItemViewModelTemplate;
            else
                return ProjectInfoItemViewModelTemplate;
        }
    }
}
