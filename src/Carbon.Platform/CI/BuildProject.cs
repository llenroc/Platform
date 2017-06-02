﻿using System;

using Carbon.Data.Annotations;
using Carbon.Platform.Resources;

namespace Carbon.CI
{
    [Dataset("BuildProjects")]
    public class BuildProject : IBuildProject
    {
        public BuildProject() { }

        public BuildProject(
            long id, 
            string name,
            long repositoryId,
            long ownerId,
            ManagedResource resource)
        {
            #region Preconditions

            if (id <= 0)
                throw new ArgumentException("Must be > 0", nameof(id));

            if (repositoryId <= 0)
                throw new ArgumentException("Must be > 0", nameof(repositoryId));

            if (ownerId <= 0)
                throw new ArgumentException("Must be > 0", nameof(ownerId));

            #endregion

            Id           = id;
            Name         = name ?? throw new ArgumentNullException(nameof(name));
            RepositoryId = repositoryId;
            OwnerId      = ownerId;
            ProviderId   = resource.ProviderId;
            ResourceId   = resource.ResourceId;
            LocationId   = resource.LocationId;
        }

        [Member("id"), Key(sequenceName: "buildProjectId")]
        public long Id { get; }

        [Member("name")]
        public string Name { get; }

        [Member("repositoryId")]
        public long RepositoryId { get; }

        [Member("imageId")]
        public long ImageId { get; set; }
        
        [Member("ownerId")]
        public long OwnerId { get; }

        #region Stats

        [Member("buildCount")]
        public int BuildCount { get; }

        #endregion

        #region IResource

        [Member("providerId")]
        public int ProviderId { get; }

        [Member("resourceId")]
        public string ResourceId { get; }

        [Member("locationId")]
        public int LocationId { get; }
        
        ResourceType IResource.ResourceType => ResourceTypes.BuildProject; // ci:project

        #endregion

        #region Timestamps

        [Member("created"), Timestamp]
        public DateTime Created { get; }

        [Member("modified"), Timestamp(true)]
        public DateTime Modified { get; }

        #endregion
    }
}