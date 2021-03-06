﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Carbon.Platform
{
    internal static class Validate
    {
        public static void Object(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            Validator.ValidateObject(value, new ValidationContext(value, null, null));
        }

        public static void Id(long value, string name = "id")
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(name, value, "Must be > 0");
            }
        }

        public static void NotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NotNullOrEmpty(string value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Must not be empty", nameof(name));
            }
        }
    }
}
