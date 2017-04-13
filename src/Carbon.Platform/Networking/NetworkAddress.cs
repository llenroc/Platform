﻿using System;
using System.Net;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Networking
{
    [Dataset("NetworkAddresses")]
    [DataIndex(IndexFlags.Unique, "providerId", "resourceId")]
    public class NetworkAddress : INetworkAddress
    {
        public NetworkAddress() { }

        public NetworkAddress(long id, IPAddress address, ManagedResource resource)
        {
            Id         = id;
            Address    = address ?? throw new ArgumentNullException(nameof(address));
            ProviderId = resource.ProviderId;
            ResourceId = resource.ResourceId;
        }

        [Member("id"), Key]
        public long Id { get; }

        [Member("address")]
        public IPAddress Address { get; }

        [Member("hostId")]
        [Indexed]
        public long? HostId { get; set; }

        [Member("networkInterfaceId")]
        [Indexed]
        public long? NetworkInterfaceId { get; set; }

        #region IResource

        [IgnoreDataMember]
        [Member("providerId")]
        public int ProviderId { get; }

        [IgnoreDataMember]
        [Member("resourceId")]
        [Ascii, StringLength(100)]
        public string ResourceId { get; }

        int IManagedResource.LocationId => 0;

        ResourceType IResource.ResourceType => ResourceType.NetworkAddress;

        #endregion

        #region Timestamps

        [Member("created"), Timestamp]
        [IgnoreDataMember]
        public DateTime Created { get; }

        [IgnoreDataMember]
        [Member("modified"), Timestamp(true)]
        public DateTime Modified { get; }

        [IgnoreDataMember]
        [Member("deleted")]
        [TimePrecision(TimePrecision.Second)]
        public DateTime? Deleted { get; }

        #endregion
    }
}