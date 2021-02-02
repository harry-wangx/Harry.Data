using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IModificationAudited<TUserKey> : IHasModificationTime
    {
        /// <summary>
        /// 最后编辑者Id
        /// </summary>
        TUserKey LastModifierId { get; set; }

        /// <summary>
        /// 最后编辑者名称
        /// </summary>
        string LastModifierName { get; set; }
    }

    public interface IModificationAudited<TUser, TUserKey> : IModificationAudited<TUserKey>
        where TUser : IEntity<TUserKey>
    {
        /// <summary>
        /// 最后编辑者
        /// </summary>
        TUser LastModifier { get; set; }
    }
}
