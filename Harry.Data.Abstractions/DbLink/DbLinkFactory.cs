using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.DbLink
{
    public class DbLinkFactory : IDbLinkFactory
    {
        private readonly List<IDbLinkProvider> _providers = new List<IDbLinkProvider>();
        public DbLinkFactory(IEnumerable<IDbLinkProvider> providers)
        {
            foreach (var provider in providers)
            {
                AddProvider(provider);
            }
        }

        public IDbLinkItem GetDbLink(string name)
        {
            IDbLinkItem result = null;
            foreach (var provider in _providers)
            {
                result = provider.GetDbLink(name);
                if (result != null)
                    break;
            }

            return result;
        }

        public void AddProvider(IDbLinkProvider provider)
        {
            _providers.Add(provider);
        }
    }
}
