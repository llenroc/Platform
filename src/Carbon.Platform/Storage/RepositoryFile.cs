﻿using System;

using Carbon.Data.Annotations;

namespace Carbon.Platform.Storage
{
    // 1/master/scripts/app.js

    [Dataset("RepositoryFiles")]
    public class RepositoryFile : IRepositoryFile
    {
        public RepositoryFile() { }

        public RepositoryFile(
            long repositoryId, 
            string branchName, 
            string path, 
            long creatorId = 0,
            long size = 0,
            byte[] sha256 = null,
            FileType type = FileType.Blob)
        {
            RepositoryId = repositoryId;
            BranchName   = branchName ?? throw new ArgumentNullException(nameof(branchName));
            Path         = path       ?? throw new ArgumentNullException(nameof(path));
            Type         = type;
            CreatorId    = creatorId;
            Size         = size;
            Sha256       = sha256;
        }

        [Member("repositoryId"), Key]
        public long RepositoryId { get; }
        
        [Member("branchName"), Key]
        [StringLength(50)]
        public string BranchName { get; }
        
        [Member("path"), Key]
        [StringLength(180)] // git limit = 4096
        public string Path { get; }

        [Member("type")]
        public FileType Type { get; }

        // TODO: Change to bigId
        [Member("blobId"), Mutable]
        public long? BlobId { get; set; }

        [Member("size"), Mutable]
        public long Size { get; set; }

        // of body...
        [Member("sha256", TypeName = "binary(32)"), Mutable]
        public byte[] Sha256 { get; set; }
        
        // Sha3?

        [Member("creatorId")]
        public long CreatorId { get; }

        // LastModifiedBy

        #region Timestamps

        [Member("created"), Timestamp]
        public DateTime Created { get; }

        [Member("deleted")]
        [TimePrecision(TimePrecision.Second)]
        public DateTime? Deleted { get; }

        [Member("modified"), Timestamp(true)]
        public DateTime Modified { get; }

        #endregion
    }

    public enum FileType : byte
    {
        Blob      = 1,
        Directory = 2
    }
}

// Provides a flattened view of all the files within a revision
// A revision may be either a tag or a branch name