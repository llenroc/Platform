﻿using System.Net;

using Amazon.Metadata;

using Carbon.Platform.Resources;

namespace Carbon.Platform.Services
{
    using Computing;

    public class CreateHostRequest
    {
        public CreateHostRequest() { }

        public CreateHostRequest(
            IPAddress[] addresses, 
            IEnvironment env, 
            ILocation location,
            IMachineImage machineImage,
            IMachineType machineType,
            ManagedResource resource,
            long networkId = 0,
            long? groupId = null,
            HostStatus status = HostStatus.Pending)
        {
            Addresses      = addresses;
            EnvironmentId  = env.Id;
            Location       = location;
            MachineImageId = machineImage.Id;
            MachineTypeId  = machineType.Id;
            NetworkId      = networkId;
            Status         = status;
            Resource       = resource;
            GroupId        = groupId;
        }

        // private ips first...
        public IPAddress[] Addresses { get; set; }

        public long EnvironmentId { get; set; }

        public ILocation Location { get; set; }

        public HostStatus Status { get; set; } = HostStatus.Pending;

        public ManagedResource Resource { get; set; }
        
        public long MachineTypeId { get; set; }

        public long MachineImageId { get; set; }

        public long NetworkId { get; set; }

        public long? GroupId { get; set; }

        public static CreateHostRequest Create(InstanceIdentity instance, IEnvironment env)
        {
            var location = Locations.Get(ResourceProvider.Aws, instance.AvailabilityZone);

            // machineImage = await GetImageAsync(ResourceProvider.Amazon, instance.ImageId).ConfigureAwait(false);
            return new CreateHostRequest
            {
                Status = HostStatus.Running,
                Addresses = new[] { IPAddress.Parse(instance.PrivateIp) },
                Resource = ManagedResource.Host(location, instance.InstanceId),
                EnvironmentId = env.Id,
                Location = location,
                MachineImageId = 0, // TODO
                MachineTypeId = AwsInstanceType.GetId(instance.InstanceType),
                NetworkId = 0
            };

        }
    }
}