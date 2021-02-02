
namespace Harry.Data
{
    public interface IDataHandler
    {
        /// <summary>
        /// 排序值(数值小的先执行)
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 插入前执行
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void OnInserting(object entity);

        /// <summary>
        /// 更新前执行
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void OnUpdating(object entity);

        /// <summary>
        /// 删除前执行
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void OnDeleting(object entity);
    }
}
