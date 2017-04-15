﻿using Xunit;

namespace Carbon.Platform.Resources
{
	public class ResourceTests
	{
        [Fact]
        public void ParseHost()
        {
            var resource = ManagedResource.Parse("ibm:host/18");

            Assert.Equal("IBM", ResourceProvider.Get(resource.ProviderId).Name);
            Assert.Equal(ResourceType.Host, resource.Type);
            Assert.Equal("18", resource.ResourceId);

            Assert.Equal("ibm:host/18", resource.ToString());
        }

        [Fact]
        public void ParseHostWithZone()
        {
            var resource = ManagedResource.Parse("aws:us-east-1:host/18");

            Assert.Equal(1, resource.ProviderId);
            Assert.Equal(ResourceType.Host, resource.Type);
            Assert.Equal("18", resource.ResourceId);

            Assert.Equal("aws:us-east-1:host/18", resource.ToString());
        }

        [Fact]
        public void ParseRegion()
        {
            var resource = ManagedResource.Parse("aws:region/us-east-1");

            Assert.Equal("Amazon",              ResourceProvider.Get(resource.ProviderId).Name);
            Assert.Equal(ResourceType.Region,   resource.Type);
            Assert.Equal("us-east-1",           resource.ResourceId);
            
            Assert.Equal("aws:region/us-east-1", resource.ToString());
        }

        [Fact]
        public void ParseZone()
        {
            var resource = ManagedResource.Parse("aws:zone/us-east-1b");

            Assert.Equal(1,                 resource.ProviderId);
            Assert.Equal(ResourceType.Zone, resource.Type);
            Assert.Equal("us-east-1b",      resource.ResourceId);

            Assert.Equal("aws:zone/us-east-1b", resource.ToString());
        }

        [Theory]
        [InlineData(ResourceType.App,              "app")]
        [InlineData(ResourceType.Bucket,           "bucket")]
        [InlineData(ResourceType.Database,         "database")]
        [InlineData(ResourceType.Domain,           "domain")]
        [InlineData(ResourceType.MachineImage,     "machineImage")]
        [InlineData(ResourceType.Host,             "host")]
        [InlineData(ResourceType.Network,          "network")]
        [InlineData(ResourceType.NetworkGateway,   "gateway")]
        [InlineData(ResourceType.NetworkInterface, "networkInterface")]
        [InlineData(ResourceType.Region,           "region")]
        [InlineData(ResourceType.Subnet,           "subnet")]
        [InlineData(ResourceType.Zone,             "zone")]
        [InlineData(ResourceType.Repository,       "repository")]
        public void Types(ResourceType type, string name)
        {
            Assert.Equal(name, type.GetName());
            Assert.Equal(type, ResourceTypeHelper.Parse(name));
        }
    }
}