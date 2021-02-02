using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
