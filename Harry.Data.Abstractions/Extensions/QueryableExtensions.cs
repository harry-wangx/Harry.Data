using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Harry.Data
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 按字段名称顺序排序
        /// </summary>
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyOrFieldName)
        {
            return source.OrderBy(CreateOrderByExpression<TSource, object>(propertyOrFieldName));
        }

        /// <summary>
        /// 按字段名称逆序排序
        /// </summary>
        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string propertyOrFieldName)
        {
            return source.OrderByDescending(CreateOrderByExpression<TSource, object>(propertyOrFieldName));
        }

        /// <summary>
        /// 按字段名称顺序排序
        /// </summary>
        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> source, string propertyOrFieldName)
        {
            return source.ThenBy(CreateOrderByExpression<TSource, object>(propertyOrFieldName));
        }

        /// <summary>
        /// 按字段名称逆序排序
        /// </summary>
        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(this IOrderedQueryable<TSource> source, string propertyOrFieldName)
        {
            return source.ThenByDescending(CreateOrderByExpression<TSource, object>(propertyOrFieldName));
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="pagination"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static PaginatedResult<TResult> PagesOf<TEntity, TResult>(this IQueryable<TEntity> source, Pagination pagination, Expression<Func<TEntity, TResult>> selector)
            where TEntity : class
            where TResult : class
        {
            var data = source.PagesOf(pagination);
            return new PaginatedResult<TResult>(data.Page, data.Size, data.Count, ((IQueryable<TEntity>)data.Data).Select(selector));
        }

        public static PaginatedResult<TResult> PagesOf<TEntity, TResult>(this IQueryable<TEntity> source, Pagination pagination, Expression<Func<TEntity, IEnumerable<TResult>>> selector)
            where TEntity : class
            where TResult : class
        {
            var data = source.PagesOf(pagination);
            return new PaginatedResult<TResult>(data.Page, data.Size, data.Count, ((IQueryable<TEntity>)data.Data).SelectMany(selector));
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public static PaginatedResult<TEntity> PagesOf<TEntity>(this IQueryable<TEntity> source, Pagination pagination) where TEntity : class
        {
            IQueryable<TEntity> results;
            if (pagination.OrderFileds == null || pagination.OrderFileds.Count <= 0)
            {
                results = source
                    .Skip(pagination.Offset())
                    .Take(pagination.Limit())
                    ;
            }
            else
            {
                IOrderedQueryable<TEntity> orderItems = null;
                bool isFirst = true;
                foreach (var item in pagination.OrderFileds)
                {
                    if (isFirst)
                    {
                        orderItems = item.Value == Pagination.OrderType.ASC ? source.OrderBy(item.Key) : source.OrderByDescending(item.Key);
                        isFirst = false;
                    }
                    else
                    {
                        orderItems = item.Value == Pagination.OrderType.ASC ? orderItems.ThenBy(item.Key) : orderItems.ThenByDescending(item.Key);
                    }
                }
                results = orderItems
                    .Skip(pagination.Offset())
                    .Take(pagination.Limit());
            }

            return new PaginatedResult<TEntity>(pagination.Page, pagination.Size, source.LongCount(), results);
        }

        public static PaginatedResult<TEntity> PagesOf<TEntity>(this IQueryable<TEntity> source, int page, int size) where TEntity : class
        {
            return source.PagesOf(Pagination.From(page, size));
        }

        public static Expression<Func<TEntity, TKey>> CreateOrderByExpression<TEntity, TKey>(string propertyOrFieldName)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            Expression propertyAccess = Expression.PropertyOrField(lambdaParam, propertyOrFieldName);
            return Expression.Lambda<Func<TEntity, TKey>>(Expression.Convert(propertyAccess, typeof(TKey)), lambdaParam);
        }
    }
}
