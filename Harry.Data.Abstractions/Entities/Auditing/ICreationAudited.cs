using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface ICreationAudited<TUserKey> : IHasCreationTime
    {
        /// <summary>
        /// 创建者Id
        /// </summary>
        TUserKey CreatorId { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        string CreatorName { get; set; }
    }

    public interface ICreationAudited<TUser, TUserKey> : ICreationAudited<TUserKey>
        where TUser : IEntity<TUserKey>
    {
        /// <summary>
        /// 创建者
        /// </summary>
        TUser Creator { get; set; }
    }
}
