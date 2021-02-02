using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Harry.Data
{
    public interface IRepository
    {
        IServiceProvider ServiceProvider { get; }

        DbContext DbContext { get; }

        IDbProvider DbProvider { get; }

        IEnumerable<IDataHandler> Handlers { get; }
    }
}
