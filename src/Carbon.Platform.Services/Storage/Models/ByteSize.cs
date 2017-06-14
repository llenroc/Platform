﻿using System;

namespace Carbon.Platform.Storage
{
    public struct ByteSize
    {
        public static readonly ByteSize Zero = new ByteSize(0);

        public ByteSize(long totalBytes)
        {
            #region Preconditions

            if (totalBytes < 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName   : nameof(totalBytes),
                    actualValue : totalBytes,
                    message     : "Must be 0 or greater"
                );
            }

            #endregion

            TotalBytes = totalBytes;
        }

        public long TotalBytes { get; }

        private const long _1KiB = 1024;
        private const long _1MiB = _1KiB * 1024;
        private const long _1GiB = _1MiB * 1024;
        private const long _1TiB = _1GiB * 1024;

        public static ByteSize FromMiB(double value) => new ByteSize((long)(value * _1MiB));

        public static ByteSize FromGiB(double value) => new ByteSize((long)(value * _1GiB));

        public static ByteSize FromTiB(double value) => new ByteSize((long)(value * _1TiB));
    }
}

/*
TODO: 
- Move to Carbon.Storage
- Watch to see if this lands in CoreFx (https://github.com/dotnet/corefx/issues/14234)
*/