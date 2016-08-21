﻿using System.Threading.Tasks;

namespace Carbon.Platform
{
    using Packaging;

    public interface IRepositoryClient
    {
        // ListBranches
        // GetBranch

        Task<Package> DownloadAsync(Revision revision);

        Task<ICommit> GetCommitAsync(Revision revision);

        Task TagAsync(ICommit commit, string name);
    }
}