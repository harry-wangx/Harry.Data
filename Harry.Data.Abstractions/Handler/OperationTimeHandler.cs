using Harry.Data.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Handler
{
    public class OperationTimeHandler : IDataHandler
    {
        public int Order => 0;

        public void OnInserting(object entity)
        {
            //如果实体对象继承于IHasCreationTime,且未设置创建时间,则将创建时间设置为当前时间
            if (entity is IHasCreationTime obj && obj.CreationTime == default(DateTime))
            {
                obj.CreationTime = DateTime.Now;
            }

            //var entity = model as ICreationAudited<TUserKey>;
            //if (entity == null)
            //{
            //    //对象未实现 ICreationAudited
            //    return;
            //}

            //if (EqualityComparer<TUserKey>.Default.Equals(userId, default(TUserKey)))
            //{
            //    //userId为默认值，直接返回
            //    return;
            //}

            //if (!EqualityComparer<TUserKey>.Default.Equals(entity.CreatorUserId, default(TUserKey)))
            //{
            //    //已经设置了创建者Id
            //    return;
            //}
        }

        public void OnUpdating(object entity)
        {
            //设置修改时间
            if (entity is IHasModificationTime obj)
            {
                obj.LastModificationTime = DateTime.Now;
            }
        }

        public void OnDeleting(object entity)
        {
            //设置删除时间
            if (entity is IHasDeletionTime obj)
            {
                obj.DeletionTime = DateTime.Now;
            }
        }
    }
}
