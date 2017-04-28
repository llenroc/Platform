﻿
using Carbon.Versioning;

namespace Carbon.Platform.Apps
{
    public interface IAppRelease
    {
        long AppId { get; }

        SemanticVersion Version { get; }

        long CommitId { get; }

        long CreatorId { get; }

        byte[] Sha256 { get; }
    }
}