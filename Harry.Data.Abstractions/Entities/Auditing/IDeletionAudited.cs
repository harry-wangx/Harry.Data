using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IDeletionAudited<TUserKey> : IHasDeletionTime
    {
        /// <summary>
        /// 删除者Id
        /// </summary>
        TUserKey DeleterId { get; set; }

        /// <summary>
        /// 删除者名称
        /// </summary>
        string DeleterName { get; set; }
    }

    public interface IDeletionAudited<TUser, TUserKey> : IDeletionAudited<TUserKey>
        where TUser : IEntity<TUserKey>
    {
        /// <summary>
        /// 删除者
        /// </summary>
        TUser Deleter { get; set; }
    }
}
