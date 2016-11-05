﻿using System;
using System.Net;

namespace Carbon.Computing
{
    public struct IPAddressRange
    {
        public IPAddress Prefix { get; set; } 

        public int Cidr { get; set; } // 24

        #region Helpers

        public long Netmask => ~((1 << (32 - Cidr)) - 1);  // 255.255.255.0

        public IPAddress Broadcast => null;

        public IPAddress Start 
            => new IPAddress(IP4Number(Prefix) & Netmask);  // 10.1.1.1
        
        public IPAddress End 
            => new IPAddress((IP4Number(Prefix) & Netmask) | ~Netmask);    // 10.1.1.254

        #endregion

        // 10.138.0.0/20
        // 192.168.2.0/24

        private static int IP4Number(IPAddress ip)
            => BitConverter.ToInt32(ip.GetAddressBytes(), 0);
        
        public static IPAddressRange Parse(string text)
        {
            var parts = text.Split('/');

            return new IPAddressRange  {
                Prefix = IPAddress.Parse(parts[0]),
                Cidr = int.Parse(parts[1])
            };
        }
    }
}
