using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IAudited<TUserKey> : ICreationAudited<TUserKey>, IModificationAudited<TUserKey>, IDeletionAudited<TUserKey>
    {
    }

    public interface IAudited<TUser, TUserKey> :
        ICreationAudited<TUser, TUserKey>,
        IModificationAudited<TUser, TUserKey>,
        IDeletionAudited<TUser, TUserKey>
        where TUser : IEntity<TUserKey>
    {
    }
}
