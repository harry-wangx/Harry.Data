using Harry.Data.DbLink;
using System;

namespace Harry.Data
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// 创建Repository
        /// </summary>
        IRepository CreateRepository(IServiceProvider serviceProvider, IDbLinkItem dbLink, params Type[] entityTypes);
    }
}
