﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Networking
{
    [Dataset("LoadBalancers")]
    [DataIndex(IndexFlags.Unique, "providerId", "resourceId")]
    public class LoadBalancer : ILoadBalancer
    {
        public LoadBalancer() { }

        public LoadBalancer(
            long id, 
            string name,
            string address, 
            IEnvironment env, 
            ManagedResource resource)
        {
            Id            = id;
            Address       = address;
            EnvironmentId = env.Id;
            ProviderId    = resource.ProviderId;
            LocationId    = resource.LocationId;
            ResourceId    = resource.ResourceId;
        }

        // envId + Sequence
        [Member("id"), Key]
        public long Id { get; }

        [Member("name")]
        [StringLength(63)]
        public string Name { get; }

        [Member("address")]
        public string Address { get; }

        [Member("environmentId")]
        public long EnvironmentId { get; }

        [Member("networkId")]
        public long NetworkId { get; set; }

        #region IResource
        
        [IgnoreDataMember]
        [Member("providerId")]
        public int ProviderId { get; }

        [IgnoreDataMember]
        [Member("resourceId")]
        [Ascii, StringLength(100)]
        public string ResourceId { get; }

        [Member("locationId")]
        public int LocationId { get; }

        ResourceType IResource.ResourceType => ResourceType.LoadBalancer;

        #endregion

        #region Timestamps

        [IgnoreDataMember]
        [Member("created"), Timestamp]
        public DateTime Created { get; }

        [IgnoreDataMember]
        [Member("deleted")]
        [TimePrecision(TimePrecision.Second)]
        public DateTime? Deleted { get; }

        [IgnoreDataMember]
        [Member("modified"), Timestamp(true)]
        public DateTime Modified { get; }

        #endregion
    }
}
