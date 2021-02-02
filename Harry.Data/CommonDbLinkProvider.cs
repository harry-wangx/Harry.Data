using Harry.Data.DbLink;
using System;

namespace Harry.Data
{
    public class CommonDbLinkProvider : IDbLinkProvider
    {
        private readonly DbLinkItem dblinkItem;
        public CommonDbLinkProvider(string name, string dbType, string connectionString, string description = null)
        {
            dblinkItem = new DbLinkItem()
            {
                Name = name ?? throw new ArgumentNullException(nameof(name)),
                DbType = dbType ?? throw new ArgumentNullException(nameof(dbType)),
                ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString)),
                Description = description
            };
        }

        public IDbLinkItem GetDbLink(string name)
        {
            if (!string.Equals(name, dblinkItem.Name, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return dblinkItem;
        }
    }
}
