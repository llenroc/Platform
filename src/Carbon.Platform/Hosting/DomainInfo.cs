﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Hosting
{
    [Dataset("Domains")]
    public class DomainInfo : IDomain
    {
        public DomainInfo() { }

        public DomainInfo(long id, string name, long ownerId)
        {
            Id      = id;
            Name    = name ?? throw new ArgumentNullException(nameof(name));
            OwnerId = ownerId;
        }

        [Member("id"), Key(sequenceName: "domainId")]
        public long Id { get; }

        // max-length = 253 characters
        [Member("name"), Unique]
        [StringLength(180)]
        public string Name { get; }

        [Member("certificateId")]
        public long? CertificateId { get; }

        [Member("ownerId")]
        public long OwnerId { get; }

        #region IResource
        
        ResourceType IResource.ResourceType => ResourceType.Domain;

        #endregion

        #region Timestamps

        [Member("validated")]
        public DateTime? Validated { get; }

        [Member("expires"), Mutable]
        public DateTime Expires { get; }

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