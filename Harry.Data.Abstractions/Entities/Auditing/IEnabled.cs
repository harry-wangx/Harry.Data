using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities.Auditing
{
    public interface IEnabled
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
