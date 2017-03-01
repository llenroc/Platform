﻿using System;

namespace Carbon.Platform
{
    public struct CloudResourceInfo : IEquatable<CloudResourceInfo>
    {
        public CloudResourceInfo(CloudProvider provider, ResourceType type, string id)
        {
            #region Preconditions

            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            #endregion

            Provider = provider;
            Type = type;
            Region = null;
            Name = id;
        }

        public CloudProvider Provider { get; }

        public string Region { get; set; }

        public ResourceType Type { get; }

        public string Name { get; }

        public override string ToString() =>
            Provider.Code.ToLower() + ":" + Type.GetName() + "/" + Name;

        public static implicit operator string(CloudResourceInfo resource) => 
            resource.ToString();

        #region Amazon Helpers

        public static CloudResourceInfo Ec2Instance(string id)
            => new CloudResourceInfo(CloudProvider.Amazon, ResourceType.Instance, id);

        public static CloudResourceInfo Ec2Volume(string id)
            => new CloudResourceInfo(CloudProvider.Amazon, ResourceType.Volume, id);

        public static CloudResourceInfo Ec2Image(string id)
           => new CloudResourceInfo(CloudProvider.Amazon, ResourceType.Image, id);

        public static CloudResourceInfo Ec2NetworkInterface(string id)
            => new CloudResourceInfo(CloudProvider.Amazon, ResourceType.NetworkInterface, id);

        public static CloudResourceInfo Ec2Vpc(string id)
            => new CloudResourceInfo(CloudProvider.Amazon, ResourceType.Network, id);

        #endregion

        #region Azure


        public static CloudResourceInfo AzureVolume(string id)
            => new CloudResourceInfo(CloudProvider.Microsoft, ResourceType.Volume, id);

        public static CloudResourceInfo AzureInstance(string id)
            => new CloudResourceInfo(CloudProvider.Microsoft, ResourceType.Instance, id);

        #endregion

        private static readonly char[] splitOn = { ':', '/' };

        // e.g. 
        // amzn:instance/i-453-352-18
        public static CloudResourceInfo Parse(string text)
        {
            var parts = text.Split(splitOn);

            var platform = CloudProvider.Parse(parts[0]);
            var type     = ResourceTypeHelper.Parse(parts[1]);
            var id       = parts[2];

            return new CloudResourceInfo(platform, type, id);            
        }

        #region IEquatable

        bool IEquatable<CloudResourceInfo>.Equals(CloudResourceInfo other) => 
            Provider.Code == other.Provider.Code 
            && Type == other.Type 
            && Region == other.Region 
            && Name == other.Name;

        #endregion
    }

}

// 1/i-07e6001e0415497e4
// amzn:region/us-east-1

/*
amzn:instance/i-453-352-18
amzn:volume/vol-1a2b3c4d
amzn:image/ami-1a2b3c4d
amzn:bucket:us-east-1/name
goog:instance/1234
goog:bucket/name
*/
