﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Json;
using Carbon.Platform.Resources;

namespace Carbon.Platform
{
    [Dataset("Environments")]
    public class EnvironmentInfo : IEnvironment
    {
        public EnvironmentInfo() { }

        public EnvironmentInfo(
            long id, 
            string name,
            long ownerId,
            string slug = null,
            JsonObject variables = null)
        {
            #region Preconditions

            if (id <= 0)
                throw new ArgumentException("Must be > 0", nameof(id));

            if (ownerId <= 0)
                throw new ArgumentException("Must be > 0", nameof(ownerId));

            #endregion

            Id        = id;
            Name      = name;
            Slug      = slug;
            OwnerId   = ownerId;
            Variables = variables;
        }

        [Member("id"), Key]
        public long Id { get; }

        [Member("name")]
        [StringLength(63)]
        public string Name { get; }

        [Member("slug"), Unique]
        [StringLength(63)]
        public string Slug { get; }

        [Member("variables")]
        [StringLength(1000)]
        public JsonObject Variables { get; }
  
        // e.g. 1.2.9
        [Member("revision"), Mutable]
        public string Revision { get; set; }

        [Member("ownerId")]
        public long OwnerId { get; }

        public EnvironmentType Type
        {
            get
            {
                switch (Id % 4)
                {
                    case 0  : return EnvironmentType.Development;
                    case 1  : return EnvironmentType.Production;
                    case 2  : return EnvironmentType.Staging;
                    case 3  : return EnvironmentType.Intergration;
                    default: throw new Exception("unknown");
                }
            }
        }
        
        #region IResource

        ResourceType IResource.ResourceType => ResourceTypes.Environment;

        #endregion

        #region Stats

        // the number of deployments made to the environment
        [Member("deploymentCount")]
        public int DeploymentCount { get; }

        // the number of commands issued against the environment
        [Member("commandCount")]
        public int CommandCount { get; }

        // The number of resources added to the environment
        // e.g. buckets, encryptionKeys, loadBalancers, etc

        [Member("resourceCount")]
        public int ResourceCount { get; }

        // HostCount

        #endregion

        #region Timestamps

        [IgnoreDataMember]
        [Member("created"), Timestamp]
        public DateTime Created { get; }

        [IgnoreDataMember]
        [Member("deleted")]
        public DateTime? Deleted { get; }

        [IgnoreDataMember]
        [Member("modified"), Timestamp(true)]
        public DateTime Modified { get; }

        #endregion
    }
}

// e.g. image.processor#production
