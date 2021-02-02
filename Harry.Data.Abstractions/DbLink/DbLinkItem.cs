using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.DbLink
{
    public class DbLinkItem : IDbLinkItem
    {
        /// <summary>
        /// 数据库链接名称,唯一
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库类型(如SqlServer,Sqlite,MySql)
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
