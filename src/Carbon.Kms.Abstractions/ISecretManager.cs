﻿using System;
using System.Threading.Tasks;

namespace Carbon.Kms
{
    public interface ISecretManager
    {
        Task CreateAsync(string name, byte[] value, long keyId, DateTime? expires = null);

        Task<byte[]> GetAsync(string name);

        Task DeleteAsync(long id);
    }
}