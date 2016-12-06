﻿namespace Carbon.Platform.Computing
{
    public enum InstanceStatus
    {
        Pending     = 0, // provisioning
        Running     = 1,
        Suspending  = 2, // stopping
        Suspended   = 3, // stopped
        Terminating = 4, // shutting down ?
        Terminated  = 5 
    }

    // Google Cloud: Staging?
}