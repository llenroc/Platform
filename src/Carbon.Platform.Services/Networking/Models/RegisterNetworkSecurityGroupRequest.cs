﻿using System;
using System.ComponentModel.DataAnnotations;

using Carbon.Platform.Resources;

namespace Carbon.Platform.Networking
{
    public class RegisterNetworkSecurityGroupRequest
    {
        public RegisterNetworkSecurityGroupRequest() { }

        public RegisterNetworkSecurityGroupRequest(
            string name,
            long networkId,
            ManagedResource resource)
        {
            Name      = name ?? throw new ArgumentNullException(nameof(name));
            NetworkId = networkId;
            Resource  = resource;
        }

        [Required]
        public string Name { get; set; }

        [Range(1, 2_199_023_255_552)]
        public long NetworkId { get; set; }

        public ManagedResource Resource { get; set; }
    }
}