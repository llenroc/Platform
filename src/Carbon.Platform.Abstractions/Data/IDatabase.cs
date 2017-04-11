﻿namespace Carbon.Platform.Data
{
    public interface IDatabase
    {
        long Id { get; }

        string Name { get; }
    }
}



// A database may be spread across mutiple clusters & instances
// Each database may have mutiple schemas

// e.g. (1, "Carbon", "aws:database:345-234-5234234)