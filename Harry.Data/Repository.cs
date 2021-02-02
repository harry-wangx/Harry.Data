using Harry.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;


namespace Harry.Data
{
    public class Repository : IRepository
    {
        private readonly Lazy<DbContext> dbContextAccessor;
        private readonly Lazy<IEnumerable<IDataHandler>> handlersAccessor;
        /// <summary>
        /// ¹¹Ôìº¯Êý
        /// </summary>
        public Repository(IServiceProvider serviceProvider, Func<DbContext> dbContextFactory, IDbProvider dbProvider)
        {
            this.ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            if (dbContextFactory == null)
                throw new ArgumentNullException(nameof(dbContextFactory));
            this.DbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));

            dbContextAccessor = new Lazy<DbContext>(dbContextFactory);
            handlersAccessor = new Lazy<IEnumerable<IDataHandler>>(() =>
                this.ServiceProvider.GetServices<IDataHandler>().OrderBy(m => m.Order)
            );
        }

        public IServiceProvider ServiceProvider { get; }

        public DbContext DbContext
        {
            get { return dbContextAccessor.Value; }
        }

        public IDbProvider DbProvider { get; }

        public IEnumerable<IDataHandler> Handlers
        {
            get { return handlersAccessor.Value; }
        }
    }
}
