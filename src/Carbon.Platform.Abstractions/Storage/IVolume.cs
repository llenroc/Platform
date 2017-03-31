﻿namespace Carbon.Platform.Storage
{
    public interface IVolume
    {
        long Id { get; }

        long Size { get; } // in octets

        long LocationId { get; }
    }
}


// Azure: Managed Disk

//                 id                          | name
// AWS           : vol-1234567890abcdef0       | volume
// Google        : 6527490933702336850         | compute#disk
// Azure         : ?                           | Managed Disk

// Azure has crazy identifiers

// subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.Compute/disks/{diskName}?api-version={api-version}