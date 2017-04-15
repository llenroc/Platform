﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Net;
using Carbon.Platform.Sequences;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Networking
{
    [Dataset("LoadBalancerListener")]
    [DataIndex(IndexFlags.Unique, "providerId", "resourceId")]
    public class LoadBalancerListener : ILoadBalancerListener
    {
        public LoadBalancerListener() { }

        public LoadBalancerListener(
            long id, 
            ApplicationProtocal protocal,
            ushort port,
            long? certificateId = null)
        {
            Id            = id;
            Protocal      = protocal;
            Port          = port;
            CertificateId = certificateId;
        }

        // proxyId | index
        [Member("id"), Key]
        public long Id { get;  }

        // e.g. HTTP | HTTPS
        [Member("protocal")]
        public ApplicationProtocal Protocal { get; }

        [Member("port")]
        public ushort Port { get; }
     
        [Member("certificateId")]
        public long? CertificateId { get; }

        public long LoadBalancerId => ScopedId.GetScope(Id);

        #region IResource

        // aws
        [Member("providerId")]
        public int ProviderId { get; set; }
        
        [Member("resourceId")]
        [Ascii, StringLength(100)]
        public string ResourceId { get; set; }

        [IgnoreDataMember]
        [Member("locationId")]
        public int LocationId { get; set; }

        // arn:aws:elasticloadbalancing:us-west-2:123456789012:listener/app/my-load-balancer/50dc6c495c0c9188/f2f7dc8efc522ab2

        ResourceType IResource.ResourceType => ResourceType.LoadBalancerListener;

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