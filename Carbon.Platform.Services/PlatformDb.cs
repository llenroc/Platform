﻿namespace Carbon.Platform
{
    using Computing;
    using Data;
    using Packaging;

    public class PlatformDb
    {
        public PlatformDb(IDbContext context)
        {
            Apps              = new Table<App>(context);
            Networks          = new Table<Network>(context);
            NetworkInterfaces = new Table<NetworkInterface>(context);
            Packages          = new Table<PackageInfo>(context);
            Programs          = new Table<Program>(context);
            Processes         = new Table<Process>(context);
            Hosts             = new Table<Host>(context);
            Volumes           = new Table<VolumeInfo>(context);
        }

        public Table<App>              Apps              { get; }
        public Table<Network>          Networks          { get; }
        public Table<NetworkInterface> NetworkInterfaces { get; }
        public Table<PackageInfo>      Packages          { get; }
        public Table<Program>          Programs          { get; }
        public Table<Process>          Processes         { get; }
        public Table<Host>             Hosts             { get; }
        public Table<VolumeInfo>       Volumes           { get; }
    }
}