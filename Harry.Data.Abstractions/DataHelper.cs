using System;
using System.Linq.Expressions;

namespace Harry.Data
{
    public static class DataHelper
    {
        public static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId<TEntity, TPrimaryKey>(TPrimaryKey id, string idName = null)
        {
            if (string.IsNullOrEmpty(idName))
                idName = "Id";

            return LinqExtensions.CreateEqualityExpression<TEntity, TPrimaryKey>(idName, id);
        }
    }
}
