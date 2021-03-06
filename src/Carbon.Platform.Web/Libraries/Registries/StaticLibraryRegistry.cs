﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Carbon.Platform.Web.Libraries
{
    using Json;
    using Versioning;

    public class StaticLibraryRegistry : IWebLibraryRegistry
    {
        private readonly Dictionary<string, IWebLibrary> items = new Dictionary<string, IWebLibrary>();

        public StaticLibraryRegistry() { }

        public StaticLibraryRegistry(IWebLibrary[] items)
        {
            #region Preconditions

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            #endregion

            foreach (var item in items)
            {
                if (this.items.ContainsKey(item.Name))
                {
                    throw new ArgumentException($"Registry already contains entry named '{item.Name}'");
                }

                this.items.Add(item.Name, item);
            }
        }

        public void Add(IWebLibrary library)
        {
            items.Add(library.Name, library);
        }

        public IWebLibrary Find(string name, SemanticVersionRange range)
        {
            if (items.TryGetValue(name, out IWebLibrary lib))
            {
                return lib;
            }
            else
            {
                throw new Exception($"No library named '{name}' was not found");
            }
        }

        public IWebLibrary Find(string name, SemanticVersion version)
        {
            if (items.TryGetValue(name, out IWebLibrary lib))
            {
                return lib;
            }

            return null;
        }

        public string Serialize()
        {
            return JsonNode.FromObject(items.Select(i => i.Value)).ToString(pretty: true);
        }

        public static StaticLibraryRegistry Deserialize(string text)
        {
            #region Preconditions

            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (text.Length == 0)
                throw new ArgumentException("Must not be empty", nameof(text));

            #endregion

            var a = JsonArray.Parse(text).ToArrayOf<StaticLibrary>();

            return new StaticLibraryRegistry(a);
        }
    }
}

/*

[
  { 
    name       : "basejs", 
    version    : "1.0.0",
    main       : { name: "hi.json", sha256: "base64encoded" }
  },
  ...
]

*/
