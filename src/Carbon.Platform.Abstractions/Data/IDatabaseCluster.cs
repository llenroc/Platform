﻿namespace Carbon.Platform.Data
{
    public interface IDatabaseCluster : IManagedResource
    {
        long Id { get; }

        long DatabaseId { get; }

        string Name { get; }
    }
}