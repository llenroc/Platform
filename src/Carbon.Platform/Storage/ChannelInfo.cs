﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Json;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Storage
{
    [Dataset("Channels")]
    public class ChannelInfo : IChannelInfo
    {
        public ChannelInfo() { }

        public ChannelInfo(
            long id, 
            string name,
            long ownerId,
            ManagedResource resource,
            ChannelFlags flags = ChannelFlags.None)
        {
            #region Preconditions

            if (id <= 0) throw new ArgumentException("Invalid", nameof(id));

            #endregion

            Id         = id;
            Name       = name ?? throw new ArgumentNullException(nameof(name));
            OwnerId    = ownerId;
            ProviderId = resource.ProviderId;
            LocationId = resource.LocationId;
            ResourceId = resource.ResourceId;
            Flags      = flags;
        }

        [Member("id"), Key(sequenceName: "channelId")]
        public long Id { get; }

        [Member("name")]
        [StringLength(63)]
        public string Name { get; }
        
        [Member("ownerId")]
        public long OwnerId { get; }

        [Member("flags")]
        public ChannelFlags Flags { get; }

        [Member("properties")]
        [StringLength(1000)]
        public JsonObject Properties { get; set; }

        // A channel may be a firehose, SNS Topic, Kinesis Stream, etc
        // A channel may have one or more consumers / subscribers

        // RentitionPeriod

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
        public int LocationId { get; }

        ResourceType IResource.ResourceType => ResourceTypes.Channel;

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

    public enum ChannelFlags
    {
        None = 0
    }
}
