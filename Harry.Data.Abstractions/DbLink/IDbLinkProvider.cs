using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.DbLink
{
    public interface IDbLinkProvider 
    {
        IDbLinkItem GetDbLink(string name);
    }
}
