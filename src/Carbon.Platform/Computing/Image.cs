﻿using System;
using System.Runtime.Serialization;

using Carbon.Data.Annotations;
using Carbon.Json;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Computing
{
    [Dataset("Images", Schema = "Computing")]
    [DataIndex(IndexFlags.Unique, "providerId", "resourceId")]
    public class Image : IImage
    {
        public Image() { }

        public Image(
            long id,
            ImageType type,
            string name,
            long ownerId,
            ManagedResource resource)
        {
            Id          = id;
            Type        = type;
            Name        = name ?? throw new ArgumentNullException(nameof(name));
            OwnerId     = ownerId;
            ResourceId  = resource.ResourceId;
            ProviderId  = resource.ProviderId;
            LocationId  = resource.LocationId;
        }

        [Member("id"), Key(sequenceName: "imageId")]
        public long Id { get; }
        
        [Member("type")]
        public ImageType Type { get; }
        
        [Member("name")]
        [StringLength(3, 128)]
        public string Name { get; }

        [Member("ownerId")]
        public long OwnerId { get; }

        [Member("details")]
        [StringLength(1000)]
        public JsonObject Details { get; set; }

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

        ResourceType IResource.ResourceType => ResourceTypes.Image;

        #endregion

        #region Timestamps

        [IgnoreDataMember]
        [Member("created"), Timestamp]
        public DateTime Created { get; }

        [IgnoreDataMember]
        [Member("deleted")]
        [TimePrecision(TimePrecision.Second)]
        public DateTime? Deleted { get; }

        // Machine images are immutable 
        // They may only be marked as deleted

        #endregion
    }
}