using Harry.Data.DbLink;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell;
using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Shell.Models;
using System.IO;

namespace Harry.Data.OrchardCore
{
    public class SystemDbLinkProvider : IDbLinkProvider
    {
        private readonly SystemDbLinkOptions options;
        private readonly IServiceProvider sp;
        public SystemDbLinkProvider(IOptions<SystemDbLinkOptions> optionsAccessor, IServiceProvider serviceProvider)
        {
            options = optionsAccessor.Value;
            this.sp = serviceProvider;

            if (string.IsNullOrEmpty(options.DbLinkName))
            {
                options.DbLinkName = SystemDbLinkOptions.DefaultDbLinkName;
            }
        }

        public IDbLinkItem GetDbLink(string name)
        {
            if (!string.Equals(name, options.DbLinkName, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            var shellSettings = sp.GetRequiredService<ShellSettings>();
            // Before the setup a 'DatabaseProvider' may be configured without a required 'ConnectionString'.
            if (shellSettings.State != TenantState.Running || shellSettings["DatabaseProvider"] == null)
            {
                return null;
            }

            var result = new DbLinkItem()
            {
                Name = options.DbLinkName,
                ConnectionString = shellSettings["ConnectionString"]
            };

            switch (shellSettings["DatabaseProvider"])
            {
                case "SqlConnection":
                    result.DbType = "SqlServer";
                    break;
                case "Sqlite":
                    var shellOptions = sp.GetService<IOptions<ShellOptions>>();
                    var option = shellOptions.Value;
                    var databaseFolder = Path.Combine(option.ShellsApplicationDataPath, option.ShellsContainerName, shellSettings.Name);
                    var databaseFile = Path.Combine(databaseFolder, "yessql.db");
                    if (!Directory.Exists(databaseFolder))
                    {
                        Directory.CreateDirectory(databaseFolder);
                    }
                    result.DbType = "Sqlite";
                    result.ConnectionString = $"Data Source={databaseFile};Cache=Shared";
                    break;
                default:
                    result.DbType = shellSettings["DatabaseProvider"];
                    break;
            }
            return result;
        }
    }
}
