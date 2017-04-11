﻿using System;

namespace Carbon.Platform.VersionControl
{
    public interface IRepositoryCommit
    {
        long Id { get; }

        long RepositoryId { get; }

        byte[] Sha1 { get; }

        string Message { get; }

        DateTime Created { get; }
    }
}

// FUTURE: SHA3