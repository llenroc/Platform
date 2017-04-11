﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;

namespace Carbon.Platform.Data
{
    [Dataset("DatabaseCluster")]
    public class DatabaseCluster : IDatabaseCluster
    {
        public DatabaseCluster() { }

        public DatabaseCluster(long id, string name, ManagedResource resource)
        {
            Id = id;
            Name = name;

            ProviderId = resource.ProviderId;
            LocationId = resource.LocationId;
            ResourceId = resource.ResourceId;
        }

        // DatabaseId + Sequence
        [Member("id"), Key]
        public long Id { get; }

        [Member("name")]
        public string Name { get; }

        #region IResource

        [IgnoreDataMember]
        [Member("providerId")]
        public int ProviderId { get; }

        [IgnoreDataMember]
        [Member("resourceId")]
        [StringLength(100)]
        public string ResourceId { get; }

        [IgnoreDataMember]
        [Member("locationId")]
        public long LocationId { get; }

        ResourceType IManagedResource.ResourceType => ResourceType.DatabaseCluster;

        #endregion

        #region Timestamps

        [Member("created"), Timestamp]
        public DateTime Created { get; }

        #endregion

        public long DatabaseId => ScopedId.GetScope(Id);
    }
}