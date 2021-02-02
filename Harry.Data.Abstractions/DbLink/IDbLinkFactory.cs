using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.DbLink
{
    public interface IDbLinkFactory 
    {
        IDbLinkItem GetDbLink(string name);

        void AddProvider(IDbLinkProvider provider);
    }
}
