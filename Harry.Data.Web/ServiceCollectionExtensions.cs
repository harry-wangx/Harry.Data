using Harry.Data;
using Harry.Data.DbLink;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Harry.Data.Handler;
using System.Linq;
using Harry.Data.Web;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册IDataHandler,自动更新操作者ID
        /// </summary>
        /// <typeparam name="TUserKey"></typeparam>
        /// <param name="services"></param>
        /// <param name="getUserIdFunc"></param>
        /// <returns></returns>
        public static IDataBuilder AddWebUserHandler<TUserKey>(this IDataBuilder builder, Func<string, TUserKey> getUserIdFunc)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (getUserIdFunc == null)
                throw new ArgumentNullException(nameof(getUserIdFunc));

            builder.Services.AddScoped<IDataHandler, OperationUserHandler<TUserKey>>(sp =>
            {
                return new OperationUserHandler<TUserKey>(sp.GetRequiredService<IHttpContextAccessor>(), getUserIdFunc);
            });
            return builder;
        }
    }
}
