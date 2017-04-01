﻿using System;
using System.Text;

namespace Carbon.Repositories
{
    public class RepositoryInfo
    {
        public RepositoryInfo(
            RepositoryProviderId provider,
            string accountName,
            string name,
            Revision? revision)
        {
            Provider = provider;
            AccountName = accountName;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Revision = revision;
        }

        public RepositoryProviderId Provider { get; }

        public string AccountName { get; }

        public string Name { get; }

        public Revision? Revision { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Provider != RepositoryProviderId.GitHub)
            {
                sb.Append(Provider.ToString().ToLower());
                sb.Append(":");
            }

            sb.Append(AccountName);
            sb.Append("/");
            sb.Append(Name);

            if (Revision != null)
            {
                sb.Append("#");
                sb.Append(Revision.Value.Name);
            }

            return sb.ToString();
        }

        public static RepositoryInfo Parse(string text)
        {
            #region Preconditions

            if (text == null) throw new ArgumentNullException(nameof(text));

            #endregion

            var hasHost = text.Contains(":");

            if (text.Contains("://"))
            {
                // Strip off the protocal
                text = text.Substring(text.IndexOf("://") + 3);
            }

            var hostType = RepositoryProviderId.GitHub;
            string accountName = null;
            string repositoryName = null;
            Revision? revision = null;

            var i = 0;

            foreach (var part in text.Split(':', '/', '#'))
            {
                if (i == 0)
                {
                    if (hasHost)
                    {
                        hostType = RepositoryProvider.Parse(part);
                    }
                    else
                    {
                        hostType = RepositoryProviderId.GitHub;
                        accountName = part;
                    }
                }

                if (i == 1)
                {
                    if (accountName == null)
                    {
                        accountName = part;
                    }
                    else
                    {
                        repositoryName = part;
                    }
                }

                if (i == 2)
                {
                    if (repositoryName == null)
                    {
                        repositoryName = part;
                    }
                    else
                    {
                        revision = Repositories.Revision.Parse(part);
                    }
                }

                if (i == 3)
                {
                    revision = Repositories.Revision.Parse(part);
                }

                i++;
            }

            if (repositoryName.EndsWith(".git"))
            {
                repositoryName = repositoryName.Replace(".git", "");
            }

            return new RepositoryInfo(hostType, accountName, repositoryName, revision);
        }
    }
}
