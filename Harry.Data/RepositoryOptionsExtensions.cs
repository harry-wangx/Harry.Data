using Harry.Data.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data
{
    public static class RepositoryOptionsExtensions
    {
        /// <summary>
        /// 添加 <see cref="ILoggerProvider"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static RepositoryOptions UseLogger(this RepositoryOptions options, ILoggerProvider provider = null)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.LoggerFactory == null)
            {
                options.LoggerFactory = new LoggerFactory();
            }
            if (provider == null)
            {
                options.LoggerFactory.AddProvider(new EFLoggerProvider());
            }
            else
            {
                options.LoggerFactory.AddProvider(provider);
            }
            return options;
        }
    }
}
