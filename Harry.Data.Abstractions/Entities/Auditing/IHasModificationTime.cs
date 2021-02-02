using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IHasModificationTime
    {
        /// <summary>
        /// 更改时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
