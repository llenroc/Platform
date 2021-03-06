﻿using System;
using System.Threading.Tasks;
using Carbon.Platform.Resources;

namespace Carbon.Platform.Storage
{
    public class BucketService : IBucketService
    {
        private readonly PlatformDb db;

        public BucketService(PlatformDb db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<BucketInfo> GetAsync(long id)
        {
            return await db.Buckets.FindAsync(id).ConfigureAwait(false)
                ?? throw ResourceError.NotFound(ResourceTypes.Volume, id);
        }

        public async Task<BucketInfo> RegisterAsync(RegisterBucketRequest request)
        {
            #region Validation

            Validate.Object(request, nameof(request));

            #endregion

            var bucket = new BucketInfo(
                id       : db.Buckets.Sequence.Next(),
                name     : request.Name,
                ownerId  : request.OwnerId,
                resource : request.Resource
            );

            await db.Buckets.InsertAsync(bucket).ConfigureAwait(false);

            return bucket;
        }
    }
}