using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}
