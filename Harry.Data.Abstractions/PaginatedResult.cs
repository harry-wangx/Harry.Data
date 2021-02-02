using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PaginatedResult<TEntity> where TEntity : class
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public long Count { get; private set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public IEnumerable<TEntity> Data { get; private set; }

        public PaginatedResult(int page, int size, long count, IEnumerable<TEntity> data)
        {
            this.Page = page;
            this.Size = size;
            this.Count = count;
            this.Data = data;
        }

    }
}
