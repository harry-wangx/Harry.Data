using System;
using System.Collections.Generic;

namespace Harry.Data
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        private int _page = 1;
        private int _size = 20;

        public Pagination() { }

        public Pagination(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value > 0)
                {
                    _page = value;
                }
            }
        }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value > 0)
                {
                    _size = value;
                }
            }
        }

        /// <summary>
        /// 跳过数量(最小值为0)
        /// </summary>
        public int Offset() => (_page - 1) * _size;

        /// <summary>
        /// 返回数量(最小为1)
        /// </summary>
        public int Limit() => _size;


        public Dictionary<string, OrderType> OrderFileds { get; set; }

        /// <summary>
        /// 添加排序项
        /// </summary>
        /// <returns></returns>
        public Pagination AddOrderItem(string field, OrderType orderType)
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentNullException(nameof(field));
            }
            if (OrderFileds == null)
                OrderFileds = new Dictionary<string, OrderType>(StringComparer.OrdinalIgnoreCase);

            //如果不存在该排序字段,则添加
            if (!OrderFileds.ContainsKey(field))
            {
                OrderFileds.Add(field, orderType);
            }
            return this;
        }

        /// <summary>
        /// 添加排序项
        /// </summary>
        /// <returns></returns>
        public Pagination AddOrderItem(string field, string orderType)
        {
            return AddOrderItem(field, GetOrderType(orderType));
        }

        public static OrderType GetOrderType(string input)
        {
            if (string.IsNullOrEmpty(input))
                return OrderType.ASC;

            return "DESC".Equals(input, StringComparison.OrdinalIgnoreCase) ? OrderType.DESC : OrderType.ASC;
        }

        public static Pagination From(int page, int size)
        {
            return new Pagination(page, size);
        }

        public enum OrderType
        {
            ASC, DESC
        }
    }
}
