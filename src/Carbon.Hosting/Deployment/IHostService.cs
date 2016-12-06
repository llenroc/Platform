﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carbon.Hosting
{
    using Platform.Computing;
    using Packaging;

    // This could be IIS, Apache, Unicorn, or a Self Host

    public interface IHostService
    {
        IEnumerable<Process> Scan();

        Process Find(long id); 

        Task DeleteAsync(IProgram program);

        Task DeployAsync(IProgram program, Package package);

        // This will install the program if it doesn't already exist
        Task<Process> ActivateAsync(IProgram app); 

        Task ReloadAsync(IProgram app);

        bool IsDeployed(IProgram app);
    }
}