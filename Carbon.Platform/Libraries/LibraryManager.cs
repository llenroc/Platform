﻿namespace Carbon.Libraries
{
    using System.Threading.Tasks;

    
    public class LibraryManager
    {
        public LibraryRelease Find(string name, Semver version)
        {
            // What store should we query?

            switch (version.Level)
            {
                case MatchLevel.Patch: // find exact
                case MatchLevel.Minor: // find highest matching major & minor
                case MatchLevel.Major: // find higest matching major 
                    break;
            }

            return null;
        }
        
        public async Task<LibraryRelease> PublishAsync(Library library, Semver version)
        {
            // - Get source from GIT
            // - Verify source
            // - Read package.json

            // Make sure version is incrimental within the series


            // As needed
            // - Extract TypeScript and build
            // - Extract SASS and build



            // Push to CDN

            // Create release record
            // Where? DynamoDb or RelationalDB?

            // TODO: return record
            return null;
        }

    }
}
