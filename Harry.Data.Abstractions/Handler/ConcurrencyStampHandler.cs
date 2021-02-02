using Harry.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Handler
{
    /// <summary>
    /// 检测实体对象是否继随于<see cref="IHasConcurrencyStamp"/>,如果是,设置ConcurrencyStamp属性
    /// </summary>
    public class ConcurrencyStampHandler : IDataHandler
    {
        public int Order => 0;

        public void OnInserting(object entity)
        {
            if (entity is IHasConcurrencyStamp concurrencyStampEntity)
            {
                if (string.IsNullOrEmpty(concurrencyStampEntity.ConcurrencyStamp))
                    concurrencyStampEntity.ConcurrencyStamp = Guid.NewGuid().ToString();
            }
        }

        public void OnUpdating(object entity)
        {
            //if (entity is IHasConcurrencyStamp concurrencyStampEntity)
            //{
            //    concurrencyStampEntity.ConcurrencyStamp = Guid.NewGuid().ToString();
            //}
        }

        public void OnDeleting(object entity)
        {
            //if (entity is IHasConcurrencyStamp concurrencyStampEntity)
            //{
            //    concurrencyStampEntity.ConcurrencyStamp = Guid.NewGuid().ToString();
            //}
        }
    }
}
