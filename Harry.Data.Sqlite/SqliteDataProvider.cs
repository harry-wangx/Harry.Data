using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.EntityFrameworkCore.Sqlite
{
    public class SqliteDataProvider : IDataProvider
    {
        private readonly DbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        public SqliteDataProvider(DbContext dbContext, IServiceProvider serviceProvider)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public Guid NewGuid()
        {
            //todo:针对不同数据库,生成不同类型GUID
            return Guid.NewGuid();
        }
    }
}
