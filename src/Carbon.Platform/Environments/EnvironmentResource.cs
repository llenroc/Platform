﻿using System;

using Carbon.Data.Annotations;
using Carbon.Platform.Resources;

namespace Carbon.Platform
{
    [Dataset("EnvironmentResources")]
    public class EnvironmentResource
    {
        public EnvironmentResource() { }

        public EnvironmentResource(
            long id,
            IEnvironment environment,
            ILocation location,
            IResource resource
        )
        {
            #region Preconditions

            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            if (location == null)
                throw new ArgumentNullException(nameof(location));

            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            #endregion

            Id            = id;
            EnvironmentId = environment.Id;
            LocationId    = location.Id;
            ResourceType  = resource.ResourceType;
            ResourceId    = resource.Id;
        }

        [Member("id"), Key]
        public long Id { get; }

        [Member("environmentId")]
        public long EnvironmentId { get; }

        [Member("locationId")]
        public long LocationId { get; }
        
        // e.g. Bucket | LoadBalancer | HostGroup ...
        [Member("resourceType")]
        public ResourceType ResourceType { get; }

        [Member("resourceId")]
        public long ResourceId { get; }
    }
}
