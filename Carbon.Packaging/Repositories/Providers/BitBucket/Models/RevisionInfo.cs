﻿namespace Bitbucket
{
    using System;

    using Carbon.Platform;

    public class BitbucketCommit : ICommit
    {
        public string Hash { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        string ICommit.Id => Hash;

    }
}