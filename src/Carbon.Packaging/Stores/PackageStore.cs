﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Carbon.Packaging
{
    using Protection;
    using Storage;

    public class PackageStore : IPackageStore
    {
        private readonly IBucket bucket;

        public PackageStore(IBucket bucket)
        {
            this.bucket = bucket;
        }

        public async Task<Hash> PutAsync(string name, SemanticVersion version, Package package)
        {
            #region Preconditions

            if (package == null) throw new ArgumentNullException(nameof(package));

            #endregion

            var key = name + "/" + version.ToString();

            using (var ms = new MemoryStream())
            {
                var hash = Hash.ComputeSHA256(ms, true);

                await package.ZipToAsync(ms).ConfigureAwait(false);

                ms.Seek(0, SeekOrigin.Begin);

                var blob = new Blob(ms) {
                    ContentType = "application/zip"
                };

                await bucket.PutAsync(key, blob).ConfigureAwait(false);

                return hash;
            }
        }

        public async Task<Package> GetAsync(string name, SemanticVersion version)
        {
            var key = name + "/" + version.ToString();

            var ms = new MemoryStream();

            var blob = await bucket.GetAsync(key);

            using (var channel = blob.Open())
            {
                await channel.CopyToAsync(ms).ConfigureAwait(false);
            }

            ms.Seek(0, SeekOrigin.Begin);

            return ZipPackage.FromStream(ms, stripFirstLevel: false);
        }       
    }
}

// var key = tag.Path + ".zip";  (e.g. app/2.1.1.zip)