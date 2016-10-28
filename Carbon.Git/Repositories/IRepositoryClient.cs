﻿using System.Threading.Tasks;

namespace Carbon.Packaging
{
    using Repositories;

    public interface IRepositoryClient
    {
        Task<Package> DownloadAsync(Revision revision);

        Task<IGitCommit> GetCommitAsync(Revision revision);

        Task TagAsync(IGitCommit commit, Semver version);
    }
}