﻿using System;

using Carbon.Net;

namespace Carbon.Platform.Computing
{
    public interface IHealthCheck : IManagedResource
    {
        long Id { get; }

        string Host { get; }

        string Path { get; }

        ushort Port { get; }
        
        NetworkProtocal Protocal { get; }

        TimeSpan Interval { get; }

        TimeSpan Timeout { get; }

        int HealthyThreshold { get; }

        int UnhealthyThreshold { get; }
    }
}


// google:  ulong  compute#healthCheck
// fastly          Healthcheck    
// azure           Probe