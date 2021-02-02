using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harry.Data.DbLink
{
    public interface IDbLinkItem
    {
        /// <summary>
        /// 数据库链接名称,唯一
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 数据库类型(如SqlServer,Sqlite,MySql)
        /// </summary>
        string DbType { get; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }
    }
}
