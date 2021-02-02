using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Events
{
    public class DataChangeEvent : Harry.EventBus.Event
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 数据编号
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType EventType { get; set; }
    }
}
