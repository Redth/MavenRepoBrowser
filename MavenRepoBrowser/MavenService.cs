﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MavenNet.Models;

namespace MavenRepoBrowser
{
    public static class MavenService
    {
        static MavenNet.MavenRepository repository;

        static MavenService()
        {
            MonkeyCache.FileStore.Barrel.ApplicationId = "mavenrepobrowser";

            repository = MavenNet.MavenRepository.FromGoogle();
        }

        static MonkeyCache.IBarrel Cache => MonkeyCache.FileStore.Barrel.Current;

        public static void ClearCache()
        {
            Cache.EmptyAll();
        }

        public static async Task LoadAsync()
        {
            Cache.EmptyExpired();
            var cachedGroups = Cache.Get<List<Group>>("mavengroups");

            if (cachedGroups != null && cachedGroups.Any())
            {
                await repository.Refresh(cachedGroups.ToArray());
            }
            else
            {
                await repository.Refresh();
                Cache.Add("mavengroups", repository.Groups, TimeSpan.FromDays(1));
            }

            Groups.Clear();
            foreach (var g in repository.Groups.OrderBy(g => g.Id))
                Groups.Add(g);
        }

        public static Group GetGroup(string groupId)
            => Groups?.FirstOrDefault(g => g.Id == groupId);

        public static Artifact GetArtifact(string groupId, string artifactId)
            => Groups?.FirstOrDefault(g => g.Id == groupId)?.Artifacts?.FirstOrDefault(a => a.Id == artifactId);
        
        public static async Task<Project> GetProjectAsync(Artifact artifact, string version)
        {
            //Cache.EmptyExpired();

            //var cacheKey = $"PROJECT-{artifact.GroupId}.{artifact.Id}.{version}";
            //var cachedProject = Cache.Get<Project>(cacheKey);
            //if (cachedProject != null)
            //    return cachedProject;

            Project project = null;

            var serializer = new XmlSerializer(typeof(Project));

            using (var pomStream = await artifact.OpenPomFile(version).ConfigureAwait(false))
            using (var sr = new StreamReader(pomStream))
                project = (Project)serializer.Deserialize(sr);

            //Cache.Add(cacheKey, project, TimeSpan.FromDays(1));

            return project;
        }

        public static async Task<Project> GetProjectAsync(string groupId, string artifactId, string version)
        {
            //Cache.EmptyExpired();

            //var cacheKey = $"PROJECT-{groupId}.{artifactId}.{version}";
            //var cachedProject = Cache.Get<Project>(cacheKey);
            //if (cachedProject != null)
            //    return cachedProject;

            Project project = null;

            var serializer = new XmlSerializer(typeof(Project));

            using (var pomStream = await repository.OpenArtifactPomFile(groupId, artifactId, version).ConfigureAwait(false))
            using (var sr = new StreamReader(pomStream))
                project = (Project)serializer.Deserialize(sr);

            //Cache.Add(cacheKey, project, TimeSpan.FromDays(1));

            return project;
        }

        public static Task<Stream> GetArtifactFileAsync(string groupId, string artifactId, string version, string type)
        {
            return repository.OpenArtifactLibraryFile(groupId, artifactId, version, type);
        }

        public static string GetArtifactFileUrl(string groupId, string artifactId, string version, string type)
        {
            var path = Path.Combine(Path.Combine(groupId.Split('.')), artifactId, version, artifactId + "-" + version + "." + "jar".ToLowerInvariant().TrimStart('.'));

            var uriBuilder = new UriBuilder(((MavenNet.GoogleMavenRepository)repository).BaseUri);
            uriBuilder.Path += path;

            return uriBuilder.Uri.OriginalString;
        }

        public static ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
    }
}
